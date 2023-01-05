
using System.Collections.ObjectModel;

namespace BlackDigital.DataBuilder
{
    public class TypeBuilder
    {
        public TypeBuilder(Type type) 
        {
            Type = type;

            var properties = Type.GetProperties().Where(p => p.CanWrite
                                         && p.CanRead
                                         && p.GetSetMethod() != null);

            TypeProperties = properties.Select(p => new PropertyBuilder(this, p))
                                   .ToList();
        }

        protected readonly Type Type;
        private List<PropertyBuilder> TypeProperties { get; set; }
        public ReadOnlyCollection<PropertyBuilder> Properties => 
            TypeProperties.AsReadOnly();

        public ReadOnlyCollection<PropertyBuilder> OrderProperties => 
            TypeProperties.OrderBy(p => p.Order).ToList().AsReadOnly();

        public static TypeBuilder Create<T>()
        {
            return new TypeBuilder(typeof(T));
        }
    }
}
