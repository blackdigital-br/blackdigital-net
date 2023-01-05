
namespace BlackDigital.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotShow : ShowAttribute
    {
        public override bool Show(object value)
        {
            return false;
        }
    }
}
