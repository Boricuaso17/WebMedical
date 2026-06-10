namespace WebMedical.Models.ViewModel
{
    public class AddAppointmentRequest
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly? Time { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
        public string UserLoginId { get; set; }
    }
}
