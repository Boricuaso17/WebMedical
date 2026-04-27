using Microsoft.AspNetCore.Identity;

namespace WebMedical.Models.Domain
{
    public class UserLogin : IdentityUser
    {

        public UserProfile? Profile { get; set; }
    }
}
