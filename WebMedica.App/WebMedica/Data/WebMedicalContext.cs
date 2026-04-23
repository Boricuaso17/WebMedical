/*
 * Author: Joshua Mercado Rivera
 * Date: 3/8/2022
 * Course: SICI4038 Tesina
 * This is the Context class for the Database WebMedicalDb
 */

using Microsoft.EntityFrameworkCore;
using System;
using WebMedical.Models.Domain;

namespace WebMedical.Data
{
    public class WebMedicalContext : DbContext // DbContext 
    {

        public WebMedicalContext(DbContextOptions<WebMedicalContext> options) : base(options) { }

        //Name of the tables in the data base WebMedicalDb
        #region DbSet Declarations
        public DbSet<User> User { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        #endregion 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.IsActive);

            modelBuilder.Entity<User>()
                .Property(u => u.IsRegister);

            base.OnModelCreating(modelBuilder);
        }
    }
}
