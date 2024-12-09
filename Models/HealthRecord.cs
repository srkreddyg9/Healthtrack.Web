using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTrack.Web.Models
{
    public class HealthRecord
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Diagnosis { get; set; } = string.Empty;

        public string? Medications { get; set; }

        public string? Notes { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
