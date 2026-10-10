namespace BlackDigital.Rest
{
    /// <summary>
    /// Provides a fluent builder for configuring REST call options such as headers and query parameters.
    /// </summary>
    public class RestCallConfig
    {
        /// <summary>
        /// Gets the extra headers to include in the request.
        /// </summary>
        public List<KeyValuePair<string, string>>? ExtraHeaders { get; private set; }

        /// <summary>
        /// Gets the query parameters to include in the request.
        /// </summary>
        public List<KeyValuePair<string, string>>? QueryParameters { get; private set; }

        /// <summary>
        /// Adds a header to the request.
        /// </summary>
        /// <param name="key">The header name.</param>
        /// <param name="value">The header value.</param>
        /// <returns>The current <see cref="RestCallConfig"/> instance for chaining.</returns>
        public RestCallConfig AddHeader(string key, string value)
        {
            if (ExtraHeaders == null)
                ExtraHeaders = new();

            ExtraHeaders.Add(new KeyValuePair<string, string>(key, value));

            return this;
        }

        /// <summary>
        /// Adds a query parameter to the request.
        /// </summary>
        /// <param name="key">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The current <see cref="RestCallConfig"/> instance for chaining.</returns>
        public RestCallConfig AddQueryParameter(string key, string value)
        {
            if (QueryParameters == null)
                QueryParameters = new();

            QueryParameters.Add(new KeyValuePair<string, string>(key, value));
            return this;
        }

        /// <summary>
        /// Adds an API version query parameter.
        /// </summary>
        /// <param name="version">The API version.</param>
        /// <returns>The current <see cref="RestCallConfig"/> instance for chaining.</returns>
        public RestCallConfig AddVersion(string version)
         => AddQueryParameter("api-version", version);

        /// <summary>
        /// Creates a new instance of <see cref="RestCallConfig"/>.
        /// </summary>
        /// <returns>A new <see cref="RestCallConfig"/> instance.</returns>
        public static RestCallConfig Create()
            => new RestCallConfig();
    }
}
