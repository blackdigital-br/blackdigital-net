
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Web;

namespace BlackDigital
{
    public static class ObjectHelper
    {
        public static string ToQueryString<T>(this T item, string fieldPrefix = "")
        {
            string prefix = string.IsNullOrEmpty(fieldPrefix) ? "" : $"{fieldPrefix}=";

            if (item is null)
                return $"";

            if (item is DateTime dateItem)
                return $"{prefix}{dateItem.ToString("o", CultureInfo.InvariantCulture)}";

            if (item is DateTimeOffset dateOffsetItem)
                return $"{prefix}{dateOffsetItem.ToString("o", CultureInfo.InvariantCulture).Substring(0, 19)}";

            if (item.GetType().IsValueType
                || item is string)
                return $"{prefix}{HttpUtility.UrlEncode(item.ToString())}";

            List<string> itens = new();
            int pos = 0;

            if (item is IEnumerable)
            {
                foreach (object? valueItem in item as IEnumerable)
                {
                    prefix = string.IsNullOrEmpty(fieldPrefix) ? "" : $"{fieldPrefix}[{pos++}]";
                    itens.Add($"{ToQueryString(valueItem, prefix)}");
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
                        itens.Add(ToQueryString(propertyValue, prefix));
                    }
                }
            }

            itens.RemoveAll(string.IsNullOrWhiteSpace);
            return string.Join("&", itens);
        }

        public static T? CloneOject<T>(this T item)
        {
            if (item == null)
                return default;

            return item.ToJson().To<T>();
        }
    }
}
