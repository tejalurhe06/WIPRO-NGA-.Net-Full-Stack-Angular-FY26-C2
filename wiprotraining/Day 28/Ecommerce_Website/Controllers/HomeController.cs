using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Website.Models;

namespace Ecommerce_Website.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index1()
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

    public static List<Product> _products = new()
        {
            new Product{ Id=1, Name="Laptop", Price=50000, ImageUrl="/images/products/laptop.jpg"},
            new Product{ Id=2, Name="Phone", Price=20000, ImageUrl= "/images/products/mobile.jpg"},
            new Product{ Id=3, Name="Headphones", Price=2000, ImageUrl="/images/products/headphones.jpg"}
        };

    public IActionResult Index()
    {
        var username = HttpContext.Session.GetString("username");
        if (string.IsNullOrEmpty(username))
            return RedirectToAction("Login", "Account"); // force login
        return View(_products);
    }
}
