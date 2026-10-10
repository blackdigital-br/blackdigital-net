
using System.Collections;

namespace BlackDigital.Model
{
    /// <summary>
    /// Represents a collection of <see cref="OptionItem"/> with filtering capabilities by label, text, and connections.
    /// </summary>
    public class Options : IEnumerable<OptionItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Options"/> class with an empty collection.
        /// </summary>
        public Options()
        {
            Items = new OptionItem[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Options"/> class with the specified items.
        /// </summary>
        /// <param name="items">The collection of option items.</param>
        public Options(IEnumerable<OptionItem> items)
        {
            Items = items.ToArray();
        }

        private OptionItem[] Items { get; set; }

        /// <summary>
        /// Gets the option item with the specified integer identifier.
        /// </summary>
        /// <param name="key">The identifier of the option item.</param>
        /// <returns>The option item, or <c>null</c> if not found.</returns>
        public OptionItem? this[int key]
        {
            get
            {
                if (Items.Any(option => option.Id == key))
                    return this.FirstOrDefault(o => o.Id == key);

                return null;
            }
        }

        /// <summary>
        /// Determines whether the collection contains an option with the specified identifier.
        /// </summary>
        /// <param name="key">The identifier to check.</param>
        /// <returns><c>true</c> if the option exists; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(int key)
        {
            return Items.Any(option => option.Id == key);
        }

        /// <summary>
        /// Filters the options to those that contain the specified connection and identifier.
        /// </summary>
        /// <param name="name">The connection name.</param>
        /// <param name="id">The connection identifier.</param>
        /// <returns>A new <see cref="Options"/> collection with the filtered items.</returns>
        public Options FilterConnections(string name, Id id)
        {
            return new Options(Items.Where(option => option.Connections.ContainsKey(name)
                                && option.Connections[name].Contains(id)));
        }

        /// <summary>
        /// Filters the options to those whose label contains the specified text.
        /// </summary>
        /// <param name="label">The text to search for in the label.</param>
        /// <returns>A new <see cref="Options"/> collection with the filtered items.</returns>
        public Options FilterLabel(string label)
        {
            return new Options(Items.Where(option => option.Label.Contains(label, StringComparison.InvariantCultureIgnoreCase)));
        }

        /// <summary>
        /// Filters the options to those whose label, code, or description contains the specified text.
        /// </summary>
        /// <param name="text">The text to search for.</param>
        /// <returns>A new <see cref="Options"/> collection with the filtered items.</returns>
        public Options FindText(string text)
        {
            return new Options(Items.Where(option =>
                            option.Label.Contains(text, StringComparison.InvariantCultureIgnoreCase)
                            || (option.Code?.Contains(text, StringComparison.InvariantCultureIgnoreCase) ?? false)
                            || (option.Description?.Contains(text, StringComparison.InvariantCultureIgnoreCase) ?? false)
                        ));
        }

        /// <summary>
        /// Filters the options to those that have a connection containing the specified identifier.
        /// </summary>
        /// <param name="id">The connection identifier to search for.</param>
        /// <returns>A new <see cref="Options"/> collection with the filtered items.</returns>
        public Options FilterConnectionsId(Id id)
        {
            return new Options(
                Items.Where(option => option.Connections.Any(parent => parent.Value.Contains(id)))
            );
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        public IEnumerator<OptionItem> GetEnumerator() => ((IEnumerable<OptionItem>)Items).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Items).GetEnumerator();
    }
}
