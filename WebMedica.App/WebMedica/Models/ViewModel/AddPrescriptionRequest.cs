using System.ComponentModel.DataAnnotations.Schema;
using WebMedical.Models.Domain;

namespace WebMedical.Models.ViewModel
{
    public class AddPrescriptionRequest
    {

        public DateOnly Date { get; set; }
        public string Notes { get; set; }

        public int PatientId { get; set; }
        public int PrescribedById { get; set; }
        public int AppointmentId { get; set; }
        public int DiagnosisId { get; set; }

        public List<AddPrescriptionDetailRequest> Details { get; set; }
    }
}
