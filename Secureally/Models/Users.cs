#nullable disable
using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public int? HouseId { get; set; }
    }
}
