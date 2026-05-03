using Microsoft.EntityFrameworkCore;
using WebMedical.Data;
using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public class MedicationRepository : IMedicationRepository
    {
        private readonly WebMedicalContext _webMedicalDbContext;

        public MedicationRepository(WebMedicalContext webMedicalDbContext)
        {
            _webMedicalDbContext = webMedicalDbContext;
        }

        public async Task<Medication> AddMedicationAsync(Medication medication)
        {
            await _webMedicalDbContext.Medication.AddAsync(medication);
            await _webMedicalDbContext.SaveChangesAsync();

            return medication;
        }

        public async Task<Medication> DeleteMedicationAsync(int id)
        {
            var medication = await _webMedicalDbContext.Medication.FirstOrDefaultAsync(m => m.Id == id);

            if(medication == null)
            {
                return null;
            }

            _webMedicalDbContext.Medication.Remove(medication);
            await _webMedicalDbContext.SaveChangesAsync();

            return medication;
        }

        public async Task<IEnumerable<Medication>> GetAllAsync()
        {
            var medicationList = await _webMedicalDbContext.Medication.ToListAsync();

            return medicationList;
        }

        public async Task<Medication> GetMedicationAsync(int id)
        {
            var medication = await _webMedicalDbContext.Medication.FindAsync(id);

            return medication;
        }

        public async Task<Medication> UpdateMedicationAsync(Medication medication)
        {
                _webMedicalDbContext.Medication.Update(medication);
                await _webMedicalDbContext.SaveChangesAsync();

            return medication;
        }
    }
}
