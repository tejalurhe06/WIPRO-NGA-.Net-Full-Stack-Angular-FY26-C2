using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingDemo.Controllers
{
    public class UsersController : Controller
    {
        // URL: /Users/tejal/Orders
        public IActionResult Orders(string username)
        {
            ViewBag.Username = username;
            return View();
        }
    }
}
