using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;

namespace WebMedical.Repositories
{
    public interface IUserRepository
    {
       Task<IEnumerable<UserProfile>> GetAllAsync();

        Task<UserProfile> GetUserAsync(Guid guid);

        Task<List<UserProfile>> GetUserByNameAsync(string name);
        Task<UserProfile> GetUserBySSN(string ssn);

        Task<UserProfile> AddSync(UserProfile user);

        Task<UserProfile> UpdateAsync(UserProfile user);

        Task<UserProfile> DeleteAsync(Guid guid);

    }
}
