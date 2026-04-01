using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace WebMedicalApp.Models
{
    public class UserRole
    {
        public IEnumerable<User> AppUsers { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
