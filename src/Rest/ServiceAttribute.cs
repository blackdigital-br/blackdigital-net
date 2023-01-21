namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute(string? baseRoute = null)
        {
            BaseRoute = baseRoute;
        }

        public string? BaseRoute { get; set; }
    }
}
