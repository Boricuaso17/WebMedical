/*
 * Author: Joshua Mercado Rivera
 * Date: 3/9/2022
 * Course: SICI4038 Tesina
 * This is the model class for the entity MedStaffUser
 */

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebMedical.Models.Domain;

namespace WebMedical.Models
{
    //Inherits all IdentityUser properties
    public class Admin //: IdentityUser //Cambio #2 IdentityUser<Guid>
    {
        ////The Social Security Number of the patient
        [Key]
        public string SSNId { get; set; } //Primary Key

        //The name of the Staff
        //[Required]
        public string Name { get; set; }

        //The middle name of the Staff
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        //The pathernal last name of the Staff
       // [Required]
        public string LastName1 { get; set; }

        //The mathernal last name of the Staff
       // [Required]
        public string LastName2 { get; set; }

        //The position in the work of the Staff
       // [Required]
        public string Position { get; set; }

        //If the Staff is resident
       // [Required]
        public bool IsResident { get; set; }

        public string AppUserId { get; set; }
        public User appUser { get; set; }

        ////This are the navigation properties for the related entities(one to many)
        #region ICollection statements (one to many)
        public ICollection<Diagnosis> Diagnosis { get; set; }
        public ICollection<Prescription> Prescription { get; set; }
        #endregion

    }
}
