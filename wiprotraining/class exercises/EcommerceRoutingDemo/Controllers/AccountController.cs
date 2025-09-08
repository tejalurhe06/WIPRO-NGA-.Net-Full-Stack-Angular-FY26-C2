using Microsoft.AspNetCore.Mvc;

namespace EcommerceRoutingDemo.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
