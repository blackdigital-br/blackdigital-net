using BlackDigital.Rest;
using BlackDigital.Test.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Test.Rest
{
    public class RestServiceTest
    {
        [Fact]
        public async void ExecuteRequest()
        {

            var builder = new RestServiceBuilder();

            builder.AddService<ITest>();

            var services = builder.Build();
            var type = services.FirstOrDefault().Value;

            var test = (ITest)Activator.CreateInstance(type, new RestClient("https://blackdigital.com.br"));

            var retorno = await test.MyFunction("Nome", Guid.NewGuid(), new SimpleModel()
            {
                Description = "Descrição",
                HttpStatus = System.Net.HttpStatusCode.OK,
                Name = "Nome",
                Value = 21
            });
        }
    }
}
