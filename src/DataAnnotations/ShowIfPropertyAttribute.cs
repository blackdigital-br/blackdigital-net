using System.Reflection;

namespace BlackDigital.DataAnnotations
{
    /// <summary>
    /// Determines whether a property should be shown based on the value of another property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ShowIfPropertyAttribute : ShowAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowIfPropertyAttribute"/> class.
        /// </summary>
        /// <param name="otherProperty">The name of the other property to check.</param>
        /// <param name="otherPropertyValue">The expected value of the other property.</param>
        /// <param name="otherPropertyValues">Additional expected values of the other property.</param>
        public ShowIfPropertyAttribute(string otherProperty, object otherPropertyValue, params object[] otherPropertyValues)
        {
            OtherProperty = otherProperty;
            OtherPropertyValues = new(otherPropertyValues);
            IsInverted = false;

            OtherPropertyValues.Insert(0, otherPropertyValue);
        }

        /// <summary>
        /// Gets the name of the other property to check.
        /// </summary>
        public string OtherProperty { get; private set; }

        /// <summary>
        /// Gets the list of values that, when matched by the other property, make this property visible.
        /// </summary>
        public List<object> OtherPropertyValues { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the condition is inverted.
        /// </summary>
        public bool IsInverted { get; set; }

        /// <summary>
        /// Determines whether the property should be shown based on the other property value of the specified object.
        /// </summary>
        /// <param name="value">The object whose other property value will be checked.</param>
        /// <returns><c>true</c> if the property should be shown; otherwise, <c>false</c>.</returns>
        public override bool Show(object value)
        {
            if (value== null) return false;

            PropertyInfo? otherProperty = value.GetType().GetProperty(this.OtherProperty);
            if (otherProperty == null)
            {
                return false;
            }

            object? otherValue = otherProperty.GetValue(value);

            // check if this value is actually required and validate it
            if (!this.IsInverted && OtherPropertyValues.Any(val => object.Equals(otherValue, val)) ||
                this.IsInverted && !OtherPropertyValues.Any(val => object.Equals(otherValue, val)))
            {
                return true;
            }

            return false;
        }
    }
}
