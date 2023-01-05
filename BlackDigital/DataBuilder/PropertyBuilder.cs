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

        private static readonly Type[] IntegersType = new Type[]
        {
            typeof(sbyte),
            typeof(short),
            typeof(int), 
            typeof(long)
        };

        private static readonly Type[] UnsignedIntegersType = new Type[]
        {
            typeof(byte),
            typeof(ushort),
            typeof(uint),
            typeof(ulong)
        };

        private static readonly Type[] DecimalsType = new Type[]
        {
            typeof(float),
            typeof(decimal),
            typeof(double)
        };

        private DisplayAttribute? DisplayAttribute =>
            GetSingleAttribute<DisplayAttribute>();

        private DataTypeAttribute? DataTypeAttribute =>
            GetSingleAttribute<DataTypeAttribute>();

        private ShowAttribute? ShowAttribute =>
            GetSingleAttribute<ShowAttribute>();

        private MaxLengthAttribute? MaxLengthAttribute =>
            GetSingleAttribute<MaxLengthAttribute>();
        
        private MinLengthAttribute? MinLengthAttribute =>
            GetSingleAttribute<MinLengthAttribute>();

        private RangeAttribute? RangeAttribute =>
            GetSingleAttribute<RangeAttribute>();

        private EditableAttribute? EditableAttribute => 
            GetSingleAttribute<EditableAttribute>();

        public int Order => DisplayAttribute?.Order ?? 10000 + Type.Properties.IndexOf(this);

        public string Name => DisplayAttribute?.Name ?? Property.Name;

        public string Description => DisplayAttribute?.Description ?? string.Empty;

        public string ComponentType => DataTypeAttribute?.GetDataTypeName() ?? GetDataTypeFromPropertyType(Property.PropertyType);

        public int? MinLength => MinLengthAttribute?.Length;

        public int? MaxLength => MaxLengthAttribute?.Length;

        public bool AllowEdit => EditableAttribute?.AllowEdit ?? true;

        public object? MinValue => RangeAttribute?.Minimum;

        public object? MaxValue => RangeAttribute?.Maximum;

        public bool Show(object value) => ShowAttribute?.Show(value) ?? true;

        public TAttribute? GetSingleAttribute<TAttribute>() 
            where TAttribute : Attribute =>
                Property.GetCustomAttributes(typeof(TAttribute), true)
                        .Cast<TAttribute>()
                        .SingleOrDefault();


        private static string GetDataTypeFromPropertyType(Type type)
        {
            if (type != typeof(string)
                && typeof(IEnumerable).IsAssignableFrom(type))
                return "List";

            if (IntegersType.Contains(type))
                return "Integer";

            if (UnsignedIntegersType.Contains(type))
                return "UnsignedInteger";

            if (DecimalsType.Contains(type))
                return "Decimal";

            return "Text";
        }
    }
}
