using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Secureally.Models
{    
    public class OTPRecord
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PhoneNumber { get; set; }
        public string? EmailID { get; set; }
        public DateTime RequestTime { get; set; }
        public int Otp { get; set; }
        public DateTime SentTime { get; set; }
    }
}
