using Microsoft.EntityFrameworkCore;
using WebMedical.Data;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;

namespace WebMedical.Repositories
{
    public class AppointmentDateAvailabilityRepository : IAppointmentDateAvailabilityRepository
    {
        private const int IntervalMinutes = 30;
        private readonly WebMedicalContext _webMedicalDbContext;

        public AppointmentDateAvailabilityRepository(WebMedicalContext webMedicalDbContext)
        {
            _webMedicalDbContext = webMedicalDbContext;
        }

        public async Task<List<AppointmentDateAvailability>> GetAllAvailabilitiesAsync()
        {
            return await _webMedicalDbContext.AppointmentDateAvailability
                .OrderByDescending(a => a.StartDate)
                .ThenBy(a => a.StartTime)
                .ToListAsync();
        }

        public async Task<List<AppointmentDateSlot>> GetSlotsByDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _webMedicalDbContext.AppointmentDateSlot
                .Include(s => s.Appointment)
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .OrderBy(s => s.Date)
                .ThenBy(s => s.Time)
                .ToListAsync();
        }

        public async Task<List<AppointmentDateSlot>> GetAvailableSlotsByDateAsync(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var now = TimeOnly.FromDateTime(DateTime.Now);

            if (date < today)
            {
                return new List<AppointmentDateSlot>();
            }

            var query = _webMedicalDbContext.AppointmentDateSlot
                .Where(s => !s.IsBooked && s.Date == date);

            if (date == today)
            {
                query = query.Where(s => s.Time > now);
            }

            return await query
                .OrderBy(s => s.Time)
                .ToListAsync();
        }

        public async Task<int> CreateAvailabilityAsync(AppointmentDateAvailabilityRequest model, string? createdByUserLoginId)
        {
            ValidateAvailability(model);

            var selectedDays = model.SelectedDays.Distinct().OrderBy(d => d).ToList();
            var availability = new AppointmentDateAvailability
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                SelectedDays = string.Join(",", selectedDays.Select(d => d.ToString())),
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                IntervalMinutes = IntervalMinutes,
                CreatedByUserLoginId = createdByUserLoginId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _webMedicalDbContext.AppointmentDateAvailability.AddAsync(availability);
            await _webMedicalDbContext.SaveChangesAsync();

            var slots = await BuildNewSlotsAsync(availability, selectedDays);

            if (slots.Count > 0)
            {
                await _webMedicalDbContext.AppointmentDateSlot.AddRangeAsync(slots);
                await _webMedicalDbContext.SaveChangesAsync();
            }

            return slots.Count;
        }

        public async Task<Appointment?> BookSlotAsync(int slotId, string userLoginId, string? reason, string? notes)
        {
            await using var transaction = await _webMedicalDbContext.Database.BeginTransactionAsync();

            var slot = await _webMedicalDbContext.AppointmentDateSlot
                .FirstOrDefaultAsync(s => s.Id == slotId);

            if (slot == null || slot.IsBooked || IsPastSlot(slot.Date, slot.Time))
            {
                return null;
            }

            var bookedRows = await _webMedicalDbContext.AppointmentDateSlot
                .Where(s => s.Id == slotId && !s.IsBooked)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(s => s.IsBooked, true));

            if (bookedRows == 0)
            {
                return null;
            }

            var appointment = new Appointment
            {
                Date = slot.Date,
                Time = slot.Time,
                Reason = string.IsNullOrWhiteSpace(reason) ? "Patient appointment" : reason.Trim(),
                Notes = notes ?? string.Empty,
                UserLoginId = userLoginId
            };

            await _webMedicalDbContext.Appointment.AddAsync(appointment);
            await _webMedicalDbContext.SaveChangesAsync();

            slot.AppointmentId = appointment.Id;

            await _webMedicalDbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return appointment;
        }

        public async Task<bool> DeleteSlotAsync(int id)
        {
            var slot = await _webMedicalDbContext.AppointmentDateSlot.FirstOrDefaultAsync(s => s.Id == id);

            if (slot == null || slot.IsBooked || IsPastSlot(slot.Date, slot.Time))
            {
                return false;
            }

            _webMedicalDbContext.AppointmentDateSlot.Remove(slot);
            await _webMedicalDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<int> DeleteSlotsAsync(IEnumerable<int> ids)
        {
            var slotIds = ids.Distinct().ToList();

            if (slotIds.Count == 0)
            {
                return 0;
            }

            var slots = await _webMedicalDbContext.AppointmentDateSlot
                .Where(s => slotIds.Contains(s.Id))
                .ToListAsync();

            var deletableSlots = slots
                .Where(s => !s.IsBooked && !IsPastSlot(s.Date, s.Time))
                .ToList();

            if (deletableSlots.Count == 0)
            {
                return 0;
            }

            _webMedicalDbContext.AppointmentDateSlot.RemoveRange(deletableSlots);
            await _webMedicalDbContext.SaveChangesAsync();

            return deletableSlots.Count;
        }

        private async Task<List<AppointmentDateSlot>> BuildNewSlotsAsync(
            AppointmentDateAvailability availability,
            List<DayOfWeek> selectedDays)
        {
            var newSlots = new List<AppointmentDateSlot>();
            var existingSlots = await _webMedicalDbContext.AppointmentDateSlot
                .Where(s => s.Date >= availability.StartDate && s.Date <= availability.EndDate)
                .Select(s => new { s.Date, s.Time, s.ProviderUserLoginId })
                .ToListAsync();

            var existingKeys = existingSlots
                .Select(s => GetSlotKey(s.Date, s.Time, s.ProviderUserLoginId))
                .ToHashSet();

            for (var date = availability.StartDate; date <= availability.EndDate; date = date.AddDays(1))
            {
                if (!selectedDays.Contains(date.DayOfWeek))
                {
                    continue;
                }

                for (var time = availability.StartTime; time < availability.EndTime; time = time.AddMinutes(IntervalMinutes))
                {
                    if (IsPastSlot(date, time))
                    {
                        continue;
                    }

                    var key = GetSlotKey(date, time, null);

                    if (existingKeys.Contains(key))
                    {
                        continue;
                    }

                    existingKeys.Add(key);
                    newSlots.Add(new AppointmentDateSlot
                    {
                        Date = date,
                        Time = time,
                        IsBooked = false,
                        AppointmentDateAvailabilityId = availability.Id,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            return newSlots;
        }

        private static void ValidateAvailability(AppointmentDateAvailabilityRequest model)
        {
            if (model.EndDate < model.StartDate)
            {
                throw new InvalidOperationException("End date cannot be before start date.");
            }

            if (model.EndTime <= model.StartTime)
            {
                throw new InvalidOperationException("End time must be after start time.");
            }

            if (model.SelectedDays.Count == 0)
            {
                throw new InvalidOperationException("Select at least one day.");
            }

            if (!IsValidHalfHour(model.StartTime) || !IsValidHalfHour(model.EndTime))
            {
                throw new InvalidOperationException("Times must use only :00 or :30 minutes.");
            }
        }

        private static bool IsValidHalfHour(TimeOnly time)
        {
            return time.Minute == 0 || time.Minute == 30;
        }

        private static bool IsPastSlot(DateOnly date, TimeOnly time)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var now = TimeOnly.FromDateTime(DateTime.Now);

            return date < today || date == today && time <= now;
        }

        private static string GetSlotKey(DateOnly date, TimeOnly time, string? providerUserLoginId)
        {
            return $"{date:yyyy-MM-dd}|{time:HH:mm}|{providerUserLoginId ?? string.Empty}";
        }
    }
}
