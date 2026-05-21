/*
 * Author: Joshua Mercado Rivera
 * Date: 3/8/2022
 * Course: SICI4038 Tesina
 * This is the Context class for the Database WebMedicalDb
 */

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WebMedical.Models.Domain;

namespace WebMedical.Data
{
    public class WebMedicalContext : IdentityDbContext<UserLogin> // DbContext 
    {

        public WebMedicalContext(DbContextOptions<WebMedicalContext> options) : base(options) { }

        //Name of the tables in the data base WebMedicalDb
        #region DbSet Declarations
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserDiagnosis> UserDiagnosis { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Medication> Medication { get; set; }
        #endregion 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().ToTable("UserProfile");

            modelBuilder.Entity<UserLogin>()
                .HasOne(u => u.UserProfile)
                .WithOne(p => p.UserLogin)
                .HasForeignKey<UserLogin>(p => p.UserProfileId);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany()
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.PrescribedBy)
                .WithMany()
                .HasForeignKey(p => p.PrescribedById)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
