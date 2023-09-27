using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
{
    public class WorkersRecord
    {
        [Key]
        public int id { get; set; }
        public int WorkerId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
