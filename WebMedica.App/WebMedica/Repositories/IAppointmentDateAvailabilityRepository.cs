using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;

namespace WebMedical.Repositories
{
    public interface IAppointmentDateAvailabilityRepository
    {
        Task<List<AppointmentDateAvailability>> GetAllAvailabilitiesAsync();
        Task<List<AppointmentDateSlot>> GetSlotsByDateRangeAsync(DateOnly startDate, DateOnly endDate);
        Task<List<AppointmentDateSlot>> GetAvailableSlotsByDateAsync(DateOnly date);
        Task<int> CreateAvailabilityAsync(AppointmentDateAvailabilityRequest model, string? createdByUserLoginId);
        Task<Appointment?> BookSlotAsync(int slotId, string userLoginId, string? reason, string? notes);
        Task<bool> DeleteSlotAsync(int id);
    }
}
