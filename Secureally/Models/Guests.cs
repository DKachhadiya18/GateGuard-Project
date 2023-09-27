#nullable disable
using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
{
    public class Guests
    {
        [Key]
        public int GuestId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime EstimatedDate { get; set; }
    }
}
