using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
#nullable disable
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

    }
}
