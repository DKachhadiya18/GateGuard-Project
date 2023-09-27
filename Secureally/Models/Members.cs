using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
#nullable disable
{
    //Members are the SocietyMembers Added by admin {invited by admin}
    public class Members
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailId { get; set; }
        
    }
}
