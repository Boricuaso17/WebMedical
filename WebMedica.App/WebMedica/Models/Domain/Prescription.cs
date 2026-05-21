using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedical.Models.Domain
{
    [Table("Prescription")]
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [Column("AppointmentId_fk")]
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        [Column("DiagnosisId_fk")]
        public int DiagnosisId { get; set; }
        public Diagnosis? Diagnosis { get; set; }

        [Column("PatientId_fk")]
        public int PatientId { get; set; }
        public UserProfile? Patient { get; set; }

        [Column("PrescribedById_fk")]
        public int PrescribedById { get; set; }
        public UserProfile? PrescribedBy { get; set; }

        public DateOnly Date { get; set; }
        public string? Notes { get; set; }

        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
