using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            await _webMedicalDbContext.Diagnosis.AddAsync(diagnosis);
            await _webMedicalDbContext.SaveChangesAsync();

            return diagnosis;
        }

        public Task<Diagnosis> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Diagnosis>> GetAllDiagnosisAsync()
        {
            var diagnoses = await _webMedicalDbContext.Diagnosis.ToListAsync();

            return diagnoses;

        }

        public Task<UserDiagnosis> GetDiagnosisAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Diagnosis> UpdateAsync(Diagnosis diagnosis)
        {
            throw new NotImplementedException();
        }

        Task<Diagnosis> IDiagnosisRepository.GetDiagnosisAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
