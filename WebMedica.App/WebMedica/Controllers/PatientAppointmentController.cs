using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    [Authorize(Roles = "User")]
    public class PatientAppointmentController : Controller
    {
        private readonly IAppointmentDateAvailabilityRepository _availabilityRepository;
        private readonly UserManager<UserLogin> _userManager;

        public PatientAppointmentController(
            IAppointmentDateAvailabilityRepository availabilityRepository,
            UserManager<UserLogin> userManager)
        {
            _availabilityRepository = availabilityRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateOnly? selectedDate)
        {
            var date = selectedDate ?? DateOnly.FromDateTime(DateTime.Now);
            var model = new PatientAppointmentRequest
            {
                SelectedDate = date,
                AvailableSlots = await _availabilityRepository.GetAvailableSlotsByDateAsync(date)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableSlots(DateOnly date)
        {
            var slots = await _availabilityRepository.GetAvailableSlotsByDateAsync(date);

            return Json(slots.Select(s => new
            {
                s.Id,
                s.Date,
                s.Time
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int slotId, string? reason, string? notes)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            var appointment = await _availabilityRepository.BookSlotAsync(slotId, user.Id, reason, notes);

            if (appointment == null)
            {
                TempData["SuccessMessage"] = "That appointment time is no longer available.";
                TempData["CrudAlertType"] = "delete";
                TempData["CrudAlertTitle"] = "Appointment unavailable";
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = $"Appointment booked for {appointment.Date} at {appointment.Time}.";
            TempData["CrudAlertType"] = "create";
            TempData["CrudAlertTitle"] = "Appointment booked";
            return RedirectToAction("Index", new { selectedDate = appointment.Date });
        }
    }
}
