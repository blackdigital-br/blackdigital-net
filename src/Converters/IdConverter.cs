using System.Text.Json.Serialization;
using System.Text.Json;
using BlackDigital.Model;

namespace BlackDigital.Converters
{
    public class IdConverter : JsonConverter<Id>
    {
        public IdConverter() { }

        public IdConverter(bool legacy) 
        { 
            _legacy = legacy;
        }

        private readonly bool _legacy;

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
