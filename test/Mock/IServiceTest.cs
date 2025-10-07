
using BlackDigital.Rest;

namespace BlackDigital.Test.Mock
{
    [Service("api/serviceTest", Version = "2025-10-07")]
    public interface IServiceTest
    {
        [Action("{name}")]
        Task<ComplexModel> MyAction([Path] string name,
                                    [Header] Guid myGuid,
                                    [Body] SimpleModel model,
                                    [Query] SimpleModel query);
    }
}
