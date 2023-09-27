using System.ComponentModel.DataAnnotations;

namespace Secureally.Models

{
    public class VendorsRecord
    {
        [Key]
        public int Id { get; set; }
        public int VendorId { get; set; }
        public int GuardId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public string? Permission { get; set; }
        public int? PermitById { get; set; }
    }
}
