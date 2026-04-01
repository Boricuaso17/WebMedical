/*
 * Author: Joshua Mercado Rivera
 * Date: 3/8/2022
 * Course: SICI4038 Tesina
 * This is the Context class for the Database WebMedicalDb
 */

using Microsoft.EntityFrameworkCore;
using System;

namespace WebMedicalApp.Models
{
    public class WebMedicalContext : DbContext // DbContext 
    {

        public WebMedicalContext(DbContextOptions<WebMedicalContext> options) : base(options) { }

        //Name of the tables in the data base WebMedicalDb
        #region DbSet Declarations
        public DbSet<Admin> MEDSTAFF { get; set; }
        public DbSet<PatientUser> PATIENT { get; set; }
        public DbSet<Diagnosis> DIAGNOSIS { get; set; }
        public DbSet<Appointment> APPOINTMENT { get; set; }
        public DbSet<Prescription> PRESCRIPTION { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Seeds data to the entities

            //Seeds 3 records on entity PATIENT
            #region PatientUser Seeds
            modelBuilder.Entity<PatientUser>().HasData(
                new PatientUser
                {
                    SSNId = "1",
                    Name = "Joshua",
                    Gender = Enum.Gender.Male,
                    MiddleName = "A",
                    LastName1 = "Mercado",
                    LastName2 = "Rivera",
                    DateOfBirth = new DateTime(1998, 01, 20),
                    CivilStatus = Enum.CivilStatus.Single,
                    CellPhone = "7874444777",
                    FisicalAddress = "0411 Helena Junction",
                    Line2 = "Mayer",
                    Town = Enum.Town.Juncos,
                    State = Enum.State.Puerto_Rico,
                    ZipCode = 00754,
                    PostalAddress = "La misma",
                    Occupation = "Programmer",
                }
                );

            modelBuilder.Entity<PatientUser>().HasData(
               new PatientUser
               {
                   SSNId = "2",
                   Name = "Ana",
                   Gender = Enum.Gender.Female,
                   MiddleName = "I",
                   LastName1 = "Perez",
                   LastName2 = "Smith",
                   DateOfBirth = new DateTime(1968, 05, 20),
                   CivilStatus = Enum.CivilStatus.Married,
                   CellPhone = "7871234567",
                   FisicalAddress = "035 Valley Edge Place",
                   Line2 = "Springs",
                   Town = Enum.Town.Canóvanas,
                   State = Enum.State.Puerto_Rico,
                   ZipCode = 00477,
                   PostalAddress = "La misma",
                   Occupation = "Maestra",
               });

            modelBuilder.Entity<PatientUser>().HasData(
               new PatientUser
               {
                   SSNId = "3",
                   Name = "Ivan",
                   Gender = Enum.Gender.Male,
                   LastName1 = "Mercado",
                   LastName2 = "Diaz",
                   DateOfBirth = new DateTime(1966, 05, 09),
                   CivilStatus = Enum.CivilStatus.Married,
                   CellPhone = "7879874561",
                   FisicalAddress = "284 Elgar Trail",
                   Line2 = "Onsgard",
                   Town = Enum.Town.Rio_Grande,
                   State = Enum.State.Puerto_Rico,
                   ZipCode = 00518,
                   PostalAddress = "La misma",
                   Occupation = "Carrero",

               });
            #endregion

            //Seeds 3 records on entity MedStaffUser
            #region MedStaff Seeds
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    SSNId = "20",
                    Name = "Charlie",
                    MiddleName = "J",
                    LastName1 = "Mercado",
                    LastName2 = "Rivera",
                    Position = "Doctor",
                    IsResident = false
                });

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    SSNId = "21",
                    Name = "Onel",
                    MiddleName = "A",
                    LastName1 = "Mercado",
                    LastName2 = "Rivera",
                    Position = "Nurse",
                    IsResident = true
                });

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    SSNId = "22",
                    Name = "Sheila",
                    LastName1 = "Calderon",
                    LastName2 = "Maldonado",
                    Position = "Doctora",
                    IsResident = false
                });
            #endregion

            //Seeds 3 records on entity DIAGNOSIS
            #region Diagnosis Seeds
            modelBuilder.Entity<Diagnosis>().HasData(
                new Diagnosis
                {
                    Id = 30,
                    Date = new DateTime(2015, 07, 18),
                    Condition = "Diabetes",
                    Notes = "Chronica Tipo 2",
                    PatientId = 1.ToString(),
                    MedStaffId = 22.ToString()
                });

            modelBuilder.Entity<Diagnosis>().HasData(
                new Diagnosis
                {
                    Id = 31,
                    Date = new DateTime(2018, 05, 08),
                    Condition = "Miopia",
                    Notes = "Chronica Tipo 2",
                    PatientId = "3",
                    MedStaffId = "22"
                });

            modelBuilder.Entity<Diagnosis>().HasData(
                new Diagnosis
                {
                    Id = 32,
                    Date = new DateTime(2020, 12, 06),
                    Condition = "Chrons",
                    Notes = "En el yeyuno",
                    IsSigned = true,
                    PatientId = "2",
                    MedStaffId = "22"
                });

            modelBuilder.Entity<Diagnosis>().HasData(
                new Diagnosis
                {
                    Id = 34,
                    Date = new DateTime(2017, 09, 04),
                    Condition = "Escoliosis",
                    Notes = "12 grados de curvatura por la espalda baja",
                    PatientId = "1",
                    MedStaffId = "20"
                });
            #endregion

            //Seeds 3 records on entity APPOINTMENT
            #region Appointment Seeds
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    Date = new DateTime(2022, 10, 1),
                    Reason = "Visita de chequeo",
                    Notes = "Visitas Programadas cada 3 meses",
                    PatientId = "2",
                });

            modelBuilder.Entity<Appointment>().HasData(
            new Appointment
            {
                Id = 2,
                Date = new DateTime(2022, 7, 19),
                Reason = "Malestar estomacal",
                Notes = "Posible diagnostico de gastritis",
                PatientId = "1"
            });

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 3,
                    Date = new DateTime(2022, 11, 21),
                    Reason = "Accidente",
                    Notes = "Dolor intenso en el cuerpo",
                    PatientId = "3"
                });
            #endregion

            //Seeds 3 records on entity PRESCRIPTION
            #region Prescription Seeds
            modelBuilder.Entity<Prescription>().HasData(
                new Prescription
                {
                    Id = 100,
                    Date = new DateTime(2022, 01, 20),
                    MedicineName = "Humira",
                    Dosis = "40g",
                    Frequency = "Cada dos semanas",
                    Notes = "Para el tratamiento del Chrons",
                    PatientId = "1",
                    MedStaffId = "22",
                });

            modelBuilder.Entity<Prescription>().HasData(
                new Prescription
                {
                    Id = 200,
                    Date = new DateTime(2022, 01, 20),
                    MedicineName = "Insulina",
                    Dosis = "1,5 UI/kg",
                    Frequency = "Diaria",
                    Notes = "Para el tratamiento de la diabetes tipo 1",
                    PatientId = "3",
                    MedStaffId = "22",
                });

            modelBuilder.Entity<Prescription>().HasData(
            new Prescription
            {
                Id = 300,
                Date = new DateTime(2022, 01, 20),
                MedicineName = "Humira",
                Dosis = "50g",
                Frequency = "Cada dos semanas",
                Notes = "Para el tratamiento de artritis",
                PatientId = "2",
                MedStaffId = "21",
            });
            #endregion

            #endregion

            #region Handles Concurrency Conflicts for the differents entities
            modelBuilder.Entity<PatientUser>().Property(p => p.RowVersion).IsRowVersion();

            #endregion
        }
    }
}
