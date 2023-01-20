using BlackDigital.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.Test.Mock
{
    public class TestBaseService : BaseService<ITest>, ITest
    {
        public TestBaseService(RestClient client) : base(client)
        {
        }

        public Task<ComplexModel> MyFunction(string name, Guid myGuid, SimpleModel model)
        {
            Dictionary<string, object> arguments = new();

            arguments.Add("name", name);
            arguments.Add("myGuid", myGuid);
            //arguments.Add("model", model);

            //return ExecuteRequest<ComplexModel>("MyFunction", arguments);
            return null;
        }
    }
}
