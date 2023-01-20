using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Test.Mock
{
    public class HttpMessageHandlerMock : HttpMessageHandler
    {
        public HttpMessageHandlerMock() { }

        private Dictionary<string, HttpResponseMessage> Responses = new();

        public RestClientMock? RestClient { get; set; }

        public HttpRequestMessage LastRequest { get; set; }

        public HttpResponseMessage LastResponse { get; set; }

        public HttpMessageHandlerMock AddResponse(string url, HttpStatusCode status)
        {
            Responses.Add(url, new HttpResponseMessage(status));

            return this;
        }

        public HttpMessageHandlerMock AddResponse(string url, HttpResponseMessage response)
        {
            Responses.Add(url, response);

            return this;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;

            if (request.RequestUri != null 
                && Responses.TryGetValue(request.RequestUri.AbsoluteUri, out var value))
            {
                LastResponse = value;
            }
            else
            {
                LastResponse = new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return Task.FromResult(LastResponse);
        }
    }
}
