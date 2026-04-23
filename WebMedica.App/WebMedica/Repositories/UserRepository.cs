using Microsoft.EntityFrameworkCore;
using WebMedical.Data;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;

namespace WebMedical.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebMedicalContext _webMedicalDbContext;

        public UserRepository(WebMedicalContext webMedicalDbContext)
        {
            _webMedicalDbContext = webMedicalDbContext;
        }
        public async Task<User> AddSync(User user)
        {
            await _webMedicalDbContext.User.AddAsync(user);
            await _webMedicalDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User?> DeleteAsync(Guid guid)
        {
            var user = await _webMedicalDbContext.User.FirstOrDefaultAsync(u => u.Guid == guid);

            if (user != null)
            {
                _webMedicalDbContext.User.Remove(user);
                await _webMedicalDbContext.SaveChangesAsync();
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var userList = await _webMedicalDbContext.User.ToListAsync();

            return userList;
        }

        public async Task<User?> GetUserAsync(Guid guid)
        {
            var user = await _webMedicalDbContext.User.FirstOrDefaultAsync(u => u.Guid == guid);

            return user;
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            var user = await _webMedicalDbContext.User.FirstOrDefaultAsync(u => u.Name == name);

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
             _webMedicalDbContext.User.Update(user);
            await _webMedicalDbContext.SaveChangesAsync();

            return user;
        }
    }
}
