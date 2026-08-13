using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMedical.Models.Domain;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    [Authorize(Roles = "User")]
    public class UserPatientController : Controller
    {
        private readonly IUserPatientRepository _userPatientRepository;
        private readonly UserManager<UserLogin> _userManager;

        public UserPatientController(
            IUserPatientRepository userPatientRepository,
            UserManager<UserLogin> userManager)
        {
            _userPatientRepository = userPatientRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            var appointments = await _userPatientRepository.GetUserAppointmentsAsync(user.Id);

            return View(appointments);
        }
    }
}

