using System.ComponentModel.DataAnnotations;

namespace WebMedical.Models.Domain
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
