
using System.Net;

namespace BlackDigital.Rest
{
    public delegate void RestEventHandler(RestEventArgs e);

    public class RestEventArgs
    {
        public string? Url { get; set; }

        public HttpStatusCode? StatusCode { get; set; }

        public Dictionary<string, string>? RequestHeaders { get; set; }

        public Dictionary<string, string>? ResponseHeaders { get; set; }

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
