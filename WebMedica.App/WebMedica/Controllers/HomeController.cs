using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Emit;
using WebMedical.Enum;
using WebMedical.Models;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    [Authorize(Roles = "SuperAdmin,AgencyAdmin")]
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

        [HttpGet]
        public async Task<IActionResult> SearchUser(AddUserRequest search)
        {
            var usersList = new List<AddUserRequest> { };

            if (HasSearchCriteria(search))
            {
                usersList = await _userRepository.SearchUsersAsync(search);
            }

            return View(usersList);
        }

        private static bool HasSearchCriteria(AddUserRequest search)
        {
            return !string.IsNullOrWhiteSpace(search.Name) ||
                   !string.IsNullOrWhiteSpace(search.MiddleName) ||
                   !string.IsNullOrWhiteSpace(search.LastName) ||
                   !string.IsNullOrWhiteSpace(search.LastName2) ||
                   !string.IsNullOrWhiteSpace(search.Email) ||
                   !string.IsNullOrWhiteSpace(search.SocialSecurityNumber) ||
                   !string.IsNullOrWhiteSpace(search.Phone);
        }
    }
}


