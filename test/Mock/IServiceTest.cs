
using BlackDigital.Rest;

namespace BlackDigital.Test.Mock
{
    [Service("api/serviceTest")]
    public interface IServiceTest
    {
        [Action("{name}")]
        Task<ComplexModel> MyAction([FromRoute] string name,
                                    [FromHeader] Guid myGuid,
                                    [FromBody] SimpleModel model,
                                    [FromQuery] SimpleModel query);
    }
}
