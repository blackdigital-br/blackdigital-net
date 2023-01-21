using System.Reflection;
using System.Text.RegularExpressions;

namespace BlackDigital.Rest
{
    public abstract class BaseService<BaseType>
    {
        #region "Constructors"

        public BaseService(RestClient client)
        {
            Client = client;
            ServiceAttributes = typeof(BaseType).GetCustomAttributes(true)
                                                .Cast<Attribute>()
                                                .ToList();
        }

        #endregion "Constructors"

        #region "Properties"

        protected readonly RestClient Client;

        private List<Attribute> ServiceAttributes;

        #endregion "Properties"

        #region "Methods"

        protected async Task<T> ExecuteRequest<T>(string name, Dictionary<string, object> arguments)
        {
            if (Client == null)
                throw new ArgumentNullException("RestClient");

            var methodInfo = typeof(BaseType).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                             .Where(x => x.Name == name && x.GetParameters().Length == arguments.Count)
                                             .SingleOrDefault();

            if (methodInfo == null)
                throw new MethodAccessException($"{name}: {arguments.Count}");


            var service = ServiceAttributes.Where(x => x is ServiceAttribute)
                                           .Cast<ServiceAttribute>()
                                           .FirstOrDefault();

            if (service == null)
                throw new CustomAttributeFormatException("ServiceAttribute");

            var action = methodInfo.GetCustomAttribute<ActionAttribute>();

            if (action == null)
                throw new CustomAttributeFormatException("ActionAttribute");

            var url = GenerateRoute(methodInfo, service, action, arguments);
            var headers = GenerateHeaders(methodInfo, service, action, arguments);
            var body = GenerateBody(methodInfo, service, action, arguments);
            var method = GetHttpMethod(action);
            var returnType = GetResponseType(methodInfo, action);

            var response = await Client.RequestAsync(method, url, returnType, body, headers, false);

            if (methodInfo.ReturnType != null)
                return (T)response;

            return default;
        }

        private string GenerateRoute(MethodInfo methodInfo,
                                     ServiceAttribute serviceAttribute,
                                     ActionAttribute actionAttribute,
                                     Dictionary<string, object> arguments)
        {
            var parameters = RestParameter<FromRouteAttribute>.GetParameters(methodInfo.GetParameters(), arguments);

            var urls = new List<string?>()
            {
                GetRouteUrl(serviceAttribute?.BaseRoute, parameters),
                GetRouteUrl(actionAttribute?.Route, parameters)
            };

            urls.Remove(null);

            return InsertQueryString(string.Join("/", urls),
                                     methodInfo,
                                     serviceAttribute,
                                     actionAttribute,
                                     arguments);
        }

        private Dictionary<string, string> GenerateHeaders(MethodInfo methodInfo,
                                     ServiceAttribute serviceAttribute,
                                     ActionAttribute actionAttribute,
                                     Dictionary<string, object> arguments)
        {
            var parameters = RestParameter<FromHeaderAttribute>.GetParameters(methodInfo.GetParameters(), arguments);

            Dictionary<string, string> headers = new();

            parameters.ForEach(p => headers.Add(p.Attribute?.Name ?? p.Name, p.Value?.ToString() ?? string.Empty));

            return headers;
        }

        private object? GenerateBody(MethodInfo methodInfo,
                                     ServiceAttribute serviceAttribute,
                                     ActionAttribute actionAttribute,
                                     Dictionary<string, object> arguments)
        {
            var parameters = RestParameter<FromBodyAttribute>.GetParameters(methodInfo.GetParameters(), arguments);

            if (parameters.Count <= 0)
                return null;

            var parameter = parameters.Single();
            return parameter.Value;
        }

        private string? GetRouteUrl(string? route, List<RestParameter<FromRouteAttribute>> arguments)
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

        private string InsertQueryString(string url,
                                         MethodInfo methodInfo,
                                         ServiceAttribute serviceAttribute,
                                         ActionAttribute actionAttribute,
                                         Dictionary<string, object> arguments)
        {
            var parameters = RestParameter<FromQueryAttribute>.GetParameters(methodInfo.GetParameters(), arguments);

            if (parameters.Count <= 0)
                return url;

            List<string> queries = new();

            foreach (var parameter in parameters)
            {
                if (parameter.Attribute?.Type == QueryParameterType.Raw)
                    queries.Add(parameter.Value?.ToQueryString() ?? string.Empty);
                else
                    queries.Add($"{parameter.Attribute?.Name ?? parameter.Name}={parameter.Value?.ToString() ?? string.Empty}");
            }

            return $"{url}?{string.Join("&", queries)}";
        }

        private HttpMethod GetHttpMethod(ActionAttribute actionAttribute)
        {
            return actionAttribute.Method switch
            {
                RestMethod.Post => HttpMethod.Post,
                RestMethod.Put => HttpMethod.Put,
                RestMethod.Delete => HttpMethod.Delete,
                _ => HttpMethod.Get,
            };
        }

        private Type? GetResponseType(MethodInfo methodInfo, ActionAttribute actionAttribute)
        {
            if (actionAttribute.ReturnIsSuccess || methodInfo.ReturnType == null)
                return null;

            if (methodInfo.ReturnType.IsGenericType 
                && methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                return methodInfo.ReturnType.GetGenericArguments()[0];

            return methodInfo.ReturnType;
        }

        #endregion "Methods"
    }
}
