
namespace BlackDigital.Rest
{
    /// <summary>
    /// Marks a method parameter as a header for the REST request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class HeaderAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderAttribute"/> class.
        /// </summary>
        /// <param name="name">The optional name of the header. Defaults to the parameter name.</param>
        public HeaderAttribute(string? name = null)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of the header.
        /// </summary>
        public string? Name { get; }
    }
}
