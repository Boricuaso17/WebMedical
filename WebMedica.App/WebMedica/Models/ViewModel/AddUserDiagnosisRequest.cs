using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using WebMedical.Models.Domain;

namespace WebMedical.Models.ViewModel
{
    public class AddUserDiagnosisRequest
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public int? DiagnosisId { get; set; }
        public int? UserId { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<SelectListItem> DiagnosisList { get; set; } = new List<SelectListItem>();
    }
}
