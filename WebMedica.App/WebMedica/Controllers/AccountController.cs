using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;

namespace WebMedical.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserLogin> _signInManager;
        private readonly UserManager<UserLogin> _userManager;

        public AccountController(SignInManager<UserLogin> signInManager, UserManager<UserLogin> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
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
                ModelState.AddModelError("", "Las contraseñas no coinciden.");
                return View(request);
            }

            var user = await _userManager.GetUserAsync(User);

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