using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMedical.Controllers
{
    [Authorize(Roles = "User")]
    public class UserPatientController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

