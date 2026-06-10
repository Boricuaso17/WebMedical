using Microsoft.EntityFrameworkCore;
using WebMedical.Data;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;

namespace WebMedical.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly WebMedicalContext _webMedicalDbContext;

        public AppointmentRepository(WebMedicalContext webMedicalDbContext)
        {
            _webMedicalDbContext = webMedicalDbContext;
        }

        public async Task<AddAppointmentRequest> AddAsync(AddAppointmentRequest appointment)
        {
            await _webMedicalDbContext.Appointment.AddAsync(ToAppointment(appointment));
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
            var appointments = await _webMedicalDbContext.Appointment
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            return appointments;
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

        public async Task<AddAppointmentRequest> UpdateAsync(AddAppointmentRequest appointment)
        {
             _webMedicalDbContext.Appointment.Update(ToAppointment(appointment));
            await _webMedicalDbContext.SaveChangesAsync();

            return appointment;
        }

        public Appointment ToAppointment(AddAppointmentRequest model)
        {

            var appointment = new Appointment
            {
                Id = model.Id,
                Date = model.Date,
                Time = model.Time,
                Reason = model.Reason,
                Notes = model.Notes,
                UserLoginId = model.UserLoginId
            };

            return appointment;
        }
    }
}
