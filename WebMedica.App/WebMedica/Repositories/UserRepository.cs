using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebMedical.Data;
using WebMedical.Enum;
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
        public async Task<UserProfile> AddSync(UserProfile user)
        {
            await _webMedicalDbContext.UserProfile.AddAsync(user);
            await _webMedicalDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            var userList = await _webMedicalDbContext.UserProfile.ToListAsync();

            return userList;
        }

        public async Task<UserProfile?> GetUserAsync(Guid guid)
        {
            var user = await _webMedicalDbContext.UserProfile.FirstOrDefaultAsync(u => u.Guid == guid);

            return user;
        }

        public async Task<List<UserProfile>> GetUserByNameAsync(string search)
        {
            var users = await _webMedicalDbContext.UserProfile.Where(u =>
                u.Name.Contains(search) ||
                u.MiddleName.Contains(search) ||
                u.LastName.Contains(search) ||
                u.LastName2.Contains(search)).ToListAsync();

            return users;
        }

        public async Task<UserProfile> UpdateAsync(UserProfile user)
        {
             _webMedicalDbContext.UserProfile.Update(user);
            await _webMedicalDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserProfile?> DeleteAsync(Guid guid)
        {
            var user = await _webMedicalDbContext.UserProfile.FirstOrDefaultAsync(u => u.Guid == guid);

            if (user != null)
            {
                _webMedicalDbContext.UserProfile.Remove(user);
                await _webMedicalDbContext.SaveChangesAsync();
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserProfile> GetUserBySSN(string ssn)
        {
            var user = await _webMedicalDbContext.UserProfile.FirstOrDefaultAsync(u => u.SocialSecurityNumber == ssn);

            return user;
        }
    }
}
