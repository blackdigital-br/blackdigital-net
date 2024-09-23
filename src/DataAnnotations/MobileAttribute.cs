using System.ComponentModel.DataAnnotations;

namespace BlackDigital.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MobileAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
            => DataValidator.IsPhone(value?.ToString() ?? string.Empty);
    }
}
