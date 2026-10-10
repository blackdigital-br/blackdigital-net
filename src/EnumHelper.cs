namespace BlackDigital
{
    public static class EnumHelper
    {
        public static bool Is<TEnum>(this TEnum value, TEnum other)
            where TEnum : struct, Enum
            => value.Equals(other);

        public static bool IsNot<TEnum>(this TEnum value, TEnum other)
            where TEnum : struct, Enum
            => !value.Equals(other);

        public static bool Is<TEnum>(this TEnum value, int other)
            where TEnum : struct, Enum
            => value.Equals(Enum.ToObject(typeof(TEnum), other));

        public static bool IsNot<TEnum>(this TEnum value, int other)
            where TEnum : struct, Enum
            => !value.Equals(Enum.ToObject(typeof(TEnum), other));

        public static bool Is<TEnum>(this TEnum value, string other, bool ignoreCase = false)
            where TEnum : struct, Enum
            => Enum.TryParse<TEnum>(other, ignoreCase, out var parsedValue) && value.Equals(parsedValue);

        public static bool IsNot<TEnum>(this TEnum value, string other, bool ignoreCase = false)
            where TEnum : struct, Enum
            => !Enum.TryParse<TEnum>(other, ignoreCase, out var parsedValue) || !value.Equals(parsedValue);
    }
}
