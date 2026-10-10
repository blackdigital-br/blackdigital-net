
namespace BlackDigital.Rest
{
    /// <summary>
    /// Marks a method parameter as the body of the REST request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class BodyAttribute : Attribute
    {
    }
}
