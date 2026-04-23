using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;

namespace WebMedical.Repositories
{
    public interface IUserRepository
    {
       Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetUserAsync(Guid guid);

        Task<User> GetUserByNameAsync(string name);

        Task<User> AddSync(User user);

        Task<User> UpdateAsync(User user);

        Task<User> DeleteAsync(Guid guid);

    }
}
