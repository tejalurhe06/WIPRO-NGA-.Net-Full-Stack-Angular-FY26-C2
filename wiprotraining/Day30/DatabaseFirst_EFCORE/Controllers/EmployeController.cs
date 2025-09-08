using DatabaseFirst_EFCORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseFirst_EFCORE.Controllers
{
    public class EmployeController : Controller
    {
        private readonly CompanyContext _context;

        public EmployeController(CompanyContext context)
        {
            _context = context;
        }

        //Read
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        // //Create
        // public IActionResult Create()
        // {
        //     return View();
        // }

        // [HttpPost]
        // public IActionResult Create(Employee employee)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Employees.Add(employee);
        //         _context.SaveChanges();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(employee);
        // }

        // //Edit
        // public IActionResult Edit(int id)
        // {
        //     var employee = _context.Employees.Find(id);
        //     if (employee == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(employee);
        // }

        // [HttpPost]
        // public IActionResult Edit(Employee employee)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Employees.Update(employee);
        //         _context.SaveChanges();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(employee);
        // }

        // //Delete
        // public IActionResult Delete(int id)
        // {
        //     var employee = _context.Employees.Find(id);
        //     if (employee == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(employee);
        // }

        // [HttpPost, ActionName("Delete")]
        // public IActionResult DeleteConfirmed(int id)
        // {
        //     var employee = _context.Employees.Find(id);
        //     if (employee != null)
        //     {
        //         _context.Employees.Remove(employee);
        //         _context.SaveChanges();
        //     }
        //     return RedirectToAction(nameof(Index));
        // }

    }
}