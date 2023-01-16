
namespace BlackDigital.DataAnnotations
{
    public abstract class ShowAttribute : Attribute
    {
        public abstract bool Show(object value);
    }
}
