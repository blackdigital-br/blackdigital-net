using BlackDigital.Rest;
using BlackDigital.Test.Mock;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlackDigital.Test.Rest
{
    public class RestServiceTest
    {
        [Fact]
        public async void ExecuteServiceFromBuilderRest()
        {
            var builder = new RestServiceBuilder();

            builder.AddService<IServiceTest>();

            var services = builder.Build();
            var type = services.FirstOrDefault().Value;

            var restClient = new RestClientMock();

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

            var myService = (IServiceTest)Activator.CreateInstance(type, restClient);

            var result = await myService.MyAction("Nome", Guid.NewGuid(), new SimpleModel()
            {
                Description = "Descrição",
                HttpStatus = System.Net.HttpStatusCode.OK,
                Name = "Nome",
                Value = 21
            },
            new()
            {
                Description = null,
                HttpStatus = System.Net.HttpStatusCode.OK,
                Name = null,
                Value = 21
            });

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
