using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace BlackDigital.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredIfPropertyAttribute : ValidationAttribute
    {
        public RequiredIfPropertyAttribute(string otherProperty, object otherPropertyValue, params object[] otherPropertyValues)
        : base("'{0}' is required")
        {
            this.OtherProperty = otherProperty;
            this.OtherPropertyValues = new(otherPropertyValues);
            this.IsInverted = false;

            OtherPropertyValues.Insert(0, otherPropertyValue);
        }

        public string OtherProperty { get; private set; }

        public List<object> OtherPropertyValues { get; private set; }

        public bool IsInverted { get; set; }

        public override bool RequiresValidationContext
        {
            get { return true; }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException("validationContext");
            }

            PropertyInfo otherProperty = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (otherProperty == null)
            {
                return new ValidationResult(
                    string.Format(CultureInfo.CurrentCulture, "Could not find a property named '{0}'.", this.OtherProperty));
            }

            object otherValue = otherProperty.GetValue(validationContext.ObjectInstance);

            // check if this value is actually required and validate it
            if (!this.IsInverted && OtherPropertyValues.Any(val => object.Equals(otherValue, val)) ||
                this.IsInverted && !OtherPropertyValues.Any(val => object.Equals(otherValue, val)))
            {
                if (value == null)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }

                // additional check for strings so they're not empty
                string val = value as string;
                if (val != null && val.Trim().Length == 0)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }
}
