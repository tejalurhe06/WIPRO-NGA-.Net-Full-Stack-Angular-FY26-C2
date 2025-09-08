using Microsoft.AspNetCore.Mvc;

[ServiceFilter(typeof(AuthenticationFilter))] // Apply auth filter
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Throw()
    {
        throw new Exception("Test exception from Product/Throw");
    }

    public IActionResult Details(int id)
    {
        return View();
    }
}
