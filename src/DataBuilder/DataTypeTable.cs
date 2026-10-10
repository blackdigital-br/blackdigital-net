
namespace BlackDigital.DataBuilder
{
    /// <summary>
    /// Provides mappings between .NET types and <see cref="DataType"/> categories.
    /// </summary>
    public static class DataTypeTable
    {
        /// <summary>
        /// The .NET types that map to <see cref="DataType.Boolean"/>.
        /// </summary>
        public static readonly Type[] BooleansTypes = new Type[]
        {
            typeof(bool)
        };

        /// <summary>
        /// The .NET types that map to <see cref="DataType.Integer"/>.
        /// </summary>
        public static readonly Type[] IntegersType = new Type[]
        {
            typeof(sbyte),
            typeof(short),
            typeof(int),
            typeof(long)
        };

        /// <summary>
        /// The .NET types that map to <see cref="DataType.UnsignedInteger"/>.
        /// </summary>
        public static readonly Type[] UnsignedIntegersType = new Type[]
        {
            typeof(byte),
            typeof(ushort),
            typeof(uint),
            typeof(ulong)
        };

        /// <summary>
        /// The .NET types that map to <see cref="DataType.Decimal"/>.
        /// </summary>
        public static readonly Type[] DecimalsType = new Type[]
        {
            typeof(float),
            typeof(decimal),
            typeof(double)
        };

        /// <summary>
        /// The .NET types that map to <see cref="DataType.DateTime"/>.
        /// </summary>
        public static readonly Type[] DateTimeTypes = new Type[]
        {
            typeof(DateTime),
            typeof(DateTimeOffset),
        };

        /// <summary>
        /// The .NET types that map to <see cref="DataType.Date"/>.
        /// </summary>
        public static readonly Type[] DateTypes = new Type[]
        {
            typeof(DateOnly)
        };

        /// <summary>
        /// The .NET types that map to <see cref="DataType.Time"/>.
        /// </summary>
        public static readonly Type[] TimeTypes = new Type[]
        {
            typeof(TimeOnly)
        };

        /// <summary>
        /// The .NET types that map to <see cref="DataType.TimeSpan"/>.
        /// </summary>
        public static readonly Type[] TimeSpanTypes = new Type[]
        {
            typeof(TimeSpan)
        };

        /// <summary>
        /// The complete mapping of <see cref="DataType"/> values to their corresponding .NET types.
        /// </summary>
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
