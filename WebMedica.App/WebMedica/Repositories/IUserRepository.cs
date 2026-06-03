using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;

namespace WebMedical.Repositories
{
    public interface IUserRepository
    {
       Task<IEnumerable<AddUserRequest>> GetAllAsync();

        Task<AddUserRequest> GetUserAsync(Guid guid);

        Task<List<AddUserRequest>> SearchUsersAsync(AddUserRequest search);

        Task<AddUserRequest> AddSync(AddUserRequest user);

        Task<AddUserRequest> UpdateAsync(AddUserRequest user);

        Task<AddUserRequest> DeleteAsync(Guid guid);

    }
}

