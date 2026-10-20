
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
        /// <param name="returnIsSuccess">Whether the return value indicates success.</param>
        /// <param name="keyIdentifier">The key identifier for the action.</param>
        /// <param name="version">The API version.</param>
        /// <param name="name">The display name of the action.</param>
        /// <param name="description">The description of the action.</param>
        /// <param name="resourceType">The resource type used to resolve localized name and description.</param>
        public ActionAttribute(string? route = null,
                               RestMethod method = RestMethod.Get,
                               bool returnIsSuccess = false,
                               string? keyIdentifier = null,
                               string? version = null,
                               string? name = null,
                               string? description = null,
                               Type? resourceType = null)
        {
            Route = route;
            Method = method;
            ReturnIsSuccess = returnIsSuccess;
            Version = version;
            KeyIdentifier = keyIdentifier;
            Name = name;
            Description = description;
            ResourceType = resourceType;
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
        /// Gets a value indicating whether the return value indicates success.
        /// </summary>
        public bool ReturnIsSuccess { get; private set; }

        /// <summary>
        /// Gets the API version.
        /// </summary>
        public string? Version { get; private set; }

        /// <summary>
        /// Gets the display name of the action.
        /// </summary>
        public string? Name { get; private set; }

        /// <summary>
        /// Gets the description of the action.
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// Gets the resource type used to resolve localized name and description.
        /// </summary>
        public Type? ResourceType { get; private set; }

        /// <summary>
        /// Gets the localized action name.
        /// </summary>
        /// <returns>The resolved action name.</returns>
        public string GetName()
        {
            return ObjectHelper.GetResourceValue(Name, ResourceType) ?? string.Empty;
        }

        /// <summary>
        /// Gets the localized action description.
        /// </summary>
        /// <returns>The resolved action description.</returns>
        public string GetDescription()
        {
            return ObjectHelper.GetResourceValue(Description, ResourceType) ?? string.Empty;
        }

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
