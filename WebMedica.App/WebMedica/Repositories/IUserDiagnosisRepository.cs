using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public interface IUserDiagnosisRepository
    {
        Task<UserDiagnosis> AddUserDiagnosisAsync(UserDiagnosis userDiagnosis);
        Task<UserDiagnosis> UpdateUserDiagnosisAsync(UserDiagnosis userDiagnosis);
        Task<List<UserDiagnosis>> GetAllUserDiagnosisAsync(int userId);
    }
}
