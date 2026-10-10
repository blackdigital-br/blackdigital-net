namespace BlackDigital.Rest
{
    /// <summary>
    /// Specifies how a query parameter should be serialized.
    /// </summary>
    public enum QueryParameterType
    {
        /// <summary>
        /// The parameter is serialized as a named query parameter.
        /// </summary>
        Parameter,

        /// <summary>
        /// The parameter is serialized as a raw query string value.
        /// </summary>
        Raw,
    }
}
