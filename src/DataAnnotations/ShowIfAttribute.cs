
namespace BlackDigital.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ShowIfAttribute<T> : ShowAttribute
    {
        public ShowIfAttribute(Func<T, bool> showValidation)
        {
            ShowValidation = showValidation;
        }

        public Func<T, bool> ShowValidation { get; protected set; }

        public override bool Show(object value)
        {
            if (value is T t)
            {
                return ShowValidation(t);
            }

            return false;
        }
    }
}
