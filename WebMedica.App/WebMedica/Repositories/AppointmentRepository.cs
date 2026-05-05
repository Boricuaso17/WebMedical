using WebMedical.Data;
using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly WebMedicalContext _webMedicalDbContext;

        public AppointmentRepository(WebMedicalContext webMedicalDbContext)
        {
            _webMedicalDbContext = webMedicalDbContext;
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            await _webMedicalDbContext.Appointment.AddAsync(appointment);
            await _webMedicalDbContext.SaveChangesAsync();

            return appointment;
        }

        public Task<Appointment> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> GetAppointmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetAppointmentsByDateAsync(DateOnly startDate, DateOnly endDate)
        {
           var appointments = _webMedicalDbContext.Appointment
                .Where(a => a.Date >= startDate && a.Date <= endDate)
                .ToList();

            return appointments;
        }

        public Task<Appointment> UpdateAsync(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
