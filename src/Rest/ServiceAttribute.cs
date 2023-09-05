namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute(string baseRoute, bool authorize = false)
        {
            BaseRoute = baseRoute;
            Authorize = authorize;
        }

        public string BaseRoute { get; set; }

        public bool Authorize { get; set; }
    }
}
