using BlackDigital.Converters;
using System.Text.Json;
using System.Text;
using System.Buffers;
using BlackDigital.Model;

namespace BlackDigital.Test.Converters
{
    public class IdConverterTest
    {
        [Fact]
        public void ReadIdConverter()
        {
            var jsonReader = new Utf8JsonReader(Encoding.UTF8.GetBytes("\"12345678-1234-5678-1234-567890abcdef\""));
            var converter = new IdConverter();

            while (jsonReader.TokenType == JsonTokenType.None)
                if (!jsonReader.Read())
                    break;

            var result = converter.Read(ref jsonReader, typeof(Id), new JsonSerializerOptions());

            Assert.Equal(new Id("12345678-1234-5678-1234-567890abcdef"), result);
        }

        [Fact]
        public void WriteIdConverter()
        {
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);
            var converter = new IdConverter();

            var id = new Id("12345678-1234-5678-1234-567890abcdef");

            converter.Write(writer, id, new JsonSerializerOptions());
            writer.Flush();

            string json = Encoding.UTF8.GetString(buffer.WrittenSpan);

            Assert.Equal("\"12345678-1234-5678-1234-567890abcdef\"", json);
        }
    }
}
