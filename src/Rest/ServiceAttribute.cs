namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute(string baseRoute,
            bool authorize = false,
            string? version = null,
            string? name = null,
            string? description = null,
            Type? resourceType = null)
        {
            BaseRoute = baseRoute;
            Authorize = authorize;
            Version = version;
            Name = name;
            Description = description;
            ResourceType = resourceType;
        }

        public string BaseRoute { get; set; }

        public bool Authorize { get; set; }

        public string? Version { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Type? ResourceType { get; set; }

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

        public string GetServiceName()
        {
            return ObjectHelper.GetResourceValue(Name, ResourceType) ?? string.Empty;
        }

        public string GetServiceDescription()
        {
            return ObjectHelper.GetResourceValue(Description, ResourceType) ?? string.Empty;
        }
    }
}
