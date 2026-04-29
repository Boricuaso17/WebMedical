using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;

namespace WebMedical.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserLogin> _signInManager;

        public AccountController(SignInManager<UserLogin> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordRequest request)
        {

            if (request != null)
            {

                //var editedUser = _userRepository.UpdateAsync(model);

                return View();
            }

            return View();

        }
    }
}
