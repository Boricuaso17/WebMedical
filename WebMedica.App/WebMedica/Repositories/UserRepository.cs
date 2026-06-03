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
        public async Task<AddUserRequest> AddSync(AddUserRequest user)
        {
            var userProfile = ToUserProfile(user);

            await _webMedicalDbContext.UserProfile.AddAsync(userProfile);
            await _webMedicalDbContext.SaveChangesAsync();

            user.Id = userProfile.Id;
            user.Guid = userProfile.Guid;

            return user;
        }

        public async Task<IEnumerable<AddUserRequest>> GetAllAsync()
        {
            var userList = await _webMedicalDbContext.UserProfile.ToListAsync();

            return ToAddUserRequest(userList);
        }

        public async Task<AddUserRequest?> GetUserAsync(Guid guid)
        {
            var user = await _webMedicalDbContext.UserProfile.FirstOrDefaultAsync(u => u.Guid == guid);

            return ToAddUserRequest(user);
        }

        public async Task<List<AddUserRequest>> SearchUsersAsync(AddUserRequest search)
        {
            var query = _webMedicalDbContext.UserProfile
                .Include(u => u.UserLogin)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(u => u.Name.ToLower().Contains(search.Name.Trim().ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(search.MiddleName))
            {
                query = query.Where(u => u.MiddleName.ToLower().Contains(search.MiddleName.Trim().ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(search.LastName))
            {
                query = query.Where(u => u.LastName.ToLower().Contains(search.LastName.Trim().ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(search.LastName2))
            {
                query = query.Where(u => u.LastName2.ToLower().Contains(search.LastName2.Trim().ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(search.Email))
            {
                query = query.Where(u =>
                    u.UserLogin != null &&
                    u.UserLogin.Email != null &&
                    u.UserLogin.Email.ToLower().Contains(search.Email.Trim().ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(search.SocialSecurityNumber))
            {
                query = query.Where(u => u.SocialSecurityNumber.Contains(search.SocialSecurityNumber.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(search.Phone))
            {
                query = query.Where(u => u.Phone.Contains(search.Phone.Trim()));
            }

            var users = await query.ToListAsync();

            return ToAddUserRequest(users);
        }

        public async Task<AddUserRequest> UpdateAsync(AddUserRequest user)
        {
             _webMedicalDbContext.UserProfile.Update(ToUserProfile(user));
            await _webMedicalDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<AddUserRequest?> DeleteAsync(Guid guid)
        {
            var user = await _webMedicalDbContext.UserProfile.FirstOrDefaultAsync(u => u.Guid == guid);

            if (user != null)
            {
                _webMedicalDbContext.UserProfile.Remove(user);
                await _webMedicalDbContext.SaveChangesAsync();
                return ToAddUserRequest(user);
            }
            else
            {
                return null;
            }
        }

        public static AddUserRequest ToAddUserRequest(UserProfile model)
        {
                var user = new AddUserRequest()
                {
                    Id = model.Id,
                    Guid = model.Guid,
                    SocialSecurityNumber = model.SocialSecurityNumber,
                    Name = model.Name,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    LastName2 = model.LastName2,
                    DateOfBirth = model.DateOfBirth,
                    Phone = model.Phone,
                    FisicalAddress = model.FisicalAddress,
                    FisicalAddressLine2 = model.FisicalAddressLine2,
                    Town = model.Town,
                    State = model.State,
                    Zipcode = model.Zipcode,
                    PostalAddress = model.PostalAddress,
                    PostalAddressLine2 = model.PostalAddressLine2,
                    IsActive = true,
                    UserLogin = model.UserLogin
                };

            return user;
        }

        public static List<AddUserRequest> ToAddUserRequest(List<UserProfile> users)
        {
            return users.Select(ToAddUserRequest).ToList();
        }

        public static UserProfile ToUserProfile(AddUserRequest model)
        {
            var user = new UserProfile
            {
                Id = model.Id,
                Guid = model.Guid == Guid.Empty ? Guid.NewGuid() : model.Guid,
                SocialSecurityNumber = model.SocialSecurityNumber,
                Name = model.Name,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                LastName2 = model.LastName2,
                DateOfBirth = model.DateOfBirth,
                Phone = model.Phone,
                FisicalAddress = model.FisicalAddress,
                FisicalAddressLine2 = model.FisicalAddressLine2,
                Town = model.Town,
                State = model.State,
                Zipcode = model.Zipcode,
                PostalAddress = model.PostalAddress,
                PostalAddressLine2 = model.PostalAddressLine2,
                IsRegister = true,
                IsActive = true,
                UserLogin = model.UserLogin
            };

            return user;
        }
    }
}

