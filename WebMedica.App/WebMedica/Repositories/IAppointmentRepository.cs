using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;

namespace WebMedical.Repositories
{
    public interface IAppointmentRepository
    {

        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentAsync(int id);
        Task<AddAppointmentRequest> UpdateAsync(AddAppointmentRequest appointment);
        Task<Appointment> DeleteAsync(int id);
        Task<AddAppointmentRequest> AddAsync(AddAppointmentRequest appointment);
        Task<List<Appointment>> GetAppointmentsByDateAsync(DateOnly startDate, DateOnly endDate);
    }
}
