using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMedical.Models;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        [HttpGet]
        public async Task<IActionResult> Record(AddUserRequest user)
        {
            // var editUser = await _userRepository.GetUserAsync(user.Guid);

            return View(user);
        }
    }
}
