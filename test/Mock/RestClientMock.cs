using BlackDigital.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Test.Mock
{
    public class RestClientMock : RestClient
    {
        public RestClientMock()
            : base("http://blackdigital.com.br/test", new HttpMessageHandlerMock())
        {
            ((HttpMessageHandlerMock)HttpHandler).RestClient = this;
        }

        public HttpMessageHandlerMock Handler => (HttpMessageHandlerMock)HttpHandler;

        public HttpRequestMessage? LastRequest => Handler.LastRequest;

        public HttpResponseMessage? LastResponse => Handler.LastResponse;
    }
}
