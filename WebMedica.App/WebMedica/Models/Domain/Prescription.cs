using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedical.Models.Domain
{
    public class Prescription
    {

        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public string Notes { get; set; }

        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        [Column("AppointmentId_fk")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        [Column("PrescribedById_fk")]
        public int PrescribedById { get; set; }
        public UserLogin PrescribedBy { get; set; }

        [Column("PatientId_fk")]
        public int PatientId { get; set; }
        public UserLogin Patient { get; set; }

        [Column("DiagnosisId_fk")]
        public int DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }



    }
}
