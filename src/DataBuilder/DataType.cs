
namespace BlackDigital.DataBuilder
{
    /// <summary>
    /// Represents the category of a property's data type for UI and metadata purposes.
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// A boolean value.
        /// </summary>
        Boolean,
        /// <summary>
        /// A signed integer value.
        /// </summary>
        Integer,
        /// <summary>
        /// An unsigned integer value.
        /// </summary>
        UnsignedInteger,
        /// <summary>
        /// A decimal or floating-point value.
        /// </summary>
        Decimal,
        /// <summary>
        /// A date and time value.
        /// </summary>
        DateTime,
        /// <summary>
        /// A date-only value.
        /// </summary>
        Date,
        /// <summary>
        /// A time-only value.
        /// </summary>
        Time,
        /// <summary>
        /// A time interval value.
        /// </summary>
        TimeSpan,
        /// <summary>
        /// A text value.
        /// </summary>
        Text = 1000,
        /// <summary>
        /// An enumeration value.
        /// </summary>
        Enumeration = 2000,
        /// <summary>
        /// A list or collection value.
        /// </summary>
        List = 10000,
        /// <summary>
        /// A dictionary value.
        /// </summary>
        Dictonary,
        /// <summary>
        /// A complex data value.
        /// </summary>
        ComplexData = Int32.MaxValue
    }
}
