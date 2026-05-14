using Microsoft.EntityFrameworkCore;
using WebMedical.Data;
using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public class UserDiagnosisRepository : IUserDiagnosisRepository
    {
        private readonly WebMedicalContext _webMedicalDbContext;

        public UserDiagnosisRepository(WebMedicalContext webMedicalDbContext)
        {
            _webMedicalDbContext = webMedicalDbContext;
        }

        public async Task<UserDiagnosis> AddUserDiagnosisAsync(UserDiagnosis userDiagnosis)
        {
            await _webMedicalDbContext.UserDiagnosis.AddAsync(userDiagnosis);
            await _webMedicalDbContext.SaveChangesAsync();

            return userDiagnosis;
        }

        public async Task<List<UserDiagnosis>> GetAllUserDiagnosisAsync(int userId)
        {
            var userDiagnosisList = await _webMedicalDbContext.UserDiagnosis.Where(ud => ud.UserId == userId).ToListAsync();

            return userDiagnosisList;
        }

        public async Task<UserDiagnosis> UpdateUserDiagnosisAsync(UserDiagnosis userDiagnosis)
        {
            _webMedicalDbContext.UserDiagnosis.Update(userDiagnosis);
            await _webMedicalDbContext.SaveChangesAsync();

            return userDiagnosis;
        }
    }
}
