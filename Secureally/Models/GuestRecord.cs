using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
{
    public class GuestRecord
    {
        [Key]
        public int GuestId { get; set; }
        public int GuardId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
