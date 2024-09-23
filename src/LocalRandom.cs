
using System.Text;

namespace BlackDigital
{
    public static class LocalRandom
    {
        private static readonly char[] upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] lowerChars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static readonly char[] numbers = "0123456789".ToCharArray();
        private static readonly char[] specialChars = "!@#$%^&*()_+-=[]{};':,.<>/?".ToCharArray();

        private static readonly Random random = new();

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

        public static string GenerateNumber(int length)
            => GenerateString(length, false, false, true, false);
    }
}
