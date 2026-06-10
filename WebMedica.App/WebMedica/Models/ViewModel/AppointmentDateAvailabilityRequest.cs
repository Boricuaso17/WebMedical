using System.ComponentModel.DataAnnotations;

namespace WebMedical.Models.ViewModel
{
    public class AppointmentDateAvailabilityRequest
    {
        public int Id { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        public List<DayOfWeek> SelectedDays { get; set; } = new List<DayOfWeek>();
    }
}
