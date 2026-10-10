using System.Reflection;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace BlackDigital.Rest
{
    /// <summary>
    /// Provides a strongly-typed wrapper for invoking REST service methods defined with <see cref="ServiceAttribute"/> and <see cref="ActionAttribute"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service interface or class.</typeparam>
    public class RestService<TService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestService{TService}"/> class.
        /// </summary>
        /// <param name="client">The <see cref="RestClient"/> used to execute requests.</param>
        public RestService(RestClient client)
        {
            Client = client;
        }

        protected readonly RestClient Client;

        /// <summary>
        /// Calls a service method that returns a value, using the provided call configuration.
        /// </summary>
        /// <typeparam name="TReturn">The return type of the service method.</typeparam>
        /// <param name="expression">An expression selecting the service method to call.</param>
        /// <param name="callConfig">The call configuration (headers, query parameters, etc.).</param>
        /// <returns>The deserialized response, or <c>null</c> if the request failed.</returns>
        public async Task<TReturn?> CallAsync<TReturn>(Expression<Func<TService, Task<TReturn>>> expression, RestCallConfig callConfig)
        {
            var result = await ExecuteRequestAsync(expression, callConfig);

            return result is TReturn ? (TReturn)result : default;
        }

        /// <summary>
        /// Calls a service method that returns a value, using the default call configuration.
        /// </summary>
        /// <typeparam name="TReturn">The return type of the service method.</typeparam>
        /// <param name="expression">An expression selecting the service method to call.</param>
        /// <returns>The deserialized response, or <c>null</c> if the request failed.</returns>
        public async Task<TReturn?> CallAsync<TReturn>(Expression<Func<TService, Task<TReturn>>> expression)
            => await CallAsync<TReturn>(expression, RestCallConfig.Create());

        /// <summary>
        /// Calls a service method that returns a value, using the specified API version.
        /// </summary>
        /// <typeparam name="TReturn">The return type of the service method.</typeparam>
        /// <param name="expression">An expression selecting the service method to call.</param>
        /// <param name="version">The API version to use.</param>
        /// <returns>The deserialized response, or <c>null</c> if the request failed.</returns>
        public async Task<TReturn?> CallAsync<TReturn>(Expression<Func<TService, Task<TReturn>>> expression, string version)
            => await CallAsync<TReturn>(expression, RestCallConfig.Create().AddVersion(version));

        /// <summary>
        /// Calls a service method that does not return a value, using the provided call configuration.
        /// </summary>
        /// <param name="expression">An expression selecting the service method to call.</param>
        /// <param name="callConfig">The call configuration (headers, query parameters, etc.).</param>
        public async Task CallAsync(Expression<Action<TService>> expression, RestCallConfig callConfig)
        {
            await ExecuteRequestAsync(expression, callConfig);
        }

        /// <summary>
        /// Calls a service method that does not return a value, using the default call configuration.
        /// </summary>
        /// <param name="expression">An expression selecting the service method to call.</param>
        public async Task CallAsync(Expression<Action<TService>> expression)
            => await CallAsync(expression, RestCallConfig.Create());

        /// <summary>
        /// Calls a service method that does not return a value, using the specified API version.
        /// </summary>
        /// <param name="expression">An expression selecting the service method to call.</param>
        /// <param name="version">The API version to use.</param>
        public async Task CallAsync(Expression<Action<TService>> expression, string version)
            => await CallAsync(expression, RestCallConfig.Create().AddVersion(version));


        private async Task<object?> ExecuteRequestAsync(LambdaExpression expression, RestCallConfig callConfig)
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

            Dictionary<string, object?> arguments = [];

            for (int i = 0; i < methodCall.Arguments.Count; i++)
            {
                var parameter = methodInfo.GetParameters()[i];

                var argument = methodCall.Arguments[i];

                if (argument.CanReduce)
                    argument = argument.Reduce();

                var value = Expression.Lambda(argument).Compile().DynamicInvoke();

                arguments.Add(parameter.Name ?? throw new InvalidOperationException("Parameter name cannot be null."), value);
            }

            var url = GenerateRoute(methodInfo, service, action, arguments, callConfig);
            var headers = GenerateHeaders(methodInfo, service, action, arguments, callConfig);
            var body = GenerateBody(methodInfo, service, action, arguments);
            var method = GetHttpMethod(action);
            var returnType = GetResponseType(methodInfo, action);

            return await Client.RequestAsync(method, url, returnType, body, headers, null);
        }

        #region "Create Call"

        private static string GenerateRoute(MethodInfo methodInfo,
                                     ServiceAttribute serviceAttribute,
                                     ActionAttribute actionAttribute,
                                     Dictionary<string, object?> arguments,
                                     RestCallConfig callConfig)
        {
            var parameters = RestParameter<PathAttribute>.GetParameters(methodInfo.GetParameters(), arguments);

            var urls = new List<string?>()
            {
                RestService<TService>.GetRouteUrl(serviceAttribute.BaseRoute, parameters),
                RestService<TService>.GetRouteUrl(actionAttribute.Route, parameters)
            };

            urls.Remove(null);

            return RestService<TService>.InsertQueryString(string.Join("/", urls),
                                     methodInfo,
                                     serviceAttribute,
                                     actionAttribute,
                                     arguments,
                                     callConfig);
        }

        private static Dictionary<string, string> GenerateHeaders(MethodInfo methodInfo,
                                     ServiceAttribute serviceAttribute,
                                     ActionAttribute actionAttribute,
                                     Dictionary<string, object?> arguments,
                                     RestCallConfig callConfig)
        {
            var parameters = RestParameter<HeaderAttribute>.GetParameters(methodInfo.GetParameters(), arguments);

            Dictionary<string, string> headers = new();

            parameters.ForEach(p => headers.Add(p.Attribute?.Name ?? p.Name, p.Value?.ToString() ?? string.Empty));

            string? version = actionAttribute?.Version ?? serviceAttribute?.Version;

            if (!string.IsNullOrWhiteSpace(version))
                headers.Add("x-api-version", version);

            if (callConfig != null 
                && callConfig.ExtraHeaders != null
                && callConfig.ExtraHeaders.Any())
            {
                foreach (var header in callConfig.ExtraHeaders)
                {
                    if (headers.ContainsKey(header.Key))
                        headers[header.Key] = header.Value;
                    else
                        headers.Add(header.Key, header.Value);
                }
            }

            return headers;
        }

        private static object? GenerateBody(MethodInfo methodInfo,
                                     ServiceAttribute serviceAttribute,
                                     ActionAttribute actionAttribute,
                                     Dictionary<string, object?> arguments)
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
                                         Dictionary<string, object?> arguments,
                                         RestCallConfig callConfig)
        {
            var parameters = RestParameter<QueryAttribute>.GetParameters(methodInfo.GetParameters(), arguments);

            List<string> queries = new();

            foreach (var parameter in parameters)
            {
                if (parameter.Attribute?.Type == QueryParameterType.Raw)
                    queries.Add(parameter.Value?.ToUrlQueryString() ?? string.Empty);
                else
                    queries.Add($"{parameter.Attribute?.Name ?? parameter.Name}={parameter.Value?.ToString() ?? string.Empty}");
            }

            if (callConfig != null
                && callConfig.QueryParameters != null
                && callConfig.QueryParameters.Any())
            {
                foreach (var header in callConfig.QueryParameters)
                {
                    if (string.IsNullOrEmpty(header.Value))
                        queries.Add(header.Key);
                    else
                        queries.Add($"{header.Key}={header.Value}");
                }
            }

            if (queries.Count <= 0)
                return url;

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
