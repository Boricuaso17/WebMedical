using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using WebMedical.Models.Domain;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {

            ViewBag.Action = "Add";
            return View();
        }
         [HttpPost]
         public async Task<IActionResult> Add(Appointment model)
         {
             if (!ModelState.IsValid)
             {
                

                 return RedirectToAction("Add");
             }

             var appointment = await _appointmentRepository.AddAsync(model);

            TempData["SuccessMessage"] = $"Appointment on {appointment.Date} added successfully";
            TempData["CrudAlertType"] = "create";
            TempData["CrudAlertTitle"] = "Appointment created";
            return RedirectToAction("Index");
         }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentAsync(id);

            ViewBag.Action = "Edit";

            return View("Add", appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Appointment model)
        {

            var appointment = await _appointmentRepository.UpdateAsync(model);

            TempData["SuccessMessage"] = $"Appointment on {appointment.Date} updated successfully";
            TempData["CrudAlertType"] = "update";
            TempData["CrudAlertTitle"] = "Appointment updated";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _appointmentRepository.DeleteAsync(id);

            TempData["SuccessMessage"] = $"Appointment on {appointment.Date} deleted successfully";
            TempData["CrudAlertType"] = "delete";
            TempData["CrudAlertTitle"] = "Appointment deleted";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetAppointmentsByDateRange(DateOnly startDate, DateOnly endDate)
        {
            var appointments = await _appointmentRepository
                .GetAppointmentsByDateAsync(startDate, endDate);

            return Json(appointments);
        }
    }
}
