using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Secureally.Models
{
    public class States
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}
