namespace BlackDigital.Model
{
    /// <summary>
    /// Base class that implements <see cref="IDeleted"/> with the creation, update, and soft-deletion dates.
    /// </summary>
    public class DeletedModel : UpdatedModel, IDeleted
    {
        /// <summary>
        /// Gets or sets the soft-deletion date of the model, or <c>null</c> if not deleted.
        /// </summary>
        public DateTime? Deleted { get; set; }
    }
}
