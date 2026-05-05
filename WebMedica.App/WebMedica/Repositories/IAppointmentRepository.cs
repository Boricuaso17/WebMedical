using WebMedical.Models.Domain;

namespace WebMedical.Repositories
{
    public interface IAppointmentRepository
    {

        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentAsync(int id);
        Task<Appointment> UpdateAsync(Appointment appointment);
        Task<Appointment> DeleteAsync(int id);
        Task<Appointment> AddAsync(Appointment appointment);
        Task<List<Appointment>> GetAppointmentsByDateAsync(DateOnly startDate, DateOnly endDate);
    }
}
