using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlackDigital.Converters
{
    /// <summary>
    /// A JSON converter for <see cref="TimeSpan"/> that uses the constant ("c") format for serialization.
    /// </summary>
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// Reads and converts the JSON to a <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">The serialization options.</param>
        /// <returns>The converted <see cref="TimeSpan"/>.</returns>
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString()
                ?? throw new JsonException("TimeSpan value cannot be null.");

            return TimeSpan.ParseExact(value, @"c", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Writes a <see cref="TimeSpan"/> as JSON using the constant ("c") format.
        /// </summary>
        /// <param name="writer">The JSON writer.</param>
        /// <param name="value">The <see cref="TimeSpan"/> value to write.</param>
        /// <param name="options">The serialization options.</param>
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("c"));
        }
    }
}
