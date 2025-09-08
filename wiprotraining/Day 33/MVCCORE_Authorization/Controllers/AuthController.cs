using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCCoreLoginModule.Data;
using MVCCoreLoginModule.Models;

namespace MVCCoreLoginModule.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // Landing page
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // shows login/register options
        }

        // Login page
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
                if (result == PasswordVerificationResult.Success)
                {
                    // Save session
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("Role", user.Role);

                    return RedirectToAction("Dashboard", "Home");
                }
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }

        // Registration page
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.Error = "Username already exists";
                return View();
            }

            var user = new User { Username = username };
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            _context.Users.Add(user);
            _context.SaveChanges();

            // Save session
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Dashboard", "Home");
        }

        // Logout (anyone can call)
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // clear session
            return RedirectToAction("Index");
        }
    }
}
