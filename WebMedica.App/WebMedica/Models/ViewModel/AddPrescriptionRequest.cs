using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebMedical.Models.ViewModel
{
    public class AddPrescriptionRequest
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? Notes { get; set; }

        public int PatientId { get; set; }
        public int PrescribedById { get; set; }
        public int AppointmentId { get; set; }
        public int DiagnosisId { get; set; }

        public IEnumerable<SelectListItem> AppointmentList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> DiagnosisList { get; set; } = new List<SelectListItem>();
    }
}
