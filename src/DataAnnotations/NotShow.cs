
namespace BlackDigital.DataAnnotations
{
    /// <summary>
    /// Specifies that a property should never be shown.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotShow : ShowAttribute
    {
        /// <summary>
        /// Always returns <c>false</c>, indicating the property should not be shown.
        /// </summary>
        /// <param name="value">The value to evaluate (ignored).</param>
        /// <returns>Always <c>false</c>.</returns>
        public override bool Show(object value)
        {
            return false;
        }
    }
}
