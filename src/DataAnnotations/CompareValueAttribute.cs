using System.ComponentModel.DataAnnotations;

namespace BlackDigital.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CompareValueAttribute(string otherPropertyName, Symbol symbol = Symbol.Equal) : ValidationAttribute
    {
        private readonly string _otherPropertyName = otherPropertyName;
        private readonly Symbol _symbol = symbol;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

            if (otherPropertyInfo == null)
                throw new ArgumentException($"Property {_otherPropertyName} not found.");

            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

            if (DataValidator.ValidateData(value, otherValue, _symbol))
                return ValidationResult.Success;

            var errorMessage = FormatErrorMessage(validationContext.DisplayName);
            return new ValidationResult(errorMessage, [validationContext.MemberName]);
        }
    }
}
