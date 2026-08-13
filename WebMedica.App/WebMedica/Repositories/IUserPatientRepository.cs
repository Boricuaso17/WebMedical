using WebMedical.Data;
using WebMedical.Models.Domain;

namespace WebMedical.Repositories


{
    public interface IUserPatientRepository
    {

        Task<List<Appointment>> GetUserAppointmentsAsync(string userId);
    }
}
