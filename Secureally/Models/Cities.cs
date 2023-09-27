using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
#nullable disable
{
    public class Cities
    {
        [Key]
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }
    }
}
