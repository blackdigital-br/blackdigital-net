using System.ComponentModel.DataAnnotations;

namespace BlackDigital.DataAnnotations
{
    /// <summary>
    /// Validates that a property value satisfies a comparison against another property value, using the specified <see cref="Symbol"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CompareValueAttribute(string otherPropertyName, Symbol symbol = Symbol.Equal) : ValidationAttribute
    {
        private readonly string _otherPropertyName = otherPropertyName;
        private readonly Symbol _symbol = symbol;

        /// <summary>
        /// Validates the specified value against the other property value.
        /// </summary>
        /// <param name="value">The value of the property being validated.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating success or failure.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

            if (otherPropertyInfo == null)
                throw new ArgumentException($"Property {_otherPropertyName} not found.");

            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

            if (DataValidator.ValidateData(value, otherValue, _symbol))
                return ValidationResult.Success;

            var errorMessage = FormatErrorMessage(validationContext.DisplayName);
            var memberNames = validationContext.MemberName is null
                ? Array.Empty<string>()
                : new[] { validationContext.MemberName };

            return new ValidationResult(errorMessage, memberNames);
        }
    }
}
