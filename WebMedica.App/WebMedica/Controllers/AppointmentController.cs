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
                

                 return View("Index");
             }

             var appointment = await _appointmentRepository.AddAsync(model);

            return View(appointment);
        }

    }
}
