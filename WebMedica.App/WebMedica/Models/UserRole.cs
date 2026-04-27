using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using WebMedical.Models.Domain;

namespace WebMedical.Models
{
    public class UserRole
    {
        public IEnumerable<UserProfile> AppUser { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
