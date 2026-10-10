using System.ComponentModel.DataAnnotations;

namespace BlackDigital.DataAnnotations
{
    /// <summary>
    /// Validates that a property value is a valid mobile phone number.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MobileAttribute : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value is a valid mobile phone number.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns><c>true</c> if the value is a valid phone number; otherwise, <c>false</c>.</returns>
        public override bool IsValid(object? value)
            => DataValidator.IsPhone(value?.ToString() ?? string.Empty);
    }
}
