using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcProjectSessions.Models;

namespace MyMvcProjectSessions.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    //session demo
    private const string SessionName = "_Name";
    private const string SessionAge = "_Age";


    public IActionResult Index()
    {
        //set session data

        HttpContext.Session.SetString("SessionName", "Tejal");
        HttpContext.Session.SetInt32("SessionAge", 24);

        return View();
    }

    public IActionResult About()
    {
        //Retrive session data

        ViewBag.Name = HttpContext.Session.GetString("SessionName");
        ViewBag.Age = HttpContext.Session.GetInt32("SessionAge");
        ViewData["Message"] = "ASP.NET Core!";
        return View();
    }

    public IActionResult Contact()
    {
        ViewData["Message"] = "Your contact page";
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

    //attribute routing demo
    [Route("number/{id:even}")]
        public IActionResult EvenCheck(int id)
    {
        return Content($"The number {id} is even!");
    }
}
