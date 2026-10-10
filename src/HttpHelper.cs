using System.Collections;
using System.Net;
using System.Reflection;
using System.Web;

namespace BlackDigital
{
    /// <summary>
    /// Provides extension methods for <see cref="HttpStatusCode"/> and query string parsing.
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// Determines whether the status code represents an informational response (1xx).
        /// </summary>
        /// <param name="httpStatus">The HTTP status code.</param>
        /// <returns><c>true</c> if the status code is in the 100-199 range; otherwise, <c>false</c>.</returns>
        public static bool IsInformational(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus >= 100 && (int)httpStatus <= 199;
        }

        /// <summary>
        /// Determines whether the status code represents a successful response (2xx).
        /// </summary>
        /// <param name="httpStatus">The HTTP status code.</param>
        /// <returns><c>true</c> if the status code is in the 200-299 range; otherwise, <c>false</c>.</returns>
        public static bool IsSuccess(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus >= 200 && (int)httpStatus <= 299;
        }

        /// <summary>
        /// Determines whether the status code represents a redirection response (3xx).
        /// </summary>
        /// <param name="httpStatus">The HTTP status code.</param>
        /// <returns><c>true</c> if the status code is in the 300-399 range; otherwise, <c>false</c>.</returns>
        public static bool IsRedirection(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus >= 300 && (int)httpStatus <= 399;
        }

        /// <summary>
        /// Determines whether the status code represents a client error response (4xx).
        /// </summary>
        /// <param name="httpStatus">The HTTP status code.</param>
        /// <returns><c>true</c> if the status code is in the 400-499 range; otherwise, <c>false</c>.</returns>
        public static bool IsClientError(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus >= 400 && (int)httpStatus <= 499;
        }

        /// <summary>
        /// Determines whether the status code represents a server error response (5xx).
        /// </summary>
        /// <param name="httpStatus">The HTTP status code.</param>
        /// <returns><c>true</c> if the status code is in the 500-599 range; otherwise, <c>false</c>.</returns>
        public static bool IsServerError(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus >= 500 && (int)httpStatus <= 599;
        }

        /// <summary>
        /// Parses a query string into an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the query string into.</typeparam>
        /// <param name="queryString">The query string to parse.</param>
        /// <returns>An instance of <typeparamref name="T"/> populated from the query string values.</returns>
        /// <exception cref="InvalidCastException">Thrown when the query string cannot be converted to <typeparamref name="T"/>.</exception>
        public static T FromQueryString<T>(this string queryString)
        {
            var result = FromQueryString(queryString, typeof(T));

            if (result is T typedResult)
                return typedResult;

            throw new InvalidCastException($"Could not convert query string to {typeof(T).FullName}.");
        }

        /// <summary>
        /// Parses a query string into an instance of the specified type.
        /// </summary>
        /// <param name="queryString">The query string to parse.</param>
        /// <param name="type">The type to deserialize the query string into.</param>
        /// <returns>An instance of the specified type populated from the query string values, or <c>null</c> if creation fails.</returns>
        public static object? FromQueryString(this string queryString, Type type)
        {
            var dictonary = HttpUtility.ParseQueryString(queryString ?? string.Empty);
            var dictonaryCast = dictonary.AllKeys
                                         .Where(k => k is not null)
                                         .ToDictionary(k => k!, k => dictonary[k!]);

            return CastFromDictonary(dictonaryCast, type);
        }
        
        private static object? CastFromDictonary(Dictionary<string, string?> dictonary, Type type)
        {
            var instance = Activator.CreateInstance(type);

            if (instance == null)
                return null;

            SetPropertiesValues(instance, dictonary);
            SetFieldsValues(instance, dictonary);

            return instance;
        }

