using System.ComponentModel.DataAnnotations;

namespace BlackDigital.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EmailMobileAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
            => DataValidator.IsEmailOrPhone(value?.ToString() ?? string.Empty);
    }
}
