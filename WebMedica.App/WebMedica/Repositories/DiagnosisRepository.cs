using WebMedical.Data;
using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public class DiagnosisRepository : IDiagnosisRepository
    {
        private readonly WebMedicalContext _webMedicalDbContext;

        public DiagnosisRepository(WebMedicalContext webMedicalContext)
        {
            _webMedicalDbContext = webMedicalContext;
        }

        public async Task<Diagnosis> AddAsync(Diagnosis diagnosis)
        {
            await _webMedicalDbContext.AddAsync(diagnosis);
            await _webMedicalDbContext.SaveChangesAsync();

            return diagnosis;
        }

        public Task<Diagnosis> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Diagnosis> GetAllDiagnosisAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Diagnosis> GetDiagnosisAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Diagnosis> UpdateAsync(Diagnosis diagnosis)
        {
            throw new NotImplementedException();
        }
    }
}
