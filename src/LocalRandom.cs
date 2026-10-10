
using System.Text;

namespace BlackDigital
{
    /// <summary>
    /// Provides methods for generating random strings and numbers.
    /// </summary>
    public static class LocalRandom
    {
        private static readonly char[] upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] lowerChars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static readonly char[] numbers = "0123456789".ToCharArray();
        private static readonly char[] specialChars = "!@#$%^&*()_+-=[]{};':,.<>/?".ToCharArray();

        private static readonly Random random = new();

        /// <summary>
        /// Generates a random string of the specified length using the selected character sets.
        /// </summary>
        /// <param name="length">The length of the generated string.</param>
        /// <param name="useUpper">Whether to include uppercase letters.</param>
        /// <param name="useLower">Whether to include lowercase letters.</param>
        /// <param name="useNumbers">Whether to include numeric digits.</param>
        /// <param name="useSpecial">Whether to include special characters.</param>
        /// <returns>A random string.</returns>
        public static string GenerateString(int length, bool useUpper, bool useLower, bool useNumbers, bool useSpecial)
        {
            List<char> chars = [];
            StringBuilder stringBuilder = new(length);

            if (useUpper)
                chars.AddRange(upperChars);
            if (useLower)
                chars.AddRange(lowerChars);
            if (useNumbers)
                chars.AddRange(numbers);
            if (useSpecial)
                chars.AddRange(specialChars);
            
            for (var i = 0; i < length; i++)
                stringBuilder.Append(chars[random.Next(chars.Count)]);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generates a random numeric string of the specified length.
        /// </summary>
        /// <param name="length">The length of the generated string.</param>
        /// <returns>A random numeric string.</returns>
        public static string GenerateNumber(int length)
            => GenerateString(length, false, false, true, false);
    }
}
