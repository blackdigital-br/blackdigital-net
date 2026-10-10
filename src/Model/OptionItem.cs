
namespace BlackDigital.Model
{
    /// <summary>
    /// Represents a single option item within a list of options, with an identifier, label, code, description, and connections.
    /// </summary>
    public struct OptionItem
    {
        /// <summary>
        /// Gets or sets the unique identifier of the option.
        /// </summary>
        public Id Id { get; set; }

        /// <summary>
        /// Gets or sets the display label of the option.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the optional code of the option.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the optional description of the option.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the connections of the option has to other options, grouped by connection name.
        /// </summary>
        public Dictionary<string, ListId> Connections { get; set; }
    }
}
