namespace BlackDigital.Rest
{
    /// <summary>
    /// Marks a class or interface as a REST service and defines its base route and metadata.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class ServiceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAttribute"/> class.
        /// </summary>
        /// <param name="baseRoute">The base route of the service.</param>
        /// <param name="version">The API version.</param>
        /// <param name="name">The display name of the service.</param>
        /// <param name="description">The description of the service.</param>
        /// <param name="resourceType">The resource type used to resolve localized name and description.</param>
        public ServiceAttribute(string baseRoute,
            string? version = null,
            string? name = null,
            string? description = null,
            Type? resourceType = null)
        {
            BaseRoute = baseRoute;
            Version = version;
            Name = name;
            Description = description;
            ResourceType = resourceType;
        }

        /// <summary>
        /// Gets or sets the base route of the service.
        /// </summary>
        public string BaseRoute { get; set; }

        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        /// Gets or sets the display name of the service.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the service.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the resource type used to resolve localized name and description.
        /// </summary>
        public Type? ResourceType { get; set; }

        /// <summary>
        /// Determines whether the specified route matches this service's base route.
        /// </summary>
        /// <param name="route">The route to check.</param>
        /// <returns>The remaining path after the base route, or <c>null</c> if the route does not match.</returns>
        public string? IsMatch(string? route)
        {
            if (route == null)
                return null;

            var routePaths = route.Split('/');
            var baseRoutePaths = BaseRoute.Split('/');

            for (int i = 0; i < baseRoutePaths.Length; i++)
            {
                if (baseRoutePaths[i].ToLower() != routePaths[i].ToLower())
                    return null;
            }

            return string.Join('/', routePaths.Skip(baseRoutePaths.Length));
        }

        /// <summary>
        /// Gets the localized service name.
        /// </summary>
        /// <returns>The resolved service name.</returns>
        public string GetServiceName()
        {
            return ObjectHelper.GetResourceValue(Name, ResourceType) ?? string.Empty;
        }

        /// <summary>
        /// Gets the localized service description.
        /// </summary>
        /// <returns>The resolved service description.</returns>
        public string GetServiceDescription()
        {
            return ObjectHelper.GetResourceValue(Description, ResourceType) ?? string.Empty;
        }
    }
}
