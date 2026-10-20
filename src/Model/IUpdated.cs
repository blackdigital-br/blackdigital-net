namespace BlackDigital.Model
{
    /// <summary>
    /// Marks a model as having an update date, extending <see cref="ICreated"/>.
    /// </summary>
    public interface IUpdated : ICreated
    {
        /// <summary>
        /// Gets or sets the last update date of the model.
        /// </summary>
        DateTime Updated { get; set; }
    }
}
