
namespace BlackDigital.Test.Mock
{
    public interface ITest
    {
        Task<ComplexModel> MyFunction(string name, Guid myGuid, SimpleModel model);
    }
}
