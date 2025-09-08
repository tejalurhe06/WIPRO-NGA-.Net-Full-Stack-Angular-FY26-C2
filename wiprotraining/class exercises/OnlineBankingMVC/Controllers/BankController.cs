using Microsoft.AspNetCore.Mvc;

[ServiceFilter(typeof(AuthenticationFilter))] // Requires login for all actions
public class BankController : Controller
{
    // Accessible by all logged-in users
    public IActionResult Index()
    {
        return View(); // Dashboard view
    }

    public IActionResult Transactions()
    {
        return View(); // Transaction list view
    }

    // Admin-only page

    [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { "Admin" })]
    public IActionResult AdminPanel()
    {
        return View(); // Admin management page
    }

    // Action to test GlobalExceptionFilter
    public IActionResult Throw()
    {
        throw new Exception("Test exception in BankController");
    }
}
