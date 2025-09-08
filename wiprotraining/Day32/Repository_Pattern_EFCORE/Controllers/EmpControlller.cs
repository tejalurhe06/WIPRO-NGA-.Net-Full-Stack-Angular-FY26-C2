
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternEFCore.Models;
using RepositoryPatternEFCore.Repository;



namespace RepositoryPatternEFCore.Controllers
{
    public class EmpController : Controller
    {
        private readonly IEmpRepository _empRepository;

        // public EmpController(Models.EmpContext Context)
        // {
        //     _empRepository = new EmpRepository(Context);
        // }
        public EmpController(IEmpRepository empRepository)
        {
            _empRepository = empRepository;
        }

        public ActionResult Index()
        {
            var model = _empRepository.GetAllEmployees();
            return View(model);
        }

        public ActionResult AddEmployee()
        {
            if (TempData["Failed"] != null)
            {
                ViewBag.Failed = "Add Employee Failed";
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Emp model)
        {
            if (ModelState.IsValid)
            {
                int result = _empRepository.AddEmployee(model);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Emp");
                }
                else
                {
                    TempData["Failed"] = "Add Employee Failed";
                    return RedirectToAction("AddEmployee", "Emp");
                }
            }
            return View();
        }

        public ActionResult EditEmployee(int id)
        {
            if (TempData["Failed"] != null)
            {
                ViewBag.Failed = "Update Employee Failed";
            }
            var model = _empRepository.GetEmployeeById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditEmployee(Emp model)
        {
            if (ModelState.IsValid)
            {
                int result = _empRepository.UpdateEmployee(model);
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Employee updated successfully";
                    return RedirectToAction("Index", "Emp");
                }
                else
                {
                    TempData["Failed"] = "Update Employee Failed";
                    return RedirectToAction("EditEmployee", "Emp", new { id = model.EmpId });
                }
            }
            return View();
        }

        public ActionResult DeleteEmployee(int id)
        {
            var model = _empRepository.GetEmployeeById(id);
            if (TempData["Failed"] != null)
            {
                ViewBag.Failed = "Delete Employee Failed";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteEmployee(Emp model)
        {
            if (ModelState.IsValid)
            {
                int result = _empRepository.DeleteEmployee(model.EmpId);
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Employee deleted successfully";
                    return RedirectToAction("Index", "Emp");
                }
                else
                {
                    TempData["Failed"] = "Delete Employee Failed";
                    return RedirectToAction("DeleteEmployee", "Emp", new { id = model.EmpId });
                }
            }
            return View(model);
        }

    }
}
