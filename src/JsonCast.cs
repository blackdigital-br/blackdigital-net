using BlackDigital.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlackDigital
{
    public static class JsonCast
    {
        private static readonly JsonSerializerOptions _options;

        static JsonCast()
        {
            Converters = new()
            {
                new JsonStringEnumConverter(),
                new TimeSpanConverter(),
                new IdConverter(),
            };

            _options = new JsonSerializerOptions().ConfigureJson();
        }

        public static JsonSerializerOptions Options => _options;

        public static List<JsonConverter> Converters { get; }

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

        public static string ToJson(this object? item)
        {
            return JsonSerializer.Serialize(item, Options);
        }
    }
}
