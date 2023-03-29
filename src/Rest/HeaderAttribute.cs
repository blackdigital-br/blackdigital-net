
namespace BlackDigital.Rest
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class HeaderAttribute : Attribute
    {
        public HeaderAttribute(string? name = null)
        {
            Name = name;
        }

        public string? Name { get; }
    }
}
