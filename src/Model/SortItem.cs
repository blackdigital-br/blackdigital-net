
namespace BlackDigital.Model
{
    /// <summary>
    /// Represents a sort instruction containing a property name and an optional sort direction.
    /// </summary>
    public class SortItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortItem"/> class.
        /// </summary>
        public SortItem() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortItem"/> class with the specified property name and optional sort direction.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <param name="sortAscending">Whether to sort ascending. Defaults to ascending when null.</param>
        public SortItem(string propertyName, bool? sortAscending = null)
        {
            Name = propertyName;
            SortAscending = sortAscending;
        }

        /// <summary>
        /// Gets or sets the name of the property to sort by.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether to sort ascending. A value of <c>null</c> indicates the default sort direction.
        /// </summary>
        public bool? SortAscending { get; set; }
    }
}
