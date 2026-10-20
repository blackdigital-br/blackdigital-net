namespace BlackDigital.Model
{
    /// <summary>
    /// Marks a model as having a soft-deletion date, extending <see cref="IUpdated"/>.
    /// </summary>
    public interface IDeleted : IUpdated
    {
        /// <summary>
        /// Gets or sets the soft-deletion date of the model, or <c>null</c> if not deleted.
        /// </summary>
        DateTime? Deleted { get; set; }
    }
}
