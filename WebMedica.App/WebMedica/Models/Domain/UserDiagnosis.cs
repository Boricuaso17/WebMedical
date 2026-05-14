using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedical.Models.Domain
{
    public class UserDiagnosis
    {
        [Key]
        public int Id { get; set; }

        public string? Status { get; set; }

        public string? Notes { get; set; }

        [Column("DiagnosisId_fk")]
        public int? DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        [Column("UserId_fk")]
        public int? UserId { get; set; }
        public UserProfile User { get; set; }

        [Column("CreatedByUserId_fk")]
        public string? CreatedByUserId { get; set; }
        public UserLogin CreatedByUser { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
