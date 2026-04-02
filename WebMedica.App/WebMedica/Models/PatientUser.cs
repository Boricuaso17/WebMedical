/*
 * Author: Joshua Mercado Rivera
 * Date: 3/9/2022
 * Course: SICI4038 Tesina
 * This is the model class for the entity patient
 */

using WebMedical.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMedical.Models
{
    public class PatientUser //Cambio #3 IdentityUser<Guid>
    {
        private string Slug =>
            Name?.Replace(' ', '-').ToLower() + '-' + LastName1?.ToString();

        ////The Social Security Number of the patient
        [Key]
        public string SSNId { get; set; } //Primary Key

        [Required(ErrorMessage = "Please select a gender")]
        public Gender Gender { get; set; }

        //The name of the patient
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        //The Middle Name of the patient
    
        public string MiddleName { get; set; }

        //The paternal Last Name of the patient
        [Required(ErrorMessage = "Please enter the paternal lastname")]
        public string LastName1 { get; set; }

        //The maternal last name of the patient
    
        public string LastName2 { get; set; }

        //The date of birth of the patient
        [Required(ErrorMessage = "Please enter date of birth in this format: mm/dd/yyyy")]
        [DataType(DataType.Date)]
    
        public DateTime DateOfBirth { get; set; } // (yyyy-mm,dd)

        //The civil status of the patient
        [Required(ErrorMessage = "Select a civil status")]
        public CivilStatus CivilStatus { get; set; }

        //The home telephone of the patient
        [Phone]
        public string HomePhone { get; set; }

        //The personal cellphone of the patient
        [Required(ErrorMessage = "Please enter a valid cellphone number  *")]
        [Phone]
        public string CellPhone { get; set; }

        //The Work telephone of the patient
        [Phone]
        public string WorkPhone { get; set; }

        //The fisical address of the patient
        [Required(ErrorMessage = "Please enter fisical address")]
        public string FisicalAddress { get; set; }

        //The second line for the fisical address of the patient
        public string Line2 { get; set; }

        //The town where the patient lives
        [Required(ErrorMessage = "Please select a town")]
        public Town Town { get; set; }

        //The states where the patient lives
        [Required(ErrorMessage = "Please select a state")]
        public State State { get; set; }

        //The postal code
        [Required(ErrorMessage = "Please enter a postal code")]
        public int ZipCode { get; set; }

        //The postal address of the patient
        [Required(ErrorMessage = "Please enter a postal address")]
        [Display(Name = "Postal Address  *")]
        public string PostalAddress { get; set; }

        //The second line for postal address of the patient
        [Display(Name = "Postal address line 2")]
        public string PostalAddLine2 { get; set; }

        //The occupation of the patient
        [Required(ErrorMessage = "Please enter a occupation")]
        [Display(Name = "Ocupation  *")]
        public string Occupation { get; set; }

        //Display the full name of the patient
        public string DisplayFullName()
        {
            return Name + MiddleName + LastName1 + LastName2;
        }
        
        //This property is used to handle concurrency conflicts,
        //it checks whether the row was change since it was retrived
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public string AppUserId { get; set; }
        public User appUser { get; set; }

        //This are the navigation properties for the related entities(one to many)
        #region ICollection statements
        public ICollection<Prescription> Prescription { get; set; }
        public ICollection<Appointment> Appointment { get; set; }
        public ICollection<Diagnosis> Diagnosis { get; set; }
        
        #endregion
    }
}
