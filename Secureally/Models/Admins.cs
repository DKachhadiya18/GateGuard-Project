using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
{
    public class Admins
    {
        [Key]
        public int UserId { get; set;}
        public int SocietyId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
