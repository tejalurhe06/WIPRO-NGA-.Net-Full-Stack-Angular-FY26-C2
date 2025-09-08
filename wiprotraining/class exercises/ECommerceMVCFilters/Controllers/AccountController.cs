using Microsoft.AspNetCore.Mvc;
using ECommerceMVCFilters.Models;
using Microsoft.AspNetCore.Http;

public class AccountController : Controller
{
    // Simulated in-memory users
    private static List<User> users = new List<User>
    {
        new User { Id = 1, Username = "tejal", Password = "1234" },
        new User { Id = 2, Username = "admin", Password = "admin" }
    };

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            HttpContext.Session.SetString("User", user.Username);
            return RedirectToAction("Index", "Product");
        }
        ViewBag.Error = "Invalid credentials!";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("User");
        return RedirectToAction("Login");
    }

    public IActionResult FakeLogin(string user = "tejal")
    {
        HttpContext.Session.SetString("User", user);
        return RedirectToAction("Index", "Product");
    }
}
