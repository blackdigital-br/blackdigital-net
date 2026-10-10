
namespace BlackDigital.DataAnnotations
{
    /// <summary>
    /// Base attribute for determining whether a property should be shown, based on a value condition.
    /// </summary>
    public abstract class ShowAttribute : Attribute
    {
        /// <summary>
        /// When overridden in a derived class, determines whether the property should be shown for the specified value.
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        /// <returns><c>true</c> if the property should be shown; otherwise, <c>false</c>.</returns>
        public abstract bool Show(object value);
    }
}
