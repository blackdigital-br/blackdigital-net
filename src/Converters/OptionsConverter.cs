using BlackDigital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlackDigital.Converters
{
    public class OptionsConverter : JsonConverter<Options>
    {
        public override Options? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var items = JsonSerializer.Deserialize<List<OptionItem>>(ref reader, options);
            return new Options(items);
        }

        public override void Write(Utf8JsonWriter writer, Options value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.ToList(), options);
        }
    }
}
