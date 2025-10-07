using System.Reflection;

namespace BlackDigital.Rest
{
    internal class RestParameter<T>
        where T : Attribute
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public T? Attribute { get; set; }

        internal static List<RestParameter<T>> GetParameters(IEnumerable<ParameterInfo> parameters,
                                                             Dictionary<string, object> arguments)
        {
            return parameters
                .Where(p => p.GetCustomAttribute<T>() != null)
                .Select(p => new RestParameter<T>
                {
                    Name = p.Name,
                    Value = arguments[p.Name],
                    Attribute = p.GetCustomAttribute<T>()
                }).ToList();
        }
    }
}
