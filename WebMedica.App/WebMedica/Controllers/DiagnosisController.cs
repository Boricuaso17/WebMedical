using Microsoft.AspNetCore.Mvc;
using WebMedical.Data;
using WebMedical.Models.Domain;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    public class DiagnosisController : Controller
    {

        private readonly IDiagnosisRepository _diagonsisRepository;

        public DiagnosisController(IDiagnosisRepository diagonsisRepository)
        {
            _diagonsisRepository=diagonsisRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                await _diagonsisRepository.AddAsync(diagnosis);
                return RedirectToAction("Index", "Home");
            }
            return View(diagnosis);
        }

        public async Task<IActionResult> Diagnoses(int id)
        {

            var diagnoses = await _diagonsisRepository.GetAllDiagnosisAsync();

            return View(diagnoses);
        }
    }
}
