using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OnlineBankingMVC.Models;

public class AccountController : Controller
{
    // Simulated in-memory users
    private static List<User> users = new List<User>
    {
        new User { Id = 1, Username = "tejal", Password = "1234", Role = "Customer" },
        new User { Id = 2, Username = "admin", Password = "admin", Role = "Admin" }
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
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserRole", user.Role);
            return RedirectToAction("Index", "Bank");
        }
        ViewBag.Error = "Invalid credentials!";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    // Optional FakeLogin for testing
    public IActionResult FakeLogin(string user = "tejal")
    {
        var foundUser = users.FirstOrDefault(u => u.Username == user);
        if (foundUser != null)
        {
            HttpContext.Session.SetString("UserId", foundUser.Id.ToString());
            HttpContext.Session.SetString("UserRole", foundUser.Role);
        }
        return RedirectToAction("Index", "Bank");
    }
}
