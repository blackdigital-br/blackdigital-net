using BlackDigital.DataAnnotations;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BlackDigital.DataBuilder
{
    /// <summary>
    /// Provides metadata and access to a single property of a type, including data annotations and display information.
    /// </summary>
    public class PropertyBuilder
    {
        internal PropertyBuilder(TypeBuilder type, PropertyInfo property)
        {
            Type = type;
            Property = property;
        }

        /// <summary>
        /// The <see cref="TypeBuilder"/> that owns this property.
        /// </summary>
        protected readonly TypeBuilder Type;

        /// <summary>
        /// The <see cref="PropertyInfo"/> that this builder wraps.
        /// </summary>
        protected readonly PropertyInfo Property;

        /// <summary>
        /// Gets the <see cref="DisplayAttribute"/> applied to the property, if any.
        /// </summary>
        public DisplayAttribute? DisplayAttribute =>
            Property.GetSingleAttribute<DisplayAttribute>();

        /// <summary>
        /// Gets the <see cref="DataTypeAttribute"/> applied to the property, if any.
        /// </summary>
        public DataTypeAttribute? DataTypeAttribute =>
            Property.GetSingleAttribute<DataTypeAttribute>();

        /// <summary>
        /// Gets the <see cref="ShowAttribute"/> applied to the property, if any.
        /// </summary>
        public ShowAttribute? ShowAttribute =>
            Property.GetSingleAttribute<ShowAttribute>();

        /// <summary>
        /// Gets the <see cref="MaxLengthAttribute"/> applied to the property, if any.
        /// </summary>
        public MaxLengthAttribute? MaxLengthAttribute =>
            Property.GetSingleAttribute<MaxLengthAttribute>();

        /// <summary>
        /// Gets the <see cref="MinLengthAttribute"/> applied to the property, if any.
        /// </summary>
        public MinLengthAttribute? MinLengthAttribute =>
            Property.GetSingleAttribute<MinLengthAttribute>();

        /// <summary>
        /// Gets the <see cref="RangeAttribute"/> applied to the property, if any.
        /// </summary>
        public RangeAttribute? RangeAttribute =>
            Property.GetSingleAttribute<RangeAttribute>();

        /// <summary>
        /// Gets the <see cref="EditableAttribute"/> applied to the property, if any.
        /// </summary>
        public EditableAttribute? EditableAttribute =>
            Property.GetSingleAttribute<EditableAttribute>();

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string PropertyName => Property.Name;

        /// <summary>
        /// Gets the display order of the property.
        /// </summary>
        public int Order => DisplayAttribute?.GetOrder() ?? 10000 + Type.Properties.IndexOf(this);

        /// <summary>
        /// Gets the display name of the property.
        /// </summary>
        public string Name => DisplayAttribute?.GetName() ?? Property.Name;

        /// <summary>
        /// Gets the description of the property.
        /// </summary>
        public string Description => DisplayAttribute?.GetDescription() ?? string.Empty;

        /// <summary>
        /// Gets the prompt of the property.
        /// </summary>
        public string Prompt => DisplayAttribute?.GetPrompt() ?? string.Empty;

        /// <summary>
        /// Gets the component type name of the property.
        /// </summary>
        public string ComponentType => DataTypeAttribute?.GetDataTypeName()
                        ?? Enum.GetName(GetDataTypeFromPropertyType(Property.PropertyType))
                        ?? "";

        /// <summary>
        /// Gets the parsed <see cref="DataType"/> of the property, or <c>null</c> if it cannot be determined.
        /// </summary>
        public DataType? CurrentDataType
        {
            get
            {
                if (Enum.TryParse(ComponentType, out DataType dataType))
                    return dataType;

                return null;
            }
        }

        /// <summary>
        /// Gets the minimum length allowed for the property, if specified.
        /// </summary>
        public int? MinLength => MinLengthAttribute?.Length;

        /// <summary>
        /// Gets the maximum length allowed for the property, if specified.
        /// </summary>
        public int? MaxLength => MaxLengthAttribute?.Length;

        /// <summary>
        /// Gets a value indicating whether the property is editable.
        /// </summary>
        public bool AllowEdit => EditableAttribute?.AllowEdit ?? true;

        /// <summary>
        /// Gets the minimum allowed value for the property, if specified.
        /// </summary>
        public object? MinValue => RangeAttribute?.Minimum;

        /// <summary>
        /// Gets the maximum allowed value for the property, if specified.
        /// </summary>
        public object? MaxValue => RangeAttribute?.Maximum;

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        public Type PropertyType => Property.PropertyType;

        /// <summary>
        /// Determines whether the property should be shown for the specified value.
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        /// <returns><c>true</c> if the property should be shown; otherwise, <c>false</c>.</returns>
        public bool Show(object value) => ShowAttribute?.Show(value) ?? true;

        /// <summary>
        /// Gets the value of the property from the specified model.
        /// </summary>
        /// <param name="model">The model instance.</param>
        /// <returns>The value of the property.</returns>
        public object? GetValue(object? model) => Property.GetValue(model);

        /// <summary>
        /// Sets the value of the property on the specified model.
        /// </summary>
        /// <param name="model">The model instance.</param>
        /// <param name="value">The value to set.</param>
        public void SetValue(object? model, object? value) => Property.SetValue(model, value);

        private static DataType GetDataTypeFromPropertyType(Type type)
        {
            Type? useType = Nullable.GetUnderlyingType(type) ?? type;

            if (useType != typeof(string)
                && typeof(IEnumerable).IsAssignableFrom(useType))
                return DataType.List;

            if (useType.IsEnum)
                return DataType.Enumeration;

            if (DataTypeTable.DataTypes.Any(dt => dt.Value.Contains(useType)))
                return DataTypeTable.DataTypes.First(dt => dt.Value.Contains(useType)).Key;

            return DataType.Text;
        }
    }
}
