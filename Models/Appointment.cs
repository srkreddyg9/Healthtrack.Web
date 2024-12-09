using System.ComponentModel.DataAnnotations;

namespace HealthTrack.Web.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        public int UserId { get; set; }
    }
}
