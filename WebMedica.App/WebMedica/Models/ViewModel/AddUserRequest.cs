using System.ComponentModel.DataAnnotations;
using WebMedical.Enum;
using WebMedical.Models.Domain;

namespace WebMedical.Models.ViewModel
{
    public class AddUserRequest
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }

        [Key]
        public string SocialSecurityNumber { get; set; } //Primary Key

        //The name of the Staff
        //[Required]
        public string Name { get; set; }

        //The name of the Staff
        public string MiddleName { get; set; }

        //The pathernal last name of the Staff
        // [Required]
        public string LastName { get; set; }

        //The mathernal last name of the Staff
        // [Required]
        public string LastName2 { get; set; }

        //The date of birth of the user
        public DateOnly DateOfBirth { get; set; }

        //The personal cellphone of the user
        public string Phone { get; set; }

        public string? MedicPlan { get; set; }

        //The fisical address of the user
        public string FisicalAddress { get; set; }

        //The second line for the fisical address of the user
        public string FisicalAddressLine2 { get; set; }

        //The town where the user lives
        public int Town { get; set; }

        //The states where the patient lives
        public int State { get; set; }

        //The postal code
        public int Zipcode { get; set; }

        //The postal address of the patient
        public string PostalAddress { get; set; }

        //The second line for postal address of the patient
        public string PostalAddressLine2 { get; set; }

        // Shows if the user is active or not in the system
        public bool IsActive { get; set; }

        // Navegation property
        public UserLogin UserLogin { get; set; }

        /// <summary>
        /// Username of the user required to login into the app
        /// </summary>
        [Required(ErrorMessage = "Please enter a username.")]
        [RegularExpression("^[a-zA-Z0-9 ]+$",
        ErrorMessage = "Username may not contain special characters.")]
        public string Username { get; set; }

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

        public int Role { get; set; }

        public string FullName => string.Join(" ", new[] { Name, MiddleName, LastName, LastName2 }
            .Where(s => !string.IsNullOrWhiteSpace(s)));

    }
}
