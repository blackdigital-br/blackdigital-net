
namespace BlackDigital.DataBuilder
{
    public enum DataType
    {
        Boolean,
        Integer,
        UnsignedInteger,
        Decimal,
        DateTime,
        Date,
        Time,
        TimeSpan,
        Text = 1000,
        Enumeration = 2000,
        List = 10000,
        Dictonary,
        ComplexData = Int32.MaxValue
    }
}
