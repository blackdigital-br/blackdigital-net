﻿
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text.Json.Serialization;
using System.Web;

namespace BlackDigital
{
    public static class ObjectHelper
    {
        public static string CreateId() => Guid.NewGuid().ToString().Replace("-", "");

        public static string ToUrlQueryString<T>(this T item, string fieldPrefix = "")
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
                    itens.Add($"{ToUrlQueryString(valueItem, prefix)}");
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

        public static T? CloneObject<T>(this T item)
        {
            if (item == null)
                return default;

            return item.ToJson().To<T>();
        }

        public static TCast? CastObject<TCast, TOrig>(this TOrig item)
        {
            if (item == null)
                return default;
            
            return item.ToJson().To<TCast>();
        }

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
