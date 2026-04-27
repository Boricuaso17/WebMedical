using System.ComponentModel.DataAnnotations;

namespace WebMedical.Models.Domain
{
    public class Diagnosis
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
