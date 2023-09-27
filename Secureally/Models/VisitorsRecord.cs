using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
#nullable disable
{
    public class VisitorsRecord
    {
        [Key]
        public int Id { get; set; }
        public int VisitorId { get; set; }
        public int GuardId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public string? Permission { get; set; }
        public int? PermitById { get; set; }
    }
}
