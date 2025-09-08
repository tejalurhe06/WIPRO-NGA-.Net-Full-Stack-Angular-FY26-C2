using Microsoft.AspNetCore.Mvc;

namespace Core_MVC_Demo.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TagHelper()    
        {
            return View();
        }

        public IActionResult RegistrationPage()
        {
            return View();
        }


    }
}
