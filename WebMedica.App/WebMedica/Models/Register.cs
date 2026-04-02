/*
 * Author: Joshua Mercado Rivera
 * Date: 4/23/2022  (MM/DD/YYYY)
 * Course: SICI4038 Tesina
 * This is the model class for Register
 */

using System.ComponentModel.DataAnnotations;

namespace WebMedical.Models
{
    public class Register
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter a username.")]
        [RegularExpression("^[a-zA-Z0-9 ]+$",
           ErrorMessage = "Username may not contain special characters.")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Please enter an email address.")]
        //[Remote("CheckEmail", "Validation")]
        //public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [Compare("ConfirmPassword")]
        [StringLength(25,
            ErrorMessage = "Please limit your password to 25 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm yout password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        ////Navigation property for the related entities RegisterMedStaff & Personal (one to one)
        //public MedStaffUser MedStaff { get; set; } //Navigation Property

    }
}
