using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public interface IPrescriptionRepository
    {
        Task<List<Prescription>> GetAllAsync();
        Task<List<Prescription>> GetAllByPatientIdAsync(int patientId);
        Task<Prescription?> GetPrescriptionAsync(int id);
        Task<Prescription> AddAsync(Prescription prescription);
        Task<Prescription?> UpdateAsync(Prescription prescription);
        Task<Prescription?> DeleteAsync(int id);
    }
}
