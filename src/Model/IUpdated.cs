using System.ComponentModel.DataAnnotations;

namespace BlackDigital.Model
{
    public class IUpdated : ICreated
    {
        [Required]
        public DateTime Updated { get; set; }
    }
}
