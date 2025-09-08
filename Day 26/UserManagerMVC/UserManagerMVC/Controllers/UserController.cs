using Microsoft.AspNetCore.Mvc;
using UserManagerMVC.Models;

namespace UserManagerMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: /User/CreateSimple
        public IActionResult CreateSimple()
        {
            return View();
        }

        // POST: /User/CreateSimple
        [HttpPost]
        public IActionResult CreateSimple(string FirstName, string LastName, int Age)
        {
            ViewBag.FirstName = FirstName;
            ViewBag.LastName = LastName;
            ViewBag.Age = Age;
            return View("DisplaySimple");
        }

        // GET: /User/CreateComplex
        public IActionResult CreateComplex()
        {
            return View();
        }

        // POST: /User/CreateComplex
        [HttpPost]
        public IActionResult CreateComplex(User user)
        {
            if (ModelState.IsValid)
            {
                return View("DisplayComplex", user);
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
