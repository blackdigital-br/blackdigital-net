
namespace BlackDigital.Rest
{
    /// <summary>
    /// Marks a method parameter as a query parameter for the REST request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class QueryAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryAttribute"/> class.
        /// </summary>
        /// <param name="type">The way the query parameter should be serialized.</param>
        /// <param name="name">The optional name of the query parameter. Defaults to the parameter name.</param>
        public QueryAttribute(QueryParameterType type = QueryParameterType.Raw, string? name = null)
        {
            Type = type;
            Name = name;
        }

        /// <summary>
        /// Gets the name of the query parameter.
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// Gets or sets the way the query parameter should be serialized.
        /// </summary>
        public QueryParameterType Type { get; set; }
    }
}
