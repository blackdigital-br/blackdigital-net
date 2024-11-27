using BlackDigital.Converters;
using System.Text.Json;
using System.Text;
using System.Buffers;
using BlackDigital.Model;

namespace BlackDigital.Test.Converters
{
    public class OptionsConverterTest
    {
        [Fact]
        public void ReadOptionsConverter()
        {
            var json = """
                [
                    {
                        "Id": "1",
                        "Label": "Item 1"
                    },
                    {
                        "Id": "2",
                        "Label": "Item 2"
                    },
                    {
                        "Id": "3",
                        "Label": "Item 3"
                    },
                    {
                        "Id": "4",
                        "Label": "Item 4"
                    }
                ]
                """;

            var jsonReader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
            var converter = new OptionsConverter();

            while (jsonReader.TokenType == JsonTokenType.None)
                if (!jsonReader.Read())
                    break;


            JsonSerializerOptions serializerOptions = new();
            serializerOptions.Converters.Add(new IdConverter());

            var result = converter.Read(ref jsonReader, typeof(Options), serializerOptions);

            Assert.NotNull(result);
            Assert.Equal(4, result.Count());
            Assert.Equal("Item 1", result.FirstOrDefault()?.Label);
        }

        [Fact]
        public void WriteOptionsConverter()
        {
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);
            var converter = new OptionsConverter();

            var options = new Options(new List<OptionItem>
            {
                new OptionItem { Id = new Id("1"), Label = "Item 1" },
                new OptionItem { Id = new Id("2"), Label = "Item 2" },
                new OptionItem { Id = new Id("3"), Label = "Item 3" },
                new OptionItem { Id = new Id("4"), Label = "Item 4" }
            });

            JsonSerializerOptions serializerOptions = new();
            serializerOptions.Converters.Add(new IdConverter());

            converter.Write(writer, options, serializerOptions);
            writer.Flush();

            var json = Encoding.UTF8.GetString(buffer.WrittenSpan);
            Assert.Contains("\"Label\":\"Item 1\"", json);
        }
    }
}
