
namespace BlackDigital.Rest
{
    /// <summary>
    /// Specifies which types of exceptions the REST client should throw automatically.
    /// </summary>
    public enum RestThownType
    {
        /// <summary>
        /// No exceptions are thrown automatically.
        /// </summary>
        None,

        /// <summary>
        /// Only <see cref="BusinessException"/> instances are thrown.
        /// </summary>
        OnlyBusiness,

        /// <summary>
        /// All exceptions are thrown.
        /// </summary>
        All
    }
}
