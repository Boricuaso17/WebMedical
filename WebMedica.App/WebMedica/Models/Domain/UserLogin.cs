using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedical.Models.Domain
{
    public class UserLogin : IdentityUser
    {
        [Column("UserProfileId_fk")]
        public int UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
