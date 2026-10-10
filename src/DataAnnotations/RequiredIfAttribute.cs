using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackDigital.DataAnnotations
{
    /// <summary>
    /// Validates that a property value is required based on a custom condition evaluated against the value.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredIfAttribute<T> : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttribute{T}"/> class with a predicate that determines whether the value is required.
        /// </summary>
        /// <param name="requiredValidation">A function that returns <c>true</c> when the value satisfies the required condition.</param>
        public RequiredIfAttribute(Func<T, bool> requiredValidation)
        {
            RequiredValidation = (T, validationContext) =>
            {
                if (requiredValidation(T))
                {
                    return ValidationResult.Success ?? new(null);
                }
                else
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttribute{T}"/> class with a custom validation function.
        /// </summary>
        /// <param name="requiredValidation">A function that returns a <see cref="ValidationResult"/> based on the value and validation context.</param>
        public RequiredIfAttribute(Func<T, ValidationContext, ValidationResult> requiredValidation)
        {
            RequiredValidation = requiredValidation;
        }

        /// <summary>
        /// Gets or sets the validation function used to determine whether the value is required.
        /// </summary>
        public Func<T, ValidationContext, ValidationResult> RequiredValidation { get; protected set; }

        /// <summary>
        /// Validates the specified value using the configured validation function.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating success or failure.</returns>
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
