
namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class RouteAttribute : Attribute
    {
        public RouteAttribute(string? name = null)
        {
            Name = name;
        }

        public string? Name { get; }
    }
}
