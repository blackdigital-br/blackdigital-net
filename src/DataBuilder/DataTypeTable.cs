
namespace BlackDigital.DataBuilder
{
    public static class DataTypeTable
    {
        public static readonly Type[] BooleansTypes = new Type[] 
        { 
            typeof(bool) 
        };

        public static readonly Type[] IntegersType = new Type[]
        {
            typeof(sbyte),
            typeof(short),
            typeof(int),
            typeof(long)
        };

        public static readonly Type[] UnsignedIntegersType = new Type[]
        {
            typeof(byte),
            typeof(ushort),
            typeof(uint),
            typeof(ulong)
        };

        public static readonly Type[] DecimalsType = new Type[]
        {
            typeof(float),
            typeof(decimal),
            typeof(double)
        };

        public static readonly Type[] DateTimeTypes = new Type[]
        {
            typeof(DateTime),
            typeof(DateTimeOffset),
        };

        public static readonly Type[] DateTypes = new Type[]
        {
            typeof(DateOnly)
        };

        public static readonly Type[] TimeTypes = new Type[]
        {
            typeof(TimeOnly)
        };

        public static readonly Type[] TimeSpanTypes = new Type[]
        {
            typeof(TimeSpan)
        };

        public static readonly Dictionary<DataType, Type[]> DataTypes = new()
        {
            { DataType.Boolean, BooleansTypes },
            { DataType.Integer, IntegersType },
            { DataType.UnsignedInteger, UnsignedIntegersType },
            { DataType.Decimal, DecimalsType },
            { DataType.DateTime, DateTimeTypes },
            { DataType.Date, DateTypes },
            { DataType.Time, TimeTypes },
            { DataType.TimeSpan, TimeSpanTypes }
        };
    }
}
