namespace WebMedical.Models.ViewModel
{
    public class AddPrescriptionDetailRequest
    {
        public int MedicationId { get; set; }
        public string Dose { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public string Instruction { get; set; }
    }
}
