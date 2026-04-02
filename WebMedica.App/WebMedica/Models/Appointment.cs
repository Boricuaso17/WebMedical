/*
 * Author: Joshua Mercado Rivera
 * Date: 3/20/2022
 * Course: SICI 4038 Tesina
 * This is the Model class for Appointment
 */


using System;
using System.ComponentModel.DataAnnotations;

namespace WebMedical.Models
{
    public class Appointment
    {
        //APPOINTMENT ID
        [Key]
        public int Id { get; set; }//Primary Key

        //The date of the appointment
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        //The reason for the appointment
        [Required]
        public string Reason { get; set; }

        //Details for the appointment
        public string Notes { get; set; }

        //The Social Security Number of PATIENT
        [Required]
        public string PatientId { get; set; } // Foreign Key
        public PatientUser Patient { get; set; } //Navigation Property
    }
}
