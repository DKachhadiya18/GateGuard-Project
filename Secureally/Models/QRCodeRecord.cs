using System.ComponentModel.DataAnnotations;

namespace Secureally.Models
{
    public class QRCodeRecord
    {
        [Key]
        public int Id { get; set; }
        public int GuardId { get; set; }
        public int QRCodeId { get; set; }
        public DateTime Time { get; set; }
    }
}
