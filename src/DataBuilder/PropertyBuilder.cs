using BlackDigital.DataAnnotations;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BlackDigital.DataBuilder
{
    public class PropertyBuilder
    {
        internal PropertyBuilder(TypeBuilder type, PropertyInfo property) 
        {
            Type = type;
            Property = property;
        }

        protected readonly TypeBuilder Type;
        
        protected readonly PropertyInfo Property;

        public DisplayAttribute? DisplayAttribute =>
            GetSingleAttribute<DisplayAttribute>();

        public DataTypeAttribute? DataTypeAttribute =>
            GetSingleAttribute<DataTypeAttribute>();

        public ShowAttribute? ShowAttribute =>
            GetSingleAttribute<ShowAttribute>();

        public MaxLengthAttribute? MaxLengthAttribute =>
            GetSingleAttribute<MaxLengthAttribute>();

        public MinLengthAttribute? MinLengthAttribute =>
            GetSingleAttribute<MinLengthAttribute>();

        public RangeAttribute? RangeAttribute =>
            GetSingleAttribute<RangeAttribute>();

        public EditableAttribute? EditableAttribute => 
            GetSingleAttribute<EditableAttribute>();

        public string PropertyName => Property.Name;

        public int Order => DisplayAttribute?.GetOrder() ?? 10000 + Type.Properties.IndexOf(this);

        public string Name => DisplayAttribute?.GetName() ?? Property.Name;

        public string Description => DisplayAttribute?.GetDescription() ?? string.Empty;

        public string Prompt => DisplayAttribute?.GetPrompt() ?? string.Empty;

        public string ComponentType => DataTypeAttribute?.GetDataTypeName() ?? GetDataTypeFromPropertyType(Property.PropertyType);

        public int? MinLength => MinLengthAttribute?.Length;

        public int? MaxLength => MaxLengthAttribute?.Length;

        public bool AllowEdit => EditableAttribute?.AllowEdit ?? true;

        public object? MinValue => RangeAttribute?.Minimum;

        public object? MaxValue => RangeAttribute?.Maximum;

        public Type PropertyType => Property.PropertyType;

        public bool Show(object value) => ShowAttribute?.Show(value) ?? true;

        public TAttribute? GetSingleAttribute<TAttribute>() 
            where TAttribute : Attribute =>
                Property.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();

        public object? GetValue(object? model) => Property.GetValue(model);

        public void SetValue(object? model, object? value) => Property.SetValue(model, value);

        private static string GetDataTypeFromPropertyType(Type type)
        {
            Type? useType = Nullable.GetUnderlyingType(type);

            if (useType == null)
                useType = type;

            if (useType != typeof(string)
                && typeof(IEnumerable).IsAssignableFrom(useType))
                return "List";

            if (useType.IsEnum)
                return "Enumeration";

            if (DataTypeTable.DataType.Any(dt => dt.Value.Contains(useType)))
                return DataTypeTable.DataType.First(dt => dt.Value.Contains(useType)).Key;

            return "Text";
        }
    }
}
