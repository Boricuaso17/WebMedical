
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMedical.Models.Domain;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    [Authorize(Roles = "SuperAdmin,AgencyAdmin")]
    public class MedicationController : Controller
    {
        private readonly IMedicationRepository _medicationRepository;

        public MedicationController(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Medication model)
        {

            var medication = await _medicationRepository.AddMedicationAsync(model);

            TempData["SuccessMessage"] = $"Medication {model.Name} was added successfuly";
            TempData["CrudAlertType"] = "create";
            TempData["CrudAlertTitle"] = "Medication created";
            return RedirectToAction("Catalog");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var medication = await _medicationRepository.GetMedicationAsync(id);

            ViewBag.Action = "Edit";
            return View("Add", medication);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Medication model)
        {
            var medication = await _medicationRepository.UpdateMedicationAsync(model);

            TempData["SuccessMessage"] = $"Medication {medication.Name} was succesfuly edited";
            TempData["CrudAlertType"] = "update";
            TempData["CrudAlertTitle"] = "Medication updated";
            return RedirectToAction("Catalog");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var medication = await _medicationRepository.DeleteMedicationAsync(id);
            TempData["SuccessMessage"] = $"Medication {medication.Name} was deleted successfully";
            TempData["CrudAlertType"] = "delete";
            TempData["CrudAlertTitle"] = "Medication deleted";
            return RedirectToAction("Catalog");
        }

        [HttpGet]
        public async Task<IActionResult> Catalog()
        {

            var meditcationList = await _medicationRepository.GetAllAsync();

            return View(meditcationList);
        }
    }
}

