using System.Text.RegularExpressions;

namespace BlackDigital
{
    public static partial class DataValidator
    {
        [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        public static partial Regex Email();

        [GeneratedRegex(@"^(\+\d{1,3})?\d{8,15}$")]
        public static partial Regex Phone();

        public static bool IsEmail(string email)
            => Email().IsMatch(email);

        public static bool IsPhone(string phone)
            => Phone().IsMatch(phone);

        public static bool IsEmailOrPhone(string emailOrPhone)
            => IsEmail(emailOrPhone) || IsPhone(emailOrPhone);

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
