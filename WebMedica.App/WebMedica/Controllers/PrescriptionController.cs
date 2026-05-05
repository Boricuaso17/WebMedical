using Microsoft.AspNetCore.Mvc;
using WebMedical.Models.Domain;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionController(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Prescription prescription)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Prescription prescription)
        {
            return View();
        }

    }
}
