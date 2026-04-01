/*
 * Author: Joshua Mercado Rivera
 * Date: 3/20/2022
 * Course: SICI 4038 Tesina
 * This is the model class for ChangePassword
 */


using System.ComponentModel.DataAnnotations;

namespace WebMedicalApp.Models
{
    public class ChangePassword
    {
        public string Username { get; set; }

        [Required(ErrorMessage ="Please enter password.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please enter a new password.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
