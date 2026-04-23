/*
 * Author: Joshua Mercado Rivera
 * Date: 3/9/2022
 * Course: SICI4038 Tesina
 * This is the model class for the entity diagnosis
 */

using System;
using System.ComponentModel.DataAnnotations;

namespace WebMedical.Models.Domain
{
    public class Diagnosis
    {
        //The ID number of diagnosis
        [Key]
        public int Id { get; set; } //Primary Key  

        //The date of the diagnosis
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        //The conditions name of the patient
        [Required]
        public string Condition { get; set; }

        //Details of the diagnosis
        public string Notes { get; set; }

        //The status of the diagnosis
       // [Required]
        public bool IsSigned { get; set; }

        //#region Declarations of foreing keys and navigation properties
        ////The Social Security Number of PATIENT
        //[Required]
        //public string UserId { get; set; } //Foreign Key
        //public User User { get; set; } //Navigation Property

        //#endregion
    }
}