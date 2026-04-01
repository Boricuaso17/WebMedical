/*
 * Author: Joshua Mercado Rivera
 * Date: 3/20/2022
 * Course: SICI 4038 Tesina
 * This is the controller class for User
 */

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMedicalApp.Enum;

namespace WebMedicalApp.Models
{
    public class User
    {
        ////The Social Security Number of the user
        [Key]
        public string SocialSecurityNumberID { get; set; } //Primary Key

        //The name of the Staff
        //[Required]
        public string Name { get; set; }

        //The name of the Staff
        public string MiddleName { get; set; }

        //The pathernal last name of the Staff
        // [Required]
        public string LastName1 { get; set; }

        //The mathernal last name of the Staff
        // [Required]
        public string LastName2 { get; set; }

        //The date of birth of the user
        public DateTime DateOfBirth { get; set; }

        //The personal cellphone of the user
        public string Phone { get; set; }

        //The fisical address of the user
        public string FisicalAddress { get; set; }

        //The second line for the fisical address of the user
        public string FisicalAddressLine2 { get; set; }

        //The town where the user lives
        public Town Town { get; set; }

        //The states where the patient lives
        public State State { get; set; }

        //The postal code
        public int ZipCode { get; set; }

        //The postal address of the patient
        public string PostalAddress { get; set; }

        //The second line for postal address of the patient
        public string PostalAddressLine2 { get; set; }

    }
}
