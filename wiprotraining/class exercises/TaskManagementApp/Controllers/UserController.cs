using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Authorization;
using TaskManagementApp.Data;
using TaskManagementApp.Models;
using TaskManagementApp.Models.ViewModels;
using System.Threading.Tasks;

namespace TaskManagementApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> TaskList()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var tasks = await _context.Tasks
                .Where(t => t.UserId == currentUser.Id)
                .ToListAsync();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask(TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var task = new TaskItem
                {
                    Title = model.Title,
                    Description = System.Web.HttpUtility.HtmlEncode(model.Description),
                    IsCompleted = model.IsCompleted,
                    UserId = currentUser.Id
                };

                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(TaskList));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Use the custom policy name
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, task, "EditTask");

            if (!authorizationResult.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
            }

            var model = new TaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = System.Web.HttpUtility.HtmlDecode(task.Description),
                IsCompleted = task.IsCompleted
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTask(TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = await _context.Tasks.FindAsync(model.Id);
                if (task == null)
                {
                    return NotFound();
                }

                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Use the custom policy name
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, task, "EditTask");

                if (!authorizationResult.Succeeded)
                {
                    return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
                }

                task.Title = model.Title;
                task.Description = System.Web.HttpUtility.HtmlEncode(model.Description);
                task.IsCompleted = model.IsCompleted;

                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(TaskList));
            }

            return View(model);
        }
    }
}