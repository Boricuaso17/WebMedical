using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    [Authorize(Roles = "SuperAdmin,AgencyAdmin")]
    public class UserDiagnosisController : Controller
    {

        private readonly IUserDiagnosisRepository _userDiagnosisRepository;
        private readonly IDiagnosisRepository _diagnosisRepository;
        public UserDiagnosisController(IUserDiagnosisRepository userDiagnosisRepository, IDiagnosisRepository diagnosisRepository)
        {
            _userDiagnosisRepository = userDiagnosisRepository;
            _diagnosisRepository = diagnosisRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int userId)
        {

            var diagnosesList = await _userDiagnosisRepository.GetAllUserDiagnosisAsync(userId);

            ViewBag.UserId = userId;

            return View(diagnosesList);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int userId)
        {
            var diagnosisList = await _diagnosisRepository.GetAllDiagnosisAsync();

            var userDiagnosis = new AddUserDiagnosisRequest { UserId = userId,
                DiagnosisList = diagnosisList.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
            };

            ViewBag.Action = "Add";

            return View(userDiagnosis);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserDiagnosisRequest model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userDiagnosis = new UserDiagnosis
            {
                Status = model.Status,
                Notes = model.Notes,
                UserId = model.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = userId,
                DiagnosisId = model.DiagnosisId
            };

            userDiagnosis = await _userDiagnosisRepository.AddUserDiagnosisAsync(userDiagnosis);
            TempData["SuccessMessage"] = "Diagnosis was added to the user successfully";
            TempData["CrudAlertType"] = "create";
            TempData["CrudAlertTitle"] = "User diagnosis created";
            return RedirectToAction("Index", new { userId = model.UserId });
        }

        [HttpPost]
        public async Task<IActionResult> Edit()
        {

            return View();
        }
    }
}

