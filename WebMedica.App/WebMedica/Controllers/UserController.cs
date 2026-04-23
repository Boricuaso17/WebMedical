using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;
using WebMedical.Repositories;

namespace WebMedical.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<AddUserRequest> _passwordHasher;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<AddUserRequest>();
        }

        [HttpGet]
        public async Task<IActionResult> Index(User user)
        {
            var userSearched = await _userRepository.GetUserByNameAsync(user.Name);

            var userFound = new AddUserRequest
            {
                Guid = userSearched.Guid,
                SocialSecurityNumber = userSearched.SocialSecurityNumber,
                Name = userSearched.Name,
                MiddleName = userSearched.MiddleName,
                LastName = userSearched.LastName,
                LastName2 = userSearched.LastName2,
                DateOfBirth = userSearched.DateOfBirth,
                Phone = userSearched.Phone,
                FisicalAddress = userSearched.FisicalAddress,
                FisicalAddressLine2 = userSearched.FisicalAddressLine2,
                Town = userSearched.Town,
                State = userSearched.State,
                Zipcode = userSearched.Zipcode,
                PostalAddress = userSearched.PostalAddress,
                PostalAddressLine2 = userSearched.PostalAddressLine2,
                Username = userSearched.Username,
                Password = userSearched.Password,
                Email = userSearched.Email,
                IsActive = userSearched.IsActive
            };

            return RedirectToAction("Record", "Home", userFound);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserRequest user)
        {
            try
            {
                var newUser = new User
                {
                    SocialSecurityNumber = user.SocialSecurityNumber,
                    Guid = Guid.NewGuid(),
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
                    Username = user.Username,
                    Password = _passwordHasher.HashPassword(user, user.Password),
                    Email = user.Email,
                    IsRegister = true,
                    IsActive = user.IsActive
                };

                await _userRepository.AddSync(newUser);

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("Add");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid guid)
        {
            ViewBag.Action = "Edit";

            var user = await _userRepository.GetUserAsync(guid);

            var editUser = new AddUserRequest()
            {
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
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                IsActive = true
            };

            return View("Add", editUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddUserRequest user)
        {
            var editedUser = new User
            {
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
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                IsRegister = true,
                IsActive = true
            };

            var userEdited = await _userRepository.UpdateAsync(editedUser);

            return RedirectToAction("Record", "Home", userEdited);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid guid)
        {
            User user = await _userRepository.DeleteAsync(guid);
            return RedirectToAction("Record", "Home");
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
                var user = new User
                {
                    Password = request.Password,
                };

                var editedUser = _userRepository.UpdateAsync(user);

                return View();
            }

            return View();

        }

    }
}
