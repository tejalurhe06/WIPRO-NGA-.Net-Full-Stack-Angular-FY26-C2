using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Data;
using TaskManagementApp.Models;
using System.Linq;

namespace TaskManagementApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ManageTasks()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }
    }
}