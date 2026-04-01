/*
 * Author: Joshua Mercado Rivera
 * Date: 3/9/2022
 * Course: SICI4038 Tesina
 * This is the model class for the entity prescription
 */

using System;
using System.ComponentModel.DataAnnotations;

namespace WebMedicalApp.Models
{
    
    public class Prescription
    {
        //The ID of the prescription
        [Key]
        public int Id { get; set; }//Primary Key

        //The date of the prescription
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        //The medicine name 
        [Required]
        [Display(Name = "Medicine Name")]
        public string MedicineName { get; set; }

        //The dosis of the medicine
        [Required]
        public string Dosis { get; set; }

        //The frequency of the medicine
        [Required]
        public string Frequency { get; set; }

        //Details of the prescription
        [Required]
        public string Notes { get; set; }

        //The Social Security Number of PATIENT
        [Required]
        public string PatientId { get; set; } //Foreign Key
        public PatientUser Patient { get; set; } //Navigation Property

        //The Social Security Number of PERSONAL
        [Required]
        public string MedStaffId { get; set; }//Foreign Key
        public Admin MedStaff { get; set; } //Navigation Property
    }
}
