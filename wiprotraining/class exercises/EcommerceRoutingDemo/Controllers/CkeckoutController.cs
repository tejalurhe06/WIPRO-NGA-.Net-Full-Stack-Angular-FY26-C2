using Microsoft.AspNetCore.Mvc;

namespace EcommerceRoutingDemo.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            bool isLoggedIn = false; // Replace with actual login logic

            if (!isLoggedIn)
                return RedirectToAction("Login", "Account"); // Redirect guest to login
            else
                return View("Checkout"); // Show checkout page for logged-in users
        }
    }
}
