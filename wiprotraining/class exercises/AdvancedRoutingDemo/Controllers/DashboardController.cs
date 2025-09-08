// AdminDashboardController.cs
using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingDemo.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            // Example dynamic routing
            string userRole = "Admin"; // This could come from Auth system

            if (userRole == "Admin")
                return RedirectToAction("AdminDashboard");
            else
                return RedirectToAction("UserDashboard");
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult UserDashboard()
        {
            return View();
        }
    }
}
