﻿using BlackDigital.Rest;
using BlackDigital.Test.Mock;
using System.Net.Http.Headers;

namespace BlackDigital.Test.Rest
{
    public class RestClientTest
    {
        [Fact]
        public async void AddAuthentication()
        {
            var client = new RestClientMock();

            client.AddAuthentication(new AuthenticationHeaderValue("bearer", "ABCDEFGHIJKLMNOPQRSTUWXYZ"));

            var test = await client.GetRestAsync<bool>("test", null, RestThownType.None);

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

            var test = await client.GetRestAsync<bool>("test", null, RestThownType.None);

            Assert.NotNull(client.LastRequest);
            Assert.Equal(1, client.LastRequest?.Headers.Count() ?? 0);
            Assert.Contains(client.LastRequest.Headers, x => x.Key == "APP-TOKEN");
            Assert.Equal("ABCDEFGHIJKLMNOPQRSTUWXYZ", client.LastRequest?.Headers.GetValues("APP-TOKEN").FirstOrDefault());
        }
    }
}
