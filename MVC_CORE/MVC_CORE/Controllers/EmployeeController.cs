using Microsoft.AspNetCore.Mvc;
using MVC_CORE.Models;
namespace MVC_CORE.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult details()
        {
            Employe e1 = new Employe
            {
                empid = 101,
                firstname = "Tejal",
                lastname = "Urhe",
                city = "Rahuri",
            };
            return View(e1);
        }

        public IActionResult viewdemo()
        {
            Employe e1 = new Employe
            {
                empid = 201,
                firstname = "Kanishka",
                lastname = "Borole",
                city = "Nashik",
            };
            return View(e1);
        }

        public IActionResult listEmployee()
        {
            return View();
        }

        public IActionResult editEmploye()
        {
            Employe e1 = new Employe
            {
                empid = 201,
                firstname = "Kanishka",
                lastname = "Borole",
                city = "Nashik",
            };
            return View(e1);
        }
        public IActionResult createEmploye()
        {
            return View();
        }

        public IActionResult deleteEmploye()
        { 
            return View();
        }


    }
}
