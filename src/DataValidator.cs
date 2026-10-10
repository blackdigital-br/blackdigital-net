using System.Text.RegularExpressions;

namespace BlackDigital
{
    /// <summary>
    /// Provides validation methods for common data formats and value comparison.
    /// </summary>
    public static partial class DataValidator
    {
        /// <summary>
        /// Gets the regular expression used to validate email addresses.
        /// </summary>
        /// <returns>A <see cref="Regex"/> for email validation.</returns>
        [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        public static partial Regex Email();

        /// <summary>
        /// Gets the regular expression used to validate phone numbers.
        /// </summary>
        /// <returns>A <see cref="Regex"/> for phone validation.</returns>
        [GeneratedRegex(@"^(\+\d{1,3})?\d{8,15}$")]
        public static partial Regex Phone();

        /// <summary>
        /// Determines whether the specified string is a valid email address.
        /// </summary>
        /// <param name="email">The string to validate.</param>
        /// <returns><c>true</c> if the string is a valid email; otherwise, <c>false</c>.</returns>
        public static bool IsEmail(string email)
            => Email().IsMatch(email);

        /// <summary>
        /// Determines whether the specified string is a valid phone number.
        /// </summary>
        /// <param name="phone">The string to validate.</param>
        /// <returns><c>true</c> if the string is a valid phone number; otherwise, <c>false</c>.</returns>
        public static bool IsPhone(string phone)
            => Phone().IsMatch(phone);

        /// <summary>
        /// Determines whether the specified string is a valid email address or phone number.
        /// </summary>
        /// <param name="emailOrPhone">The string to validate.</param>
        /// <returns><c>true</c> if the string is a valid email or phone number; otherwise, <c>false</c>.</returns>
        public static bool IsEmailOrPhone(string emailOrPhone)
            => IsEmail(emailOrPhone) || IsPhone(emailOrPhone);

        /// <summary>
        /// Compares two values using the specified comparison operator.
        /// </summary>
        /// <param name="value">The first value.</param>
        /// <param name="otherValue">The second value.</param>
        /// <param name="symbol">The comparison operator.</param>
        /// <returns><c>true</c> if the comparison holds; otherwise, <c>false</c>.</returns>
        public static bool ValidateData(object? value, object? otherValue, Symbol symbol)
        {
            if (value == null && otherValue == null)
                return true;

            if (value == null || otherValue == null)
                return false;

            return symbol switch
            {
                Symbol.Equal => value.Equals(otherValue),
                Symbol.NotEqual => !value.Equals(otherValue),
                Symbol.LessThan => value.GetHashCode() < otherValue.GetHashCode(),
                Symbol.LessThanOrEqual => value.GetHashCode() <= otherValue.GetHashCode(),
                Symbol.GreaterThan => value.GetHashCode() > otherValue.GetHashCode(),
                Symbol.GreaterThanOrEqual => value.GetHashCode() >= otherValue.GetHashCode(),
                _ => false,
            };
        }
    }
}
