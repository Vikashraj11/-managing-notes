using System.ComponentModel.DataAnnotations;

namespace Managingnotes.Models
{
    public class Notes
    {
        public int Id { get; set; }

        [Required]
        public string ? Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string ? Priority { get; set; }  
    }
}
