
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text.Json.Serialization;
using System.Web;

namespace BlackDigital
{
    /// <summary>
    /// Provides utility methods for object creation, URL query string serialization, cloning, casting, and resource lookup.
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// Creates a new unique identifier as a string without dashes.
        /// </summary>
        /// <returns>A new GUID string with dashes removed.</returns>
        public static string CreateId() => Guid.NewGuid().ToString().Replace("-", "");

        /// <summary>
        /// Converts an object to a URL-encoded query string, supporting nested objects and collections.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="item">The object to convert to a query string.</param>
        /// <param name="fieldPrefix">An optional prefix to prepend to each field name.</param>
        /// <returns>A URL-encoded query string.</returns>
        public static string ToUrlQueryString<T>(this T item, string fieldPrefix = "")
        {
            string prefix = string.IsNullOrEmpty(fieldPrefix) ? "" : $"{fieldPrefix}=";

            if (item is null)
                return string.Empty;

            if (item is DateTime dateItem)
                return $"{prefix}{HttpUtility.UrlEncode(dateItem.ToString("o", CultureInfo.InvariantCulture))}";

            if (item is DateTimeOffset dateOffsetItem)
                return $"{prefix}{HttpUtility.UrlEncode(dateOffsetItem.ToString("o", CultureInfo.InvariantCulture))}";

            if (item.GetType().IsValueType
                || item is string)
                return $"{prefix}{HttpUtility.UrlEncode(item.ToString())}";

            List<string> itens = new();
            int pos = 0;

            if (item is IEnumerable values)
            {
                foreach (object? valueItem in values)
                {
                    prefix = string.IsNullOrEmpty(fieldPrefix) ? "" : $"{fieldPrefix}[{pos++}]";
                    itens.Add(ToUrlQueryString(valueItem, prefix));
                }

                return string.Join("&", itens);
            }

            foreach (var property in item.GetType().GetProperties())
            {
                var jsonIgnore = property.GetCustomAttribute<JsonIgnoreAttribute>();

                if (property.CanRead && property.CanWrite && jsonIgnore == null)
                {
                    var propertyValue = property.GetValue(item, null);

                    if (propertyValue != null)
                    {
                        prefix = string.IsNullOrEmpty(fieldPrefix) ? $"{property.Name}" : $"{fieldPrefix}.{property.Name}";
                        itens.Add(ToUrlQueryString(propertyValue, prefix));
                    }
                }
            }

            itens.RemoveAll(string.IsNullOrWhiteSpace);
            return string.Join("&", itens);
        }

        /// <summary>
        /// Creates a deep copy of an object by serializing it to JSON and deserializing it back.
        /// </summary>
        /// <typeparam name="T">The type of the object to clone.</typeparam>
        /// <param name="item">The object to clone.</param>
        /// <returns>A deep copy of <paramref name="item"/>, or <c>null</c> if cloning fails.</returns>
        public static T? CloneObject<T>(this T item)
        {
            if (item == null)
                return default;

            return item.ToJson().To<T>();
        }

        /// <summary>
        /// Casts an object to a different type by serializing it to JSON and deserializing it into the target type.
        /// </summary>
        /// <typeparam name="TCast">The target type to cast to.</typeparam>
        /// <typeparam name="TOrig">The original type of the object.</typeparam>
        /// <param name="item">The object to cast.</param>
        /// <returns>An instance of <typeparamref name="TCast"/>, or <c>null</c> if the cast fails.</returns>
        public static TCast? CastObject<TCast, TOrig>(this TOrig item)
        {
            if (item == null)
                return default;
            
            return item.ToJson().To<TCast>();
        }

        /// <summary>
        /// Retrieves a resource value from a resource type, or returns the original value if no resource type is provided.
        /// </summary>
        /// <param name="value">The resource key to look up.</param>
        /// <param name="resourceType">The type of the resource class.</param>
        /// <returns>The localized resource value, or the original <paramref name="value"/> if no resource type is provided.</returns>
        public static string? GetResourceValue(string? value, Type? resourceType)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (resourceType != null)
            {
                ResourceManager rm = new(resourceType);
                var realValue = rm.GetString(value, CultureInfo.CurrentCulture);
                return realValue;
            }

            return value;
        }
    }
}
