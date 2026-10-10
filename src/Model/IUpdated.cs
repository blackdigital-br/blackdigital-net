using System.ComponentModel.DataAnnotations;

namespace BlackDigital.Model
{
    /// <summary>
    /// Marks a model as having an update date, extending <see cref="ICreated"/>.
    /// </summary>
    public class IUpdated : ICreated
    {
        /// <summary>
        /// Gets or sets the last update date of the model.
        /// </summary>
        [Required]
        public DateTime Updated { get; set; }
    }
}
