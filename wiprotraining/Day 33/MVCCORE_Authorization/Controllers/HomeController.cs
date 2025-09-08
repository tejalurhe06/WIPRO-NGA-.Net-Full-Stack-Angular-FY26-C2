using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCCoreLoginModule.Controllers
{
    
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Index", "Auth"); // redirect if session expired

            ViewBag.Username = username;
            return View();
        }
    }
}
