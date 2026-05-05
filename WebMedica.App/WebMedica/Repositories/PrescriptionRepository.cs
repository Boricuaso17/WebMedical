using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        public Task<Prescription> AddAsync(Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public Task<Prescription> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Prescription>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Prescription> GetPrescriptionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Prescription> UpdateAsync(Prescription prescription)
        {
            throw new NotImplementedException();
        }
    }
}
