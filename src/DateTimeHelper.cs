
using System.Globalization;

namespace BlackDigital
{
    public static class DateTimeHelper
    {
        private const string FORMAT = "yyyy-MM-ddTHH:mm:ss.ffff";

        public static DateTime ToMonthDate(this DateTime date)
                    => new(date.Year, date.Month, 1);

        public static DateTimeOffset ToMonthDate(this DateTimeOffset date)
                    => new(date.Year, date.Month, 1, 0, 0, 0, TimeSpan.Zero);


        public static bool TryToDateTime(this string datetime, out DateTime result)
        {
            var format = FORMAT[..datetime.Length];

            return DateTime.TryParseExact(datetime, 
                                          format, 
                                          null, 
                                          DateTimeStyles.None, 
                                          out result);
        }

        public static DateTime ToDateTime(this string datetime, DateTime defaultValue = default)
        {
            if (datetime.TryToDateTime(out DateTime result))
                return result;

            return defaultValue;
        }

        public static DateTime ToDateTimeUnspecified(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day,
                                date.Hour, date.Minute, date.Second,
                                date.Millisecond, DateTimeKind.Unspecified);
        }

        public static void Deconstruct(this TimeSpan timespan, out int hours, out int minutes, out int seconds) =>
            (hours, minutes, seconds) = (timespan.Hours, timespan.Minutes, timespan.Seconds);

        public static void Deconstruct(this TimeSpan timespan, out int days, out int hours, out int minutes, out int seconds) =>
            (days, hours, minutes, seconds) = (timespan.Days, timespan.Hours, timespan.Minutes, timespan.Seconds);

        public static void Deconstruct(this TimeSpan timespan, out double hours, out int minutes, out int seconds) =>
            (hours, minutes, seconds) = (Math.Truncate(timespan.TotalHours), timespan.Minutes, timespan.Seconds);
    }
}
