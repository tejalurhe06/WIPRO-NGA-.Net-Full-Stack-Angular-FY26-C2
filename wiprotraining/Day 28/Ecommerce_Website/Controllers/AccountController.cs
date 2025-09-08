using Microsoft.AspNetCore.Mvc;
using Ecommerce_Website.Data;
namespace Ecommerce_Website.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
