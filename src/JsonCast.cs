using BlackDigital.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlackDigital
{
    public static class JsonCast
    {
        static JsonCast()
        {
            Converters = new()
            {
                new JsonStringEnumConverter(),
                new TimeSpanConverter(),
                new IdConverter(),
            };
        }

        public static JsonSerializerOptions Options
        {
            get
            {
                JsonSerializerOptions options = new();
                return options.ConfigureJson();
            }
        }

        public static List<JsonConverter> Converters { get; set; }

        public static JsonSerializerOptions ConfigureJson(this JsonSerializerOptions options)
        {
            options.PropertyNameCaseInsensitive = true;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            Converters.ForEach((converter) => options.Converters.Add(converter));

            return options;
        }
        public static T? To<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, Options);
        }

        public static object? To(this string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, Options);
        }

        public static string ToJson(this object item)
        {
            return JsonSerializer.Serialize(item, Options);
        }
    }
}