using Microsoft.AspNetCore.Mvc;
using ECommerceMVCFilters.Models;

[ServiceFilter(typeof(AuthenticationFilter))]
public class OrderController : Controller
{
    private static List<Order> orders = new List<Order>();

    public IActionResult Index()
    {
        return View(orders); // List all orders
    }

    public IActionResult Create(int productId)
    {
        var username = HttpContext.Session.GetString("User");
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login", "Account");
        }

        // Simulate order creation
        orders.Add(new Order
        {
            Id = orders.Count + 1,
            ProductId = productId,
            UserId = 1, // Just a demo; normally get user id from session/db
            OrderDate = DateTime.Now
        });

        TempData["Message"] = "Order placed successfully!";
        return RedirectToAction("Index");
    }
}
