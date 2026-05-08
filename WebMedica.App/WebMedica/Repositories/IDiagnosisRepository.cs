using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public interface IDiagnosisRepository
    {

        Task<Diagnosis> GetDiagnosisAsync(int id);
        Task<Diagnosis> GetAllDiagnosisAsync();
        Task<Diagnosis> AddAsync(Diagnosis diagnosis);
        Task<Diagnosis> UpdateAsync(Diagnosis diagnosis);
        Task<Diagnosis> DeleteAsync(int id);
    }
}
