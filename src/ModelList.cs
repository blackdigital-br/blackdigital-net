
namespace BlackDigital
{
    /// <summary>
    /// Represents a paginated list of items with metadata about the current page.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    public class ModelList<T>
        where T : class
    {
        /// <summary>
        /// Gets the collection of items in the current page.
        /// </summary>
        public IEnumerable<T> Itens { get; init; } = Array.Empty<T>();

        /// <summary>
        /// Gets the total number of items across all pages.
        /// </summary>
        public int TotalItens { get; init; }

        /// <summary>
        /// Gets or sets the current page number (1-based).
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets the total number of pages, calculated from <see cref="TotalItens"/> and <see cref="PageSize"/>.
        /// </summary>
        public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling((double)TotalItens / PageSize);

        /// <summary>
        /// Gets a value indicating whether there is a previous page available.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Gets a value indicating whether there is a next page available.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
