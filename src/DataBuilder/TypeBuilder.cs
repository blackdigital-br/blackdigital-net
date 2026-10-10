
using System.Collections.ObjectModel;

namespace BlackDigital.DataBuilder
{
    /// <summary>
    /// Builds metadata for a type by inspecting its writable properties and exposing them as <see cref="PropertyBuilder"/> instances.
    /// </summary>
    public class TypeBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBuilder"/> class for the specified type.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
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

        /// <summary>
        /// Gets the read-only collection of property builders for the type, in declaration order.
        /// </summary>
        public ReadOnlyCollection<PropertyBuilder> Properties =>
            TypeProperties.AsReadOnly();

        /// <summary>
        /// Gets the read-only collection of property builders ordered by their display order.
        /// </summary>
        public ReadOnlyCollection<PropertyBuilder> OrderProperties =>
            TypeProperties.OrderBy(p => p.Order).ToList().AsReadOnly();

        /// <summary>
        /// Creates a <see cref="TypeBuilder"/> for the specified type.
        /// </summary>
        /// <typeparam name="T">The type to inspect.</typeparam>
        /// <returns>A new <see cref="TypeBuilder"/> instance.</returns>
        public static TypeBuilder Create<T>()
        {
            return new TypeBuilder(typeof(T));
        }
    }
}
