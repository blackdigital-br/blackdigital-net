using BlackDigital.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlackDigital
{
    /// <summary>
    /// Provides shared JSON serialization options and helper methods for converting objects to and from JSON.
    /// </summary>
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

        /// <summary>
        /// Gets the shared default <see cref="JsonSerializerOptions"/> configured with the framework's converters.
        /// </summary>
        public static JsonSerializerOptions Options => _options;

        /// <summary>
        /// Gets the list of JSON converters registered by the framework.
        /// </summary>
        public static List<JsonConverter> Converters { get; }

        /// <summary>
        /// Configures a <see cref="JsonSerializerOptions"/> instance with the framework's default settings and converters.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        /// <returns>The configured <paramref name="options"/> instance.</returns>
        public static JsonSerializerOptions ConfigureJson(this JsonSerializerOptions options)
        {
            options.PropertyNameCaseInsensitive = true;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            Converters.ForEach((converter) => options.Converters.Add(converter));

            return options;
        }

        /// <summary>
        /// Deserializes a JSON string into an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize into.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>An instance of <typeparamref name="T"/>, or <c>null</c> if deserialization fails.</returns>
        public static T? To<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, Options);
        }

        /// <summary>
        /// Deserializes a JSON string into an instance of the specified type.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <param name="type">The type to deserialize into.</param>
        /// <returns>An instance of the specified type, or <c>null</c> if deserialization fails.</returns>
        public static object? To(this string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, Options);
        }

        /// <summary>
        /// Serializes an object to a JSON string using the framework's default options.
        /// </summary>
        /// <param name="item">The object to serialize.</param>
        /// <returns>A JSON string representation of <paramref name="item"/>.</returns>
        public static string ToJson(this object? item)
        {
            return JsonSerializer.Serialize(item, Options);
        }
    }
}
