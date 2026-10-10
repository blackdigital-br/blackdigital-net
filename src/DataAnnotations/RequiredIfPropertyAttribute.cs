using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace BlackDigital.DataAnnotations
{
    /// <summary>
    /// Validates that a property is required when another property has one of the specified values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredIfPropertyAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfPropertyAttribute"/> class.
        /// </summary>
        /// <param name="otherProperty">The name of the other property to check.</param>
        /// <param name="otherPropertyValue">The expected value of the other property.</param>
        /// <param name="otherPropertyValues">Additional expected values of the other property.</param>
        public RequiredIfPropertyAttribute(string otherProperty, object otherPropertyValue, params object[] otherPropertyValues)
        : base("'{0}' is required")
        {
            this.OtherProperty = otherProperty;
            this.OtherPropertyValues = new(otherPropertyValues);
            this.IsInverted = false;

            OtherPropertyValues.Insert(0, otherPropertyValue);
        }

        /// <summary>
        /// Gets the name of the other property to check.
        /// </summary>
        public string OtherProperty { get; private set; }

        /// <summary>
        /// Gets the list of values that, when matched by the other property, make this property required.
        /// </summary>
        public List<object> OtherPropertyValues { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the condition is inverted.
        /// </summary>
        public bool IsInverted { get; set; }

        /// <summary>
        /// Gets a value indicating that this attribute requires a validation context.
        /// </summary>
        public override bool RequiresValidationContext
        {
            get { return true; }
        }

        /// <summary>
        /// Validates that the property has a value when the other property matches one of the expected values.
        /// </summary>
        /// <param name="value">The value of the property being validated.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating success or failure.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (otherProperty == null)
            {
                return new ValidationResult(
                    string.Format(CultureInfo.CurrentCulture, "Could not find a property named '{0}'.", this.OtherProperty));
            }

            object? otherValue = otherProperty.GetValue(validationContext.ObjectInstance);

            // check if this value is actually required and validate it
            if (!this.IsInverted && OtherPropertyValues.Any(val => object.Equals(otherValue, val)) ||
                this.IsInverted && !OtherPropertyValues.Any(val => object.Equals(otherValue, val)))
            {
                if (value == null)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }

                // additional check for strings so they're not empty
                string? val = value as string;
                if (val != null && val.Trim().Length == 0)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }
}
