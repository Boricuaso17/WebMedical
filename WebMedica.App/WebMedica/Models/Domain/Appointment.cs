using System.ComponentModel.DataAnnotations;

namespace WebMedical.Models.Domain
{
    public class Appointment
    {

        [Key]
        public int Id { get; set; }

        public DateOnly Date { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
    }
}