        private static void SetPropertiesValues(object instance, Dictionary<string, string?> dictonary)
        {
            var properties = instance.GetType().GetProperties(BindingFlags.IgnoreCase |
                                                              BindingFlags.Public |
                                                              BindingFlags.Instance);

            foreach (var property in properties)
            {
                try
                {
                    if (!property.CanWrite)
                        continue;

                    var realValue = GetRealValue(property.Name, dictonary, property.PropertyType);

                    if (realValue != null)
                        property.SetValue(instance, realValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error cast from QueryString, Property {property.Name} \n {ex.Message}");
                }
            }
        }

        private static void SetFieldsValues(object instance, Dictionary<string, string?> dictonary)
        {
            var fields = instance.GetType().GetFields(BindingFlags.IgnoreCase |
                                                         BindingFlags.Public |
                                                         BindingFlags.Instance);

            foreach (var field in fields)
            {
                try
                {
                    var realValue = GetRealValue(field.Name, dictonary, field.FieldType);

                    if (realValue != null)
                        field.SetValue(instance, realValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error cast from QueryString, Field: {field.Name} \n {ex.Message}");
                }
            }
        }

        private static object? GetRealValue(string name, Dictionary<string, string?> dictonary, Type type)
        {
            if (type.IsClass
                && type != typeof(string)
                && dictonary.Any(item => item.Key.StartsWith($"{name}.", StringComparison.InvariantCultureIgnoreCase)))
            {
                return CastFromDictonary(dictonary.Where(item => item.Key
                                                  .StartsWith($"{name}.", StringComparison.InvariantCultureIgnoreCase))
                                                  .ToDictionary(item => item.Key.Replace($"{name}.", string.Empty), 
                                                  item => item.Value), 
                                                  type);
            }

            if (type.GetInterface(nameof(IList)) != null
                && type != typeof(string)
                && dictonary.Any(item => item.Key.StartsWith($"{name}", StringComparison.InvariantCultureIgnoreCase)))
            {
                var objectsKeys = dictonary.Where(item => item.Key.StartsWith($"{name}", StringComparison.InvariantCultureIgnoreCase))
                                           .Select(item => item.Key.Split('.')[0])
                                           .Distinct()
                                           .OrderBy(item => item)
                                           .ToList();

                object? list;
                Type? itemType;

                if (type.IsArray)
                {
                    itemType = type.GetElementType();
                    if (itemType == null)
                        return null;

                    list = Array.CreateInstance(itemType, objectsKeys.Count);
                }
                else
                {
                    itemType = type.GenericTypeArguments.FirstOrDefault();
                    if (itemType == null)
                        return null;

                    list = Activator.CreateInstance(type);
                    if (list == null)
                        return null;
                }

                if (dictonary.Any(item => item.Key.StartsWith($"{name}.", StringComparison.InvariantCultureIgnoreCase)))
                {
                    foreach (string key in objectsKeys)
                    {
                        var itemValue = CastFromDictonary(dictonary.Where(item => item.Key.StartsWith($"{key}.", StringComparison.InvariantCultureIgnoreCase))
                                                               .ToDictionary(item => item.Key.Replace($"{key}.", string.Empty),
                                                                             item => item.Value), itemType);

                        if (itemValue != null)
                        {
                            if (type.IsArray)
                                ((Array)list).SetValue(itemValue, objectsKeys.IndexOf(key));
                            else
                                ((IList)list).Add(itemValue);
                        }
                    }
                }
                else
                {
                    foreach (string key in objectsKeys)
                    {
                        var itemValue = GetRealValue(key, dictonary, itemType);

                        if (itemValue != null)
                        {
                            if (type.IsArray)
                                ((Array)list).SetValue(itemValue, objectsKeys.IndexOf(key));
                            else
                                ((IList)list).Add(itemValue);
                        }
                    }
                }

                return list;
            }

            if (type == typeof(DateTime) 
                || type == typeof(DateTime?))
            {
                var dateValue = GetDictionaryValue(dictonary, name);
                return dateValue == null ? null : DateTime.Parse(dateValue);
            }

            if (type == typeof(DateTimeOffset)
                || type == typeof(DateTimeOffset?))
            {
                var dateOffsetValue = GetDictionaryValue(dictonary, name);
                return dateOffsetValue == null ? null : DateTimeOffset.Parse(dateOffsetValue);
            }

            object? value = GetDictionaryValue(dictonary, name);

            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
                return Convert.ChangeType(value, underlyingType);

            return Convert.ChangeType(value, type);
        }

        private static string? GetDictionaryValue(Dictionary<string, string?> dictonary, string name)
        {
            var item = dictonary.FirstOrDefault(item =>
                string.Equals(item.Key, name, StringComparison.InvariantCultureIgnoreCase));

            return item.Equals(default(KeyValuePair<string, string?>)) ? null : item.Value;
        }
    }
}
