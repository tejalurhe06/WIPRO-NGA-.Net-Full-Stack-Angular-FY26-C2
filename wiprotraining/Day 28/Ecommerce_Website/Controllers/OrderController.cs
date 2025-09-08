using Microsoft.AspNetCore.Mvc;
using Ecommerce_Website.Models;
using Ecommerce_Website.Extensions;
using System.Linq;

namespace Ecommerce_Website.Controllers
{
    public class OrderController : Controller
    {
        // Place order from cart
        public IActionResult PlaceOrder()
        {
            // 1. Get cart from session
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();

            // 2. Get logged-in username
            var username = HttpContext.Session.GetString("username");

            // 3. Create new order
            var order = new Order
            {
                Username = username,
                Items = cart,
                TotalAmount = cart.Sum(i => i.Product!.Price * i.Quantity)
            };

            // 4. Store order in session
            HttpContext.Session.SetObject("order", order);

            // 5. Clear cart after placing order
            HttpContext.Session.Remove("cart");

            // 6. Show order summary view
            return View(order);
        }

        // View bill
        public IActionResult Bill()
        {
            var order = HttpContext.Session.GetObject<Order>("order");
            if (order == null)
                return RedirectToAction("Index", "Home"); // fallback if no order
            return View(order);
        }
    }
}
