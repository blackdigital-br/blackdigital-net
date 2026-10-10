
namespace BlackDigital.Rest
{
    /// <summary>
    /// Marks a method as a REST action and defines its route, HTTP method, and metadata.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ActionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionAttribute"/> class.
        /// </summary>
        /// <param name="route">The route template for the action.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="authorize">Whether the action requires authorization.</param>
        /// <param name="returnIsSuccess">Whether the return value indicates success.</param>
        /// <param name="keyIdentifier">The key identifier for the action.</param>
        /// <param name="version">The API version.</param>
        public ActionAttribute(string? route = null, 
                               RestMethod method = RestMethod.Get, 
                               bool authorize = true,
                               bool returnIsSuccess = false,
                               string? keyIdentifier = null,
                               string? version = null)
        {
            Route = route;
            Method = method;
            Authorize = authorize;
            ReturnIsSuccess = returnIsSuccess;
            Version = version;
            KeyIdentifier = keyIdentifier;
        }

        /// <summary>
        /// Gets the key identifier for the action.
        /// </summary>
        public string? KeyIdentifier { get; private set; }

        /// <summary>
        /// Gets the route template for the action.
        /// </summary>
        public string? Route { get; private set; }

        /// <summary>
        /// Gets the HTTP method.
        /// </summary>
        public RestMethod Method { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the action requires authorization.
        /// </summary>
        public bool Authorize { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the return value indicates success.
        /// </summary>
        public bool ReturnIsSuccess { get; private set; }

        /// <summary>
        /// Gets the API version.
        /// </summary>
        public string? Version { get; private set; }

        /// <summary>
        /// Determines whether the specified HTTP method and route match this action.
        /// </summary>
        /// <param name="method">The HTTP method to check.</param>
        /// <param name="route">The route to check.</param>
        /// <returns><c>true</c> if the method and route match; otherwise, <c>false</c>.</returns>
        public bool IsMatch(string method, string? route)
        {
            if (!string.Equals(Enum.GetName(Method), method, StringComparison.OrdinalIgnoreCase))
                return false;

            if (string.IsNullOrWhiteSpace(Route)
                && string.IsNullOrWhiteSpace(route))
                return true;

            if (route == null || string.IsNullOrWhiteSpace(Route))
                return false;

            var routePaths = route.Split('/');
            var actionPaths = Route.Split('/');

            if (routePaths.Length != actionPaths.Length)
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

                if (!string.Equals(actionPaths[i], routePaths[i], StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }
    }
}
