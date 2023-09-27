using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Secureally.Models
{
    public class Society
    {
        [Key]
        public int SocietyId { get; set; }
        public string SocietyName { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public int CityId{ get; set; }
        public int Pincode { get; set; }
        public int StateId { get; set; }
    }
}
