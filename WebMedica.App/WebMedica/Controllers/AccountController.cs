using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;

namespace WebMedical.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserLogin> _signInManager;
        private readonly UserManager<UserLogin> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            SignInManager<UserLogin> signInManager,
            UserManager<UserLogin> userManager,
            ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            Microsoft.AspNetCore.Identity.SignInResult result;

            try
            {
                result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError(ex, "Unable to connect to the authentication database while signing in user {Username}.", request.Username);
                ModelState.AddModelError(string.Empty, "Unable to connect to the database. Please verify the database connection settings and try again.");
                return View(request);
            }

            if (result.Succeeded)
            {
                var userLogin = await _userManager.FindByNameAsync(request.Username);

                if (userLogin != null)
                {
                    var roles = await _userManager.GetRolesAsync(userLogin);

                    if (roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "UserPatient");
                    }

                    if (roles.Contains("SuperAdmin") || roles.Contains("AgencyAdmin"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult KeepItSignedIn()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {

            if (request.NewPassword != request.ConfirmPassword)
            {
                ModelState.AddModelError("", "Las contrasenas no coinciden.");
                return View(request);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }

            var result = await _userManager.ChangePasswordAsync(
                         user,
                         request.CurrentPassword,
                         request.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(request);
            }

            return RedirectToAction("Index");

        }
    }
}


