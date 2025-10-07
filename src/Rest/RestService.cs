using System.Reflection;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace BlackDigital.Rest
{
    public class RestService<TService>
    {
        public RestService(RestClient client)
        {
            Client = client;
        }

        protected readonly RestClient Client;

        public async Task<TReturn?> CallAsync<TReturn>(Expression<Func<TService, Task<TReturn>>> expression)
        {
            var result = await ExecuteRequestAsync(expression);

            return result is TReturn ? (TReturn)result : default;
        }

        public async Task CallAsync(Expression<Action<TService>> expression)
        {
            await ExecuteRequestAsync(expression);
        }

        private async Task<object?> ExecuteRequestAsync(LambdaExpression expression)
        {
            if (expression.Body is not MethodCallExpression)
                throw new ArgumentException("Invalid method expression", nameof(expression));

            var methodCall = (MethodCallExpression)expression.Body;
            var methodInfo = methodCall.Method;

            if (methodInfo.DeclaringType != typeof(TService))
                throw new ArgumentException("Invalid declaring type method expression", nameof(expression));

            var service = methodInfo.DeclaringType.GetCustomAttribute<ServiceAttribute>()
                ?? throw new CustomAttributeFormatException("ServiceAttribute");

            var action = methodInfo.GetCustomAttribute<ActionAttribute>()
                ?? throw new CustomAttributeFormatException("ActionAttribute");

            Dictionary<string, object> arguments = [];

            for (int i = 0; i < methodCall.Arguments.Count; i++)
            {
                var parameter = methodInfo.GetParameters()[i];

                var argument = methodCall.Arguments[i];

                if (argument.CanReduce)
                    argument = argument.Reduce();

                var value = Expression.Lambda(argument).Compile().DynamicInvoke();

                arguments.Add(parameter.Name, value);
            }

            var url = GenerateRoute(methodInfo, service, action, arguments);
            var headers = GenerateHeaders(methodInfo, service, action, arguments);
            var body = GenerateBody(methodInfo, service, action, arguments);
            var method = GetHttpMethod(action);
            var returnType = GetResponseType(methodInfo, action);

            return await Client.RequestAsync(method, url, returnType, body, headers, null);
        }

        #region "Create Call"

        private static string GenerateRoute(MethodInfo methodInfo,
                                     ServiceAttribute serviceAttribute,
                                     ActionAttribute actionAttribute,
                                     Dictionary<string, object> arguments)
        {
            var parameters = RestParameter<PathAttribute>.GetParameters(methodInfo.GetParameters(), arguments);

            var urls = new List<string?>()
            {
                RestService<TService>.GetRouteUrl(serviceAttribute?.BaseRoute, parameters),
                RestService<TService>.GetRouteUrl(actionAttribute?.Route, parameters)
            };

            urls.Remove(null);

            return RestService<TService>.InsertQueryString(string.Join("/", urls),
                                     methodInfo,
                                     serviceAttribute,
                                     actionAttribute,
                                     arguments);
        }

        private static Dictionary<string, string> GenerateHeaders(MethodInfo methodInfo,
                                     ServiceAttribute serviceAttribute,
                                     ActionAttribute actionAttribute,
                                     Dictionary<string, object> arguments)
        {
            var parameters = RestParameter<HeaderAttribute>.GetParameters(methodInfo.GetParameters(), arguments);

            Dictionary<string, string> headers = new();

            parameters.ForEach(p => headers.Add(p.Attribute?.Name ?? p.Name, p.Value?.ToString() ?? string.Empty));

            return headers;
        }

        private static object? GenerateBody(MethodInfo methodInfo,
                                     ServiceAttribute serviceAttribute,
                                     ActionAttribute actionAttribute,
                                     Dictionary<string, object> arguments)
        {
            var parameters = RestParameter<BodyAttribute>.GetParameters(methodInfo.GetParameters(), arguments);

            if (parameters.Count <= 0)
                return null;

            var parameter = parameters.Single();
            return parameter.Value;
        }

        private static string? GetRouteUrl(string? route, List<RestParameter<PathAttribute>> arguments)
        {
            if (route == null)
                return null;

            return Regex.Replace(route, @"{(.+?)}",
                match =>
                {
                    var parameter = match.Value.Replace("{", "").Replace("}", "");

                    if (parameter.Contains(":"))
                        parameter = parameter.Split(":")[0];

                    var routeParameter = arguments.FirstOrDefault(p => p.Attribute?.Name == parameter
                                                        || (p.Attribute?.Name == null && p.Name == parameter));

                    if (routeParameter != null)
                        return routeParameter.Value?.ToString() ?? string.Empty;

                    throw new FormatException(parameter);
                });
        }

        private static string InsertQueryString(string url,
                                         MethodInfo methodInfo,
                                         ServiceAttribute serviceAttribute,
                                         ActionAttribute actionAttribute,
                                         Dictionary<string, object> arguments)
        {
            var parameters = RestParameter<QueryAttribute>.GetParameters(methodInfo.GetParameters(), arguments);

            if (parameters.Count <= 0)
                return url;

            List<string> queries = new();

            foreach (var parameter in parameters)
            {
                if (parameter.Attribute?.Type == QueryParameterType.Raw)
                    queries.Add(parameter.Value?.ToUrlQueryString() ?? string.Empty);
                else
                    queries.Add($"{parameter.Attribute?.Name ?? parameter.Name}={parameter.Value?.ToString() ?? string.Empty}");
            }

            return $"{url}?{string.Join("&", queries)}";
        }

        private static HttpMethod GetHttpMethod(ActionAttribute actionAttribute)
        {
            return actionAttribute.Method switch
            {
                RestMethod.Post => HttpMethod.Post,
                RestMethod.Put => HttpMethod.Put,
                RestMethod.Delete => HttpMethod.Delete,
                RestMethod.Patch => HttpMethod.Patch,
                _ => HttpMethod.Get,
            };
        }

        private static Type? GetResponseType(MethodInfo methodInfo, ActionAttribute actionAttribute)
        {
            if (actionAttribute.ReturnIsSuccess || methodInfo.ReturnType == null)
                return null;

            if (methodInfo.ReturnType.IsGenericType
                && methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                return methodInfo.ReturnType.GetGenericArguments()[0];

            return methodInfo.ReturnType;
        }

        #endregion "Create Call"
    }
}
