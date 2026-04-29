/*
 * Author: Joshua Mercado Rivera
 * Date: 3/20/2022
 * Course: SICI 4038 Tesina
 * This is the controller class for User
 */

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMedical.Enum;

namespace WebMedical.Models.Domain
{
    [Table("User")]
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [Column("UserLoginId_fk")]
        public string? UserLoginId { get; set; }

        public Guid Guid { get; set; }
        ////The Social Security Number of the user
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

        // Show if the user is registered or not
        public bool IsRegister { get; set; }

        
        public UserLogin UserLogin { get; set; }

        // public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

        // public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        // public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
