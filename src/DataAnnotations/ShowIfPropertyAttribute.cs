using System.Reflection;

namespace BlackDigital.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ShowIfPropertyAttribute : ShowAttribute
    {
        public ShowIfPropertyAttribute(string otherProperty, object otherPropertyValue, params object[] otherPropertyValues)
        {
            OtherProperty = otherProperty;
            OtherPropertyValues = new(otherPropertyValues);
            IsInverted = false;

            OtherPropertyValues.Insert(0, otherPropertyValue);
        }

        public string OtherProperty { get; private set; }

        public List<object> OtherPropertyValues { get; private set; }
        public bool IsInverted { get; set; }

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
