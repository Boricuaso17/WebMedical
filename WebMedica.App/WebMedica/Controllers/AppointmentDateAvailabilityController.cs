using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    [Authorize(Roles = "SuperAdmin,AgencyAdmin")]
    public class AppointmentDateAvailabilityController : Controller
    {
        private readonly IAppointmentDateAvailabilityRepository _availabilityRepository;
        private readonly UserManager<UserLogin> _userManager;

        public AppointmentDateAvailabilityController(
            IAppointmentDateAvailabilityRepository availabilityRepository,
            UserManager<UserLogin> userManager)
        {
            _availabilityRepository = availabilityRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            return View(new AppointmentDateAvailabilityRequest
            {
                StartDate = today,
                EndDate = today.AddDays(4),
                StartTime = new TimeOnly(8, 0),
                EndTime = new TimeOnly(17, 0),
                SelectedDays = new List<DayOfWeek>
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Friday
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentDateAvailabilityRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);
                var slotsCreated = await _availabilityRepository.CreateAvailabilityAsync(model, user?.Id);

                TempData["SuccessMessage"] = $"{slotsCreated} available appointment slots created successfully.";
                TempData["CrudAlertType"] = "create";
                TempData["CrudAlertTitle"] = "Availability created";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSlotsByDateRange(DateOnly startDate, DateOnly endDate)
        {
            var slots = await _availabilityRepository.GetSlotsByDateRangeAsync(startDate, endDate);

            return Json(slots.Select(s => new
            {
                s.Id,
                s.Date,
                s.Time,
                s.IsBooked,
                s.AppointmentId
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            var deleted = await _availabilityRepository.DeleteSlotAsync(id);

            if (!deleted)
            {
                TempData["SuccessMessage"] = "The slot could not be deleted because it is booked, expired, or does not exist.";
                TempData["CrudAlertType"] = "delete";
                TempData["CrudAlertTitle"] = "Slot not deleted";
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = "Available slot deleted successfully.";
            TempData["CrudAlertType"] = "delete";
            TempData["CrudAlertTitle"] = "Slot deleted";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSlots(int[] slotIds)
        {
            var deletedCount = await _availabilityRepository.DeleteSlotsAsync(slotIds);

            if (deletedCount == 0)
            {
                TempData["SuccessMessage"] = "No slots were deleted because none were selected or the selected slots are booked/expired.";
                TempData["CrudAlertType"] = "delete";
                TempData["CrudAlertTitle"] = "Slots not deleted";
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = $"{deletedCount} available slots deleted successfully.";
            TempData["CrudAlertType"] = "delete";
            TempData["CrudAlertTitle"] = "Slots deleted";
            return RedirectToAction("Index");
        }
    }
}
