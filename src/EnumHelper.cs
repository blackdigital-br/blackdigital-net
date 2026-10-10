namespace BlackDigital
{
    /// <summary>
    /// Provides extension methods for comparing enum values against other values of different types.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Determines whether the enum value equals the specified other enum value.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="value">The enum value to compare.</param>
        /// <param name="other">The other enum value to compare against.</param>
        /// <returns><c>true</c> if the values are equal; otherwise, <c>false</c>.</returns>
        public static bool Is<TEnum>(this TEnum value, TEnum other)
            where TEnum : struct, Enum
            => value.Equals(other);

        /// <summary>
        /// Determines whether the enum value does not equal the specified other enum value.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="value">The enum value to compare.</param>
        /// <param name="other">The other enum value to compare against.</param>
        /// <returns><c>true</c> if the values are not equal; otherwise, <c>false</c>.</returns>
        public static bool IsNot<TEnum>(this TEnum value, TEnum other)
            where TEnum : struct, Enum
            => !value.Equals(other);

        /// <summary>
        /// Determines whether the enum value equals the value represented by the specified integer.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="value">The enum value to compare.</param>
        /// <param name="other">The integer representation of the other value.</param>
        /// <returns><c>true</c> if the values are equal; otherwise, <c>false</c>.</returns>
        public static bool Is<TEnum>(this TEnum value, int other)
            where TEnum : struct, Enum
            => value.Equals(Enum.ToObject(typeof(TEnum), other));

        /// <summary>
        /// Determines whether the enum value does not equal the value represented by the specified integer.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="value">The enum value to compare.</param>
        /// <param name="other">The integer representation of the other value.</param>
        /// <returns><c>true</c> if the values are not equal; otherwise, <c>false</c>.</returns>
        public static bool IsNot<TEnum>(this TEnum value, int other)
            where TEnum : struct, Enum
            => !value.Equals(Enum.ToObject(typeof(TEnum), other));

        /// <summary>
        /// Determines whether the enum value equals the value parsed from the specified string.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="value">The enum value to compare.</param>
        /// <param name="other">The string representation of the other value.</param>
        /// <param name="ignoreCase">Whether to ignore case when parsing.</param>
        /// <returns><c>true</c> if the values are equal; otherwise, <c>false</c>.</returns>
        public static bool Is<TEnum>(this TEnum value, string other, bool ignoreCase = false)
            where TEnum : struct, Enum
            => Enum.TryParse<TEnum>(other, ignoreCase, out var parsedValue) && value.Equals(parsedValue);

        /// <summary>
        /// Determines whether the enum value does not equal the value parsed from the specified string.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="value">The enum value to compare.</param>
        /// <param name="other">The string representation of the other value.</param>
        /// <param name="ignoreCase">Whether to ignore case when parsing.</param>
        /// <returns><c>true</c> if the values are not equal; otherwise, <c>false</c>.</returns>
        public static bool IsNot<TEnum>(this TEnum value, string other, bool ignoreCase = false)
            where TEnum : struct, Enum
            => !Enum.TryParse<TEnum>(other, ignoreCase, out var parsedValue) || !value.Equals(parsedValue);
    }
}
