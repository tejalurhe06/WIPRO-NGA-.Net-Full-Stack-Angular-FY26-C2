using Microsoft.AspNetCore.Mvc;
namespace Core_MVC_Demo.Controllers
{
    public class WiproController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TagHelperDemo()
        {
            return View();
        }

        public IActionResult std_htmlhelper_demo()
        {
            return View();
        }

        public IActionResult std_htmlhelper_using_model()
        {
            return View();
        }

        public IActionResult clientside_scripting_demo()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
