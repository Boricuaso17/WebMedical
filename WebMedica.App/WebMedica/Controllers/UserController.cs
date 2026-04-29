using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMedical.Models.Domain;
using WebMedical.Models.ViewModel;
using WebMedical.Repositories;
using WebMedical.Enum;

namespace WebMedical.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UserLogin> _userManager;

        public UserController(IUserRepository userRepository, UserManager<UserLogin> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;   
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid guid)
        {
            var user = await _userRepository.GetUserAsync(guid);

            return View(ToAddUserRequest(user));
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserRequest model)
        {
            try
            {
                var userLogin = new UserLogin
                {
                    UserName = model.Username,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(userLogin, model.Password);

                if (!result.Succeeded)
                {
                    return View(model);
                }

                await _userRepository.AddSync(ToUserProfile(model));
                await _userManager.AddToRoleAsync(userLogin, ((Roles)model.Role).ToString());

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

            var addUserRequest = ToAddUserRequest(user);

            return View("Add", addUserRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddUserRequest model)
        {
            var userProfile = await _userRepository.UpdateAsync(ToUserProfile(model));
            await UpdateUserRoleAsync(model.Guid, model.Role);

            TempData["SuccessMessage"] = $"The user {userProfile.Name} was successfully updated";

            return RedirectToAction("Edit", userProfile.Guid);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var user = await _userRepository.DeleteAsync(guid);
            return RedirectToAction("Index", "Home");
        }

        public static AddUserRequest ToAddUserRequest(UserProfile model)
        {
            var user = new AddUserRequest()
            {
                Id = model.Id,
                Guid = model.Guid,
                SocialSecurityNumber = model.SocialSecurityNumber,
                Name = model.Name,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                LastName2 = model.LastName2,
                DateOfBirth = model.DateOfBirth,
                Phone = model.Phone,
                FisicalAddress = model.FisicalAddress,
                FisicalAddressLine2 = model.FisicalAddressLine2,
                Town = model.Town,
                State = model.State,
                Zipcode = model.Zipcode,
                PostalAddress = model.PostalAddress,
                PostalAddressLine2 = model.PostalAddressLine2,
                IsActive = true
            };

            return user;
        }

        public static UserProfile ToUserProfile(AddUserRequest model)
        {
            var user = new UserProfile
            {
                Id = model.Id,
                Guid = model.Guid,
                SocialSecurityNumber = model.SocialSecurityNumber,
                Name = model.Name,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                LastName2 = model.LastName2,
                DateOfBirth = model.DateOfBirth,
                Phone = model.Phone,
                FisicalAddress = model.FisicalAddress,
                FisicalAddressLine2 = model.FisicalAddressLine2,
                Town = model.Town,
                State = model.State,
                Zipcode = model.Zipcode,
                PostalAddress = model.PostalAddress,
                PostalAddressLine2 = model.PostalAddressLine2,
                IsRegister = true,
                IsActive = true
            };

            return user;
        }

        public async Task<IActionResult> UpdateUserRoleAsync(Guid guid, int role)
        {
            var user = await _userManager.FindByIdAsync(guid.ToString());

            if (user == null)
                return NotFound();

            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (currentRole != null)
            {
                await _userManager.RemoveFromRoleAsync(user, currentRole);
            }

            var result = await _userManager.AddToRoleAsync(user, ((Roles)role).ToString());

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(errors);
            }

            TempData["SuccessMessage"] = $"The user {user.UserName} was successfully updated!";

            return RedirectToAction("Add");
        }

       
    }
}
