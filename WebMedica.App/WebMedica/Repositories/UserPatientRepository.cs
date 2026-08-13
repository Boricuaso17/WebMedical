using Microsoft.EntityFrameworkCore;
using WebMedical.Data;
using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public class UserPatientRepository : IUserPatientRepository
    {
        private readonly WebMedicalContext _webMedicalDbContext;

        public UserPatientRepository(WebMedicalContext webMedicalDbContext)
        {
            _webMedicalDbContext = webMedicalDbContext;
        }

        public async Task<List<Appointment>> GetUserAppointmentsAsync(string userLoginId)
        {
            var appointments = await _webMedicalDbContext.Appointment
                .Where(a => a.UserLoginId == userLoginId)
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            return appointments;
        }
    }
}
