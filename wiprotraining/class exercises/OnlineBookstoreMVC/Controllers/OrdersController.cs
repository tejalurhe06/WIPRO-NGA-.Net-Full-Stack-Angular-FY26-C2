using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineBookstoreMVC.Data;
using OnlineBookstoreMVC.Models;

namespace OnlineBookstoreMVC.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Checkout page: displays cart and confirms order
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetString("Cart");
            var items = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart);
            if(items.Count == 0)
                return RedirectToAction("Index", "Cart");

            ViewBag.Total = items.Sum(x => x.Price * x.Quantity);
            return View(items);
        }

        // Place order
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var cart = HttpContext.Session.GetString("Cart");
            var items = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart);
            if(items.Count == 0)
                return RedirectToAction("Index", "Cart");

            var order = new Order
            {
                UserId = User.Identity.Name,
                OrderDate = DateTime.Now,
                TotalAmount = items.Sum(x => x.Price * x.Quantity)
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            // Clear cart
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Confirmation", new { orderId = order.Id });
        }

        // Order confirmation
        public IActionResult Confirmation(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if(order == null) return NotFound();
            return View(order);
        }

        // View user order history
        public IActionResult History()
        {
            var orders = _context.Orders
                .Where(o => o.UserId == User.Identity.Name)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
            return View(orders);
        }
    }
}
