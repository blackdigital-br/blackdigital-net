
namespace BlackDigital.Model
{
    /// <summary>
    /// Represents the base filter parameters for paginated and sorted queries.
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// Gets or sets the number of items to skip.
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// Gets or sets the number of items to take.
        /// </summary>
        public int? Take { get; set; }

        /// <summary>
        /// Gets or sets the list of sort instructions.
        /// </summary>
        public List<SortItem> Sort { get; set; } = new();
    }
}
