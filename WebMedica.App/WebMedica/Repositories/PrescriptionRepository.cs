using Microsoft.EntityFrameworkCore;
using WebMedical.Data;
using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly WebMedicalContext _webMedicalDbContext;

        public PrescriptionRepository(WebMedicalContext webMedicalDbContext)
        {
            _webMedicalDbContext = webMedicalDbContext;
        }

        public async Task<Prescription> AddAsync(Prescription prescription)
        {
            await _webMedicalDbContext.Prescription.AddAsync(prescription);
            await _webMedicalDbContext.SaveChangesAsync();

            return prescription;
        }

        public async Task<Prescription?> DeleteAsync(int id)
        {
            var prescription = await _webMedicalDbContext.Prescription.FirstOrDefaultAsync(p => p.Id == id);

            if (prescription == null)
            {
                return null;
            }

            prescription.IsActive = false;
            prescription.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
            await _webMedicalDbContext.SaveChangesAsync();

            return prescription;
        }

        public async Task<List<Prescription>> GetAllAsync()
        {
            var prescriptions = await _webMedicalDbContext.Prescription
                .Include(p => p.Appointment)
                .Include(p => p.Diagnosis)
                .Include(p => p.Patient)
                .Include(p => p.PrescribedBy)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.Date)
                .ToListAsync();

            return prescriptions;
        }

        public async Task<List<Prescription>> GetAllByPatientIdAsync(int patientId)
        {
            var prescriptions = await _webMedicalDbContext.Prescription
                .Include(p => p.Appointment)
                .Include(p => p.Diagnosis)
                .Include(p => p.Patient)
                .Include(p => p.PrescribedBy)
                .Where(p => p.PatientId == patientId && p.IsActive)
                .OrderByDescending(p => p.Date)
                .ToListAsync();

            return prescriptions;
        }

        public async Task<Prescription?> GetPrescriptionAsync(int id)
        {
            var prescription = await _webMedicalDbContext.Prescription
                .Include(p => p.Appointment)
                .Include(p => p.Diagnosis)
                .Include(p => p.Patient)
                .Include(p => p.PrescribedBy)
                .FirstOrDefaultAsync(p => p.Id == id);

            return prescription;
        }

        public async Task<Prescription?> UpdateAsync(Prescription prescription)
        {
            var existingPrescription = await _webMedicalDbContext.Prescription
                .FirstOrDefaultAsync(p => p.Id == prescription.Id);

            if (existingPrescription == null)
            {
                return null;
            }

            existingPrescription.AppointmentId = prescription.AppointmentId;
            existingPrescription.DiagnosisId = prescription.DiagnosisId;
            existingPrescription.PatientId = prescription.PatientId;
            existingPrescription.PrescribedById = prescription.PrescribedById;
            existingPrescription.Date = prescription.Date;
            existingPrescription.Notes = prescription.Notes;
            existingPrescription.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
            existingPrescription.IsActive = prescription.IsActive;

            await _webMedicalDbContext.SaveChangesAsync();

            return existingPrescription;
        }
    }
}
