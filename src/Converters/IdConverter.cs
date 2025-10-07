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

        private bool _legacy = false;

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
                if (value.TryToInt() != null)
                    writer.WriteNumberValue(value.TryToInt().Value);
                else if (value.TryToUInt() != null)
                    writer.WriteNumberValue(value.TryToUInt().Value);
                else if (value.TryToLong() != null)
                    writer.WriteNumberValue(value.TryToLong().Value);
                else if (value.TryToULong() != null)
                    writer.WriteNumberValue(value.TryToULong().Value);
                else if (value.TryToShort() != null)
                    writer.WriteNumberValue(value.TryToShort().Value);
                else if (value.TryToUShort() != null)
                    writer.WriteNumberValue(value.TryToUShort().Value);
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
