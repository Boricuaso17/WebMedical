using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedical.Models.Domain
{
    public class AppointmentDateSlot
    {
        [Key]
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public bool IsBooked { get; set; }

        [Column("AppointmentId_fk")]
        public int? AppointmentId { get; set; }

        public Appointment? Appointment { get; set; }

        [Column("AppointmentDateAvailabilityId_fk")]
        public int AppointmentDateAvailabilityId { get; set; }

        public AppointmentDateAvailability AppointmentDateAvailability { get; set; }

        [Column("ProviderUserLoginId_fk")]
        public string? ProviderUserLoginId { get; set; }

        public UserLogin? ProviderUserLogin { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
