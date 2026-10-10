using System.ComponentModel.DataAnnotations;

namespace BlackDigital.Model
{
    /// <summary>
    /// Marks a model as having a creation date.
    /// </summary>
    public class ICreated
    {
        /// <summary>
        /// Gets or sets the creation date of the model.
        /// </summary>
        [Required]
        public DateTime Created { get; set; }
    }
}
