using System.ComponentModel.DataAnnotations;

namespace BlackDigital.Model
{
    /// <summary>
    /// Base class that implements <see cref="IUpdated"/> with the creation and update dates and their validation attributes.
    /// </summary>
    public class UpdatedModel : CreatedModel, IUpdated
    {
        /// <summary>
        /// Gets or sets the last update date of the model.
        /// </summary>
        [Required]
        public DateTime Updated { get; set; }
    }
}
