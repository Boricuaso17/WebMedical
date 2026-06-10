using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedical.Models.Domain
{
    public class AppointmentDateAvailability
    {
        [Key]
        public int Id { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string SelectedDays { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public int IntervalMinutes { get; set; } = 30;

        [Column("CreatedByUserLoginId_fk")]
        public string? CreatedByUserLoginId { get; set; }

        public UserLogin? CreatedByUserLogin { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<AppointmentDateSlot> AppointmentDateSlots { get; set; } = new List<AppointmentDateSlot>();
    }
}
