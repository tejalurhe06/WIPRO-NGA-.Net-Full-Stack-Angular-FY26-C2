using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc_filters;
using MVC_FILTERS.Models;

namespace MVC_FILTERS.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

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
    }

    //filters

    [ServiceFilter(typeof(LogActionFilter))]
    public IActionResult About()
    {
        return View();
    }
}
