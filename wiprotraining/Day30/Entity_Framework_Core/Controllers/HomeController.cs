using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Entity_Framework_Core.Models;

namespace Entity_Framework_Core.Controllers;

public class HomeController : Controller
{
    /*private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }*/

    private CompanyContext context;

    public HomeController(CompanyContext cc)
    {
        context = cc;
    }

    public IActionResult Index()
    {
        var dept = new Department()
        {
            Name = "Designing"
        };
        context.Entry(dept).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        context.SaveChanges();
        return View();
    }
}
