using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public interface IMedicationRepository
    {

        Task<IEnumerable<Medication>> GetAllAsync();

        Task<Medication> GetMedicationAsync(int id);

        Task<Medication> AddMedicationAsync(Medication medication);

        Task<Medication> UpdateMedicationAsync(Medication medication);

        Task<Medication> DeleteMedicationAsync(int id);


    }
}
