
namespace BlackDigital.DataAnnotations
{
    /// <summary>
    /// Determines whether a property should be shown based on a custom condition evaluated against a typed value.
    /// </summary>
    /// <typeparam name="T">The type of the value to evaluate.</typeparam>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ShowIfAttribute<T> : ShowAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowIfAttribute{T}"/> class with the specified condition.
        /// </summary>
        /// <param name="showValidation">A function that returns <c>true</c> when the property should be shown.</param>
        public ShowIfAttribute(Func<T, bool> showValidation)
        {
            ShowValidation = showValidation;
        }

        /// <summary>
        /// Gets or sets the function that determines whether the property should be shown.
        /// </summary>
        public Func<T, bool> ShowValidation { get; protected set; }

        /// <summary>
        /// Determines whether the property should be shown for the specified value.
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        /// <returns><c>true</c> if the property should be shown; otherwise, <c>false</c>.</returns>
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
