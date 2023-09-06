
using BlackDigital.Rest;
using BlackDigital.Test.Mock;
using System.Net;

namespace BlackDigital.Test.Rest
{
    public class RestServiceTest
    {
        [Fact]
        public async void ExecuteServiceRest()
        {
            var restClient = new RestClientMock();
            var restService = new RestService<IServiceTest>(restClient);

            var response = new ComplexModel()
            {
                Id = 15,
                Name = "Name",
                Number = 45,
                List = new()
                {
                    new()
                    {
                        Description = "MyDescription",
                        HttpStatus = System.Net.HttpStatusCode.OK,
                        Name = "Nome",
                        Value = 21
                    }
                }
            };

            restClient.Handler.AddResponse("http://blackdigital.com.br/api/serviceTest/Nome?Value=21&HttpStatus=OK", new HttpResponseMessage()
            {
                Content = new StringContent(response.ToJson()),
                StatusCode = HttpStatusCode.OK
            });

            var p1 = "Nome";
            var p2 = Guid.NewGuid();
            var p3 = new SimpleModel()
            {
                Description = "Descrição",
                HttpStatus = System.Net.HttpStatusCode.OK,
                Name = "Nome",
                Value = 21
            };
            var p4 = new SimpleModel()
            {
                Description = null,
                HttpStatus = System.Net.HttpStatusCode.OK,
                Name = null,
                Value = 21
            };

            var result = await restService.CallAsync(s => s.MyAction(p1, p2, p3, p4));

            Assert.NotNull(result);
            Assert.NotNull(restClient.LastRequest.Headers.GetValues("myGuid").FirstOrDefault());
            Assert.Equal(HttpMethod.Get, restClient.LastRequest.Method);
            Assert.Equal("http://blackdigital.com.br/api/serviceTest/Nome?Value=21&HttpStatus=OK", restClient.LastRequest.RequestUri.ToString());
            Assert.Equal(HttpStatusCode.OK, restClient.LastResponse.StatusCode);
            Assert.Equal(15, result.Id);
            Assert.Equal("Name", result.Name);
            Assert.Equal(45, result.Number);
            Assert.Equal(1, result.List.Count);
            Assert.Equal("MyDescription", result.List.First().Description);
        }
    }
}
