using System.ComponentModel.DataAnnotations;

namespace Secureally.Models

{
    public class Vendors
    {
        [Key]
        public int VendorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public int HouseId { get; set; }
        public string Category { get; set; }
        public string? Company { get; set; }
    }
}
