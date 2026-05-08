using WebMedical.Data;
using WebMedical.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Appointment?> DeleteAsync(int id)
        {
            var appointment = await _webMedicalDbContext.Appointment.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return null;
            }

            _webMedicalDbContext.Appointment.Remove(appointment);
            await _webMedicalDbContext.SaveChangesAsync();

            return appointment;
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Appointment> GetAppointmentAsync(int id)
        {
            var appointment = await _webMedicalDbContext.Appointment.FirstOrDefaultAsync(a => a.Id == id);

            return appointment;
        }

        public async Task<List<Appointment>> GetAppointmentsByDateAsync(DateOnly startDate, DateOnly endDate)
        {
           var appointments = _webMedicalDbContext.Appointment
                .Where(a => a.Date >= startDate && a.Date <= endDate)
                .ToList();

            return appointments;
        }

        public async Task<Appointment> UpdateAsync(Appointment appointment)
        {
             _webMedicalDbContext.Appointment.Update(appointment);
            await _webMedicalDbContext.SaveChangesAsync();

            return appointment;
        }
    }
}
