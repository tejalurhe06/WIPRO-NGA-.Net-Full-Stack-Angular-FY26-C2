using Microsoft.AspNetCore.Mvc;
using RegistrationValidationSystem.Models;

namespace RegistrationValidationSystem.Controllers
{
    public class RegistrationController : Controller
    {
        // Display Registration Form
        public IActionResult Index()
        {
            return View();
        }

        // Handle Form Submission
        [HttpPost]
        public IActionResult Submit(UserRegistration user)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", user); // return errors to view
            }

            TempData["Message"] = "Registration successful!";
            return RedirectToAction("Index");
        }
    }
}
