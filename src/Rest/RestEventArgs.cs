
using System.Net;

namespace BlackDigital.Rest
{
    /// <summary>
    /// Represents the method that handles REST client events.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    public delegate void RestEventHandler(RestEventArgs e);

    /// <summary>
    /// Provides data for REST client events, including the request URL, status code, and headers.
    /// </summary>
    public class RestEventArgs
    {
        /// <summary>
        /// Gets or sets the request URL.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the response status code.
        /// </summary>
        public HttpStatusCode? StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the request headers.
        /// </summary>
        public Dictionary<string, string>? RequestHeaders { get; set; }

        /// <summary>
        /// Gets or sets the response headers.
        /// </summary>
        public Dictionary<string, string>? ResponseHeaders { get; set; }

        /// <summary>
        /// Creates a <see cref="RestEventArgs"/> instance from an HTTP request and response.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="response">The HTTP response message, if any.</param>
        /// <returns>A new <see cref="RestEventArgs"/> instance.</returns>
        public static RestEventArgs Create(HttpRequestMessage request, HttpResponseMessage? response)
        {
            return new RestEventArgs
            {
                Url = request.RequestUri?.ToString(),
                StatusCode = response?.StatusCode,
                RequestHeaders = request.Headers.ToDictionary(h => h.Key, h => string.Join(",", h.Value)),
                ResponseHeaders = response?.Headers.ToDictionary(h => h.Key, h => string.Join(",", h.Value))
            };
        }
    }
}
