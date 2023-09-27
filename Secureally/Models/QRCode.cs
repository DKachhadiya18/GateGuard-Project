using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Secureally.Models
{
    public class QRCode
    {
        [Key]
        public int QRCodeId { get; set; }
        public string CodeName { get; set; }

    }
}
