using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
#nullable disable
{
    public class Houses
    {
        [Key]
        public int HouseId { get; set; }
        public int BlockId { get; set; }
        public string House { get; set; }
    }
}
