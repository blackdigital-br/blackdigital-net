namespace BlackDigital.Model
{
    /// <summary>
    /// Marks a model as having a creation date.
    /// </summary>
    public interface ICreated
    {
        /// <summary>
        /// Gets or sets the creation date of the model.
        /// </summary>
        DateTime Created { get; set; }
    }
}
