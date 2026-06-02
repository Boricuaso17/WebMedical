using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    [Authorize(Roles = "SuperAdmin,AgencyAdmin")]
    public class PrescriptionController : Controller
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDiagnosisRepository _diagnosisRepository;
        private readonly UserManager<UserLogin> _userManager;

        public PrescriptionController(
            IPrescriptionRepository prescriptionRepository,
            IAppointmentRepository appointmentRepository,
            IDiagnosisRepository diagnosisRepository,
            UserManager<UserLogin> userManager)
        {
            _prescriptionRepository = prescriptionRepository;
            _appointmentRepository = appointmentRepository;
            _diagnosisRepository = diagnosisRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Record(int patientId)
        {
            var prescriptions = await _prescriptionRepository.GetAllByPatientIdAsync(patientId);

            ViewBag.PatientId = patientId;

            return View(prescriptions);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int patientId)
        {
            var prescribedById = await GetCurrentUserProfileIdAsync();
            var model = await BuildPrescriptionRequestAsync(new AddPrescriptionRequest
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                PatientId = patientId,
                PrescribedById = prescribedById
            });

            ViewBag.Action = "Add";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPrescriptionRequest model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Action = "Add";
                return View(await BuildPrescriptionRequestAsync(model));
            }

            var prescribedById = await GetCurrentUserProfileIdAsync();

            var prescription = new Prescription
            {
                AppointmentId = model.AppointmentId,
                DiagnosisId = model.DiagnosisId,
                PatientId = model.PatientId,
                PrescribedById = prescribedById > 0 ? prescribedById : model.PrescribedById,
                Date = model.Date,
                Notes = model.Notes,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now),
                IsActive = true
            };

            await _prescriptionRepository.AddAsync(prescription);

            TempData["SuccessMessage"] = "Prescription was added successfully";
            TempData["CrudAlertType"] = "create";
            TempData["CrudAlertTitle"] = "Prescription created";
            return RedirectToAction("Record", new { patientId = prescription.PatientId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var prescription = await _prescriptionRepository.GetPrescriptionAsync(id);

            if (prescription == null)
            {
                return NotFound();
            }

            var model = await BuildPrescriptionRequestAsync(ToAddPrescriptionRequest(prescription));

            ViewBag.Action = "Edit";

            return View("Add", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddPrescriptionRequest model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Action = "Edit";
                return View("Add", await BuildPrescriptionRequestAsync(model));
            }

            var prescribedById = await GetCurrentUserProfileIdAsync();
            var prescription = await _prescriptionRepository.UpdateAsync(new Prescription
            {
                Id = model.Id,
                AppointmentId = model.AppointmentId,
                DiagnosisId = model.DiagnosisId,
                PatientId = model.PatientId,
                PrescribedById = prescribedById > 0 ? prescribedById : model.PrescribedById,
                Date = model.Date,
                Notes = model.Notes,
                IsActive = true
            });

            if (prescription == null)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Prescription was updated successfully";
            TempData["CrudAlertType"] = "update";
            TempData["CrudAlertTitle"] = "Prescription updated";
            return RedirectToAction("Record", new { patientId = prescription.PatientId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var prescription = await _prescriptionRepository.DeleteAsync(id);

            if (prescription == null)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Prescription was deleted successfully";
            TempData["CrudAlertType"] = "delete";
            TempData["CrudAlertTitle"] = "Prescription deleted";
            return RedirectToAction("Record", new { patientId = prescription.PatientId });
        }

        private async Task<AddPrescriptionRequest> BuildPrescriptionRequestAsync(AddPrescriptionRequest model)
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
            var diagnoses = await _diagnosisRepository.GetAllDiagnosisAsync();

            model.AppointmentList = appointments.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = $"{a.Date:yyyy-MM-dd} - {a.Reason}"
            });

            model.DiagnosisList = diagnoses.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            });

            return model;
        }

        private async Task<int> GetCurrentUserProfileIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            return user?.UserProfileId ?? 0;
        }

        private static AddPrescriptionRequest ToAddPrescriptionRequest(Prescription prescription)
        {
            return new AddPrescriptionRequest
            {
                Id = prescription.Id,
                AppointmentId = prescription.AppointmentId,
                DiagnosisId = prescription.DiagnosisId,
                PatientId = prescription.PatientId,
                PrescribedById = prescription.PrescribedById,
                Date = prescription.Date,
                Notes = prescription.Notes
            };
        }
    }
}

