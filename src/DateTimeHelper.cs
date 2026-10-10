
using System.Globalization;

namespace BlackDigital
{
    /// <summary>
    /// Provides extension methods for working with <see cref="DateTime"/>, <see cref="DateTimeOffset"/>, and <see cref="TimeSpan"/> values.
    /// </summary>
    public static class DateTimeHelper
    {
        private const string FORMAT = "yyyy-MM-ddTHH:mm:ss.ffff";

        /// <summary>
        /// Returns a <see cref="DateTime"/> representing the first day of the month of the specified date.
        /// </summary>
        /// <param name="date">The date to process.</param>
        /// <returns>A <see cref="DateTime"/> set to the first day of the month.</returns>
        public static DateTime ToMonthDate(this DateTime date)
                    => new(date.Year, date.Month, 1);

        /// <summary>
        /// Returns a <see cref="DateTimeOffset"/> representing the first day of the month of the specified date.
        /// </summary>
        /// <param name="date">The date to process.</param>
        /// <returns>A <see cref="DateTimeOffset"/> set to the first day of the month.</returns>
        public static DateTimeOffset ToMonthDate(this DateTimeOffset date)
                    => new(date.Year, date.Month, 1, 0, 0, 0, TimeSpan.Zero);

        /// <summary>
        /// Tries to parse a string into a <see cref="DateTime"/> using the framework's default format.
        /// </summary>
        /// <param name="datetime">The string to parse.</param>
        /// <param name="result">When this method returns, contains the parsed <see cref="DateTime"/>.</param>
        /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
        public static bool TryToDateTime(this string datetime, out DateTime result)
        {
            var format = FORMAT[..datetime.Length];

            return DateTime.TryParseExact(datetime, 
                                          format, 
                                          null, 
                                          DateTimeStyles.None, 
                                          out result);
        }

        /// <summary>
        /// Converts a string to a <see cref="DateTime"/>, returning a default value if parsing fails.
        /// </summary>
        /// <param name="datetime">The string to parse.</param>
        /// <param name="defaultValue">The value to return if parsing fails.</param>
        /// <returns>The parsed <see cref="DateTime"/>, or <paramref name="defaultValue"/>.</returns>
        public static DateTime ToDateTime(this string datetime, DateTime defaultValue = default)
        {
            if (datetime.TryToDateTime(out DateTime result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Creates a new <see cref="DateTime"/> with the same components but with <see cref="DateTimeKind.Unspecified"/>.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <returns>A new <see cref="DateTime"/> with <see cref="DateTimeKind.Unspecified"/>.</returns>
        public static DateTime ToDateTimeUnspecified(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day,
                                date.Hour, date.Minute, date.Second,
                                date.Millisecond, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Deconstructs a <see cref="TimeSpan"/> into hours, minutes, and seconds.
        /// </summary>
        /// <param name="timespan">The time span to deconstruct.</param>
        /// <param name="hours">The hours component.</param>
        /// <param name="minutes">The minutes component.</param>
        /// <param name="seconds">The seconds component.</param>
        public static void Deconstruct(this TimeSpan timespan, out int hours, out int minutes, out int seconds) =>
            (hours, minutes, seconds) = (timespan.Hours, timespan.Minutes, timespan.Seconds);

        /// <summary>
        /// Deconstructs a <see cref="TimeSpan"/> into days, hours, minutes, and seconds.
        /// </summary>
        /// <param name="timespan">The time span to deconstruct.</param>
        /// <param name="days">The days component.</param>
        /// <param name="hours">The hours component.</param>
        /// <param name="minutes">The minutes component.</param>
        /// <param name="seconds">The seconds component.</param>
        public static void Deconstruct(this TimeSpan timespan, out int days, out int hours, out int minutes, out int seconds) =>
            (days, hours, minutes, seconds) = (timespan.Days, timespan.Hours, timespan.Minutes, timespan.Seconds);

        /// <summary>
        /// Deconstructs a <see cref="TimeSpan"/> into total hours (truncated), minutes, and seconds.
        /// </summary>
        /// <param name="timespan">The time span to deconstruct.</param>
        /// <param name="hours">The total hours, truncated.</param>
        /// <param name="minutes">The minutes component.</param>
        /// <param name="seconds">The seconds component.</param>
        public static void Deconstruct(this TimeSpan timespan, out double hours, out int minutes, out int seconds) =>
            (hours, minutes, seconds) = (Math.Truncate(timespan.TotalHours), timespan.Minutes, timespan.Seconds);
    }
}
