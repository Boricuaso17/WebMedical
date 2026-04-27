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
            var usersList = new List<UserProfile>();

            if (search.Name != null)
            {
                usersList = await _userRepository.GetUserByNameAsync(search.Name);
            }
            //else
            //{
            //    usersList = await _userRepository.GetUserBySSN(search.SocialSecurityNumber);
            //}

            var usersFound = new List<AddUserRequest> { };

            foreach (var user in usersList)
            {
                var newUser = new AddUserRequest
                {
                    Guid = user.Guid,
                    SocialSecurityNumber = user.SocialSecurityNumber,
                    Name = user.Name,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    LastName2 = user.LastName2,
                    DateOfBirth = user.DateOfBirth,
                    Phone = user.Phone,
                    FisicalAddress = user.FisicalAddress,
                    FisicalAddressLine2 = user.FisicalAddressLine2,
                    Town = user.Town,
                    State = user.State,
                    Zipcode = user.Zipcode,
                    PostalAddress = user.PostalAddress,
                    PostalAddressLine2 = user.PostalAddressLine2,
                    IsActive = user.IsActive
                };
                usersFound.Add(newUser);
            }

            return View(usersFound);
        }
    }
}
