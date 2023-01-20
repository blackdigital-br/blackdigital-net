using BlackDigital.Test.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Test.Rest
{
    public class RestClientTest
    {
        [Fact]
        public async void AddAuthentication()
        {
            var client = new RestClientMock();

            client.AddAuthentication(new AuthenticationHeaderValue("bearer", "ABCDEFGHIJKLMNOPQRSTUWXYZ"));

            var test = await client.GetRestAsync<bool>("test");

            Assert.NotNull(client.LastRequest);
            Assert.Equal(1, client.LastRequest?.Headers.Count() ?? 0);
            Assert.Contains(client.LastRequest.Headers, x => x.Key == "Authorization");
            Assert.Equal("bearer ABCDEFGHIJKLMNOPQRSTUWXYZ", client.LastRequest?.Headers.GetValues("Authorization").FirstOrDefault());
        }

        [Fact]
        public async void AddHeaders()
        {
            var client = new RestClientMock();

            client.AddHeader("APP-TOKEN", "ABCDEFGHIJKLMNOPQRSTUWXYZ");

            var test = await client.GetRestAsync<bool>("test");

            Assert.NotNull(client.LastRequest);
            Assert.Equal(1, client.LastRequest?.Headers.Count() ?? 0);
            Assert.Contains(client.LastRequest.Headers, x => x.Key == "APP-TOKEN");
            Assert.Equal("ABCDEFGHIJKLMNOPQRSTUWXYZ", client.LastRequest?.Headers.GetValues("APP-TOKEN").FirstOrDefault());
        }
    }
}
