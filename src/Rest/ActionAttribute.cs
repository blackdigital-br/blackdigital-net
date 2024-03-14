
namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ActionAttribute : Attribute
    {
        public ActionAttribute(string? route = null, 
                               RestMethod method = RestMethod.Get, 
                               bool authorize = true,
                               bool returnIsSuccess = false)
        {
            Route = route;
            Method = method;
            Authorize = authorize;
            ReturnIsSuccess = returnIsSuccess;
        }

        public string? Route { get; private set; }

        public RestMethod Method { get; private set; }

        public bool Authorize { get; private set; }

        public bool ReturnIsSuccess { get; private set; }

        public bool IsMatch(string method, string? route)
        {
            if (Enum.GetName(Method)?.ToLower() != method.ToLower())
                return false;

            if (string.IsNullOrWhiteSpace(Route)
                && string.IsNullOrWhiteSpace(route))
                return true;

            if (route == null)
                return false;

            var routePaths = route?.Split('/');
            var actionPaths = Route?.Split('/');

            if (routePaths?.Length != actionPaths?.Length)
                return false;

            for (int i = 0; i < actionPaths.Length; i++)
            {
                if (actionPaths[i].StartsWith('{') && actionPaths[i].EndsWith('}'))
                {
                    if (actionPaths[i].Contains(':'))
                    {

                    }

                    continue;
                }

                if (actionPaths[i].ToLower() != routePaths[i].ToLower())
                    return false;
            }

            return true;
        }
    }
}
