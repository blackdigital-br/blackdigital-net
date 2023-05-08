using System.ComponentModel.DataAnnotations;

namespace BlackDigital.Model
{
    public class ICreated
    {
        [Required]
        public DateTime Created { get; set; }
    }
}
