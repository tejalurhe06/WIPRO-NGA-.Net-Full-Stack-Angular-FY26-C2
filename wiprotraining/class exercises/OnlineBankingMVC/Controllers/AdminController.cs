using Microsoft.AspNetCore.Mvc;

[ServiceFilter(typeof(AuthenticationFilter))] // User must be logged in
[TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { "Admin" })]
public class AdminController : Controller
{
    // Admin Dashboard
    public IActionResult Index()
    {
        return View(); // Views/Admin/Index.cshtml
    }

    // Manage Users
    public IActionResult ManageUsers()
    {
        return View(); // Views/Admin/ManageUsers.cshtml
    }

    // Admin-only test action to throw exception
    public IActionResult Throw()
    {
        throw new Exception("Test exception in AdminController");
    }
}
