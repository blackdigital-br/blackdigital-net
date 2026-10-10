using System.Text.Json.Serialization;
using System.Text.Json;
using BlackDigital.Model;

namespace BlackDigital.Converters
{
    /// <summary>
    /// A JSON converter for <see cref="Id"/> that reads and writes numeric values when possible, or falls back to strings.
    /// </summary>
    public class IdConverter : JsonConverter<Id>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdConverter"/> class.
        /// </summary>
        public IdConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdConverter"/> class, optionally using legacy (string-only) serialization.
        /// </summary>
        /// <param name="legacy">Whether to use legacy string-only serialization.</param>
        public IdConverter(bool legacy)
        {
            _legacy = legacy;
        }

        private readonly bool _legacy;

        /// <summary>
        /// Reads and converts the JSON to an <see cref="Id"/>.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">The serialization options.</param>
        /// <returns>The converted <see cref="Id"/>.</returns>
        public override Id Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!_legacy && reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt16(out short shortValue))
                    return new Id(shortValue);
                if (reader.TryGetUInt16(out ushort ushortValue))
                    return new Id(ushortValue);
                if (reader.TryGetInt32(out int intValue))
                    return new Id(intValue);
                if (reader.TryGetUInt32(out uint uintValue))
                    return new Id(uintValue);
                if (reader.TryGetInt64(out long longValue))
                    return new Id(longValue);
                if (reader.TryGetUInt64(out ulong ulongValue))
                    return new Id(ulongValue);
            }

            return new Id(reader.GetString());
        }

        /// <summary>
        /// Writes an <see cref="Id"/> as JSON, using a numeric value when possible or a string otherwise.
        /// </summary>
        /// <param name="writer">The JSON writer.</param>
        /// <param name="value">The <see cref="Id"/> value to write.</param>
        /// <param name="options">The serialization options.</param>
        public override void Write(Utf8JsonWriter writer, Id value, JsonSerializerOptions options)
        {
            if (!_legacy)
            {
                if (value.TryToInt() is int intValue)
                    writer.WriteNumberValue(intValue);
                else if (value.TryToUInt() is uint uintValue)
                    writer.WriteNumberValue(uintValue);
                else if (value.TryToLong() is long longValue)
                    writer.WriteNumberValue(longValue);
                else if (value.TryToULong() is ulong ulongValue)
                    writer.WriteNumberValue(ulongValue);
                else if (value.TryToShort() is short shortValue)
                    writer.WriteNumberValue(shortValue);
                else if (value.TryToUShort() is ushort ushortValue)
                    writer.WriteNumberValue(ushortValue);
                else
                    writer.WriteStringValue(value.ToString());
            }
            else
            {
                writer.WriteStringValue(value.ToString());
            }
        }

    }
}
