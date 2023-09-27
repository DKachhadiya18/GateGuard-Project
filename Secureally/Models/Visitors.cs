using System.ComponentModel.DataAnnotations;

namespace Secureally.Models

{
    public class Visitors
    {
        [Key]
        public int VisitorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public int HouseId { get; set; }
        public string? VehicleNumber { get; set; }
    }
}
