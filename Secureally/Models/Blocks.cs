using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
#nullable disable
{
    public class Blocks
    {
        [Key]
        public int BlockId { get; set; }
        public int SocietyId { get; set; }
        public string Block { get; set; }
    }
}
