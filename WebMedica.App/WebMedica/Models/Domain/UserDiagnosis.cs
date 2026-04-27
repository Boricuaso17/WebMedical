using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedical.Models.Domain
{
    public class UserDiagnosis
    {
        [Key]
        public long Id { get; set; }

        public string? Status { get; set; }

        public string? Notes { get; set; }

        [Column("DiagnosisId_pk")]
        public long? DiagnosisId { get; set; }

        [Column("UserId_pk")]
        public long? UserId { get; set; }

        [Column("CreatedByUserId_pk")]
        public long? CreatedByUserId { get; set; }
    }
}
