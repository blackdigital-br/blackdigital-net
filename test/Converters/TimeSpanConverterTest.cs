
using BlackDigital.Converters;
using System.Buffers;
using System.Text.Json;
using System.Text;

namespace BlackDigital.Test.Converters
{
    public class TimeSpanConverterTest
    {
        [Fact]
        public void ReadTimespanConverter()
        {
            var timespan = new TimeSpan(1, 12, 33, 12, 555);
            var jsonReader = new Utf8JsonReader(Encoding.UTF8.GetBytes("\"1.12:33:12.555\""));
            var converter = new TimeSpanConverter();

            while (jsonReader.TokenType == JsonTokenType.None)
                if (!jsonReader.Read())
                    break;

            var result = converter.Read(ref jsonReader, typeof(TimeSpan), new JsonSerializerOptions());

            Assert.Equal(timespan, result);
        }

        [Fact]
        public void WriteTimespanConverter()
        {
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);
            var converter = new TimeSpanConverter();

            var timespan = new TimeSpan(1, 12, 33, 12, 555);

            converter.Write(writer, timespan, new JsonSerializerOptions());
            writer.Flush();

            string json = Encoding.UTF8.GetString(buffer.WrittenSpan);

            Assert.Equal("\"1.12:33:12.5550000\"", json);
        }
    }
}
