
using BlackDigital.Rest;

namespace BlackDigital.Test.Mock
{
    [Service("api/serviceTest")]
    public interface IServiceTest
    {
        [Action("{name}")]
        Task<ComplexModel> MyAction([Route] string name,
                                    [Header] Guid myGuid,
                                    [Body] SimpleModel model,
                                    [Query] SimpleModel query);
    }
}
