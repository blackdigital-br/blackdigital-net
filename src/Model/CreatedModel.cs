using System.ComponentModel.DataAnnotations;

namespace BlackDigital.Model
{
    /// <summary>
    /// Base class that implements <see cref="ICreated"/> with the creation date and its validation attributes.
    /// </summary>
    public class CreatedModel : ICreated
    {
        /// <summary>
        /// Gets or sets the creation date of the model.
        /// </summary>
        [Required]
        public DateTime Created { get; set; }
    }
}
