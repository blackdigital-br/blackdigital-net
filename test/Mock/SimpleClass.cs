
using System.ComponentModel.DataAnnotations;

namespace BlackDigital.Test.Mock
{
    public class SimpleClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name;
    }
}
