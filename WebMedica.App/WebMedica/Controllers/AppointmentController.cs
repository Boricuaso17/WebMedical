using Microsoft.AspNetCore.Mvc;
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
            return View(appointment);
         }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var appointment = _appointmentRepository.GetAppointmentAsync(id);

            return RedirectToAction("Add", appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Appointment model)
        {

            var appointment = await _appointmentRepository.UpdateAsync(model);

            return RedirectToAction("Add", appointment);
        }

        public IActionResult AppointmentsByDateRange(DateOnly startDate, DateOnly endDate)
        {
            var appointments = _appointmentRepository
                .GetAppointmentsByDateAsync(startDate, endDate);

            return View(appointments);
        }
    }
}
