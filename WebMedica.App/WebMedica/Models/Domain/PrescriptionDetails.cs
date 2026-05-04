using System.ComponentModel.DataAnnotations;

namespace WebMedical.Models.Domain
{
    public class PrescriptionDetails
    {
        [Key]
        public int Id { get; set; }

        public string Dose { get; set; }
        public string Frequency { get; set; }
        public string Duration {  get; set; }

        public string Instruction { get; set; }

        public int MedicationId { get; set; }
        public Medication Medication { get; set; }

        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }
    }
}
