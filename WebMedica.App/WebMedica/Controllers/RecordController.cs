using Microsoft.AspNetCore.Mvc;
using WebMedical.Models;

namespace WebMedical.Controllers
{
    public class RecordController : Controller
    {
        public IActionResult Index(User model)
        {
            if (model.IsRegister == false)
            {
                return RedirectToAction("Register");
            }

            return View(model);
        }

        public IActionResult 
    }
}
