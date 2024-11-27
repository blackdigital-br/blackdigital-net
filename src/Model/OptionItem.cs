
namespace BlackDigital.Model
{
    public class OptionItem
    {
        public Id Id { get; set; }

        public string Label { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public Dictionary<string, ListId> Connections { get; set; }
    }
}
