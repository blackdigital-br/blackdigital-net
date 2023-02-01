using System.Collections;
using System.Reflection;
using System.Web;

namespace BlackDigital
{
    public static class HttpHelper
    {
        public static T FromQueryString<T>(this string queryString)
            => (T)FromQueryString(queryString, typeof(T));

        public static object? FromQueryString(this string queryString, Type type)
        {
            var dictonary = HttpUtility.ParseQueryString(queryString ?? string.Empty);
            var dictonaryCast = dictonary.Cast<string>().ToDictionary(k => k, v => dictonary[v]);

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

                object list;
                Type itemType;

                if (type.IsArray)
                {
                    itemType = type.GetElementType();
                    list = Array.CreateInstance(itemType, objectsKeys.Count);
                }
                else
                {
                    itemType = type.GenericTypeArguments[0];
                    list = Activator.CreateInstance(type);
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
                return DateTime.Parse(dictonary[name]);

            if (type == typeof(DateTimeOffset)
                || type == typeof(DateTimeOffset?))
                return DateTimeOffset.Parse(dictonary[name]);

            object value = null;

            if (dictonary.Any(item => string.Compare(item.Key, name, StringComparison.InvariantCultureIgnoreCase) == 0))
            {
                var item = dictonary.First(item => string.Compare(item.Key, name, StringComparison.InvariantCultureIgnoreCase) == 0);
                value = item.Value;
            }

            if (Nullable.GetUnderlyingType(type) != null)
                return Convert.ChangeType(value, Nullable.GetUnderlyingType(type));

            return Convert.ChangeType(value, type);
        }
    }
}
