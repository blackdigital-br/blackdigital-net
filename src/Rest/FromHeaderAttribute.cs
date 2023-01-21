
namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class FromHeaderAttribute : Attribute
    {
        public FromHeaderAttribute(string? name = null)
        {
            Name = name;
        }

        public string? Name { get; }
    }
}
