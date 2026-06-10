using WebMedical.Models.Domain;

namespace WebMedical.Models.ViewModel
{
    public class PatientAppointmentRequest
    {
        public DateOnly SelectedDate { get; set; }

        public string? Reason { get; set; }

        public string? Notes { get; set; }

        public IEnumerable<AppointmentDateSlot> AvailableSlots { get; set; } = new List<AppointmentDateSlot>();
    }
}
