namespace BlackDigital.Model
{
    /// <summary>
    /// Marks a model as having an active state.
    /// </summary>
    public interface IActive
    {
        /// <summary>
        /// Gets a value indicating whether the model is active.
        /// </summary>
        bool Active { get; }
    }
}
