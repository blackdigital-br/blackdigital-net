using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredIfAttribute<T> : ValidationAttribute
    {
        public RequiredIfAttribute(Func<T, bool> requiredValidation)
        {
            RequiredValidation = (T, validationContext) =>
            {
                if (requiredValidation(T))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            };
        }

        public RequiredIfAttribute(Func<T, ValidationContext, ValidationResult> requiredValidation)
        {
            RequiredValidation = requiredValidation;
        }

        public Func<T, ValidationContext, ValidationResult> RequiredValidation { get; protected set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (RequiredValidation == null)
                throw new ArgumentNullException("RequiredValidation");

            if (value is T)
                return RequiredValidation((T)value, validationContext);

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
