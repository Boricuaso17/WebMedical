using Microsoft.AspNetCore.Mvc;
using WebMedical.Models;

namespace WebMedical.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
