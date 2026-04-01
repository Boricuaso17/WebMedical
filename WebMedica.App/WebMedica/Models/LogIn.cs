/*
 * Author: Joshua Mercado Rivera
 * Date: 4/23/2022  (MM/DD/YYYY)
 * Course: SICI4038 Tesina
 * This is the model class for Login 
 */

using System.ComponentModel.DataAnnotations;

namespace WebMedicalApp.Models
{
    public class LogIn
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(255)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(255)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
