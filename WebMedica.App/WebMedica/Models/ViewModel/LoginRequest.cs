using Microsoft.AspNetCore.Mvc;

namespace WebMedical.Models.ViewModel
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
