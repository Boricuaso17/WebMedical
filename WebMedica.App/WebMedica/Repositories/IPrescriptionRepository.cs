using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public interface IPrescriptionRepository
    {

        Task<IEnumerable<Prescription>> GetAllAsync();
        Task<Prescription> GetPrescriptionAsync(int id);
        Task<Prescription> AddAsync(Prescription prescription);
        Task<Prescription> UpdateAsync(Prescription prescription);
        Task<Prescription> DeleteAsync(int id);
    }
}
