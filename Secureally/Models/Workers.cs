using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
#nullable disable
{
    public class Workers
    {
        [Key]
        public int WorkerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Category { get; set; }
        public int PhoneNumber { get; set; }
        public int? HouseId { get; set; }
        public string? VehicleNumber { get; set; }
        public string Picture { get; set; }

    }
}
