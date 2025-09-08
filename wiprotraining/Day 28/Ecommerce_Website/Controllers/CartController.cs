using Microsoft.AspNetCore.Mvc;
using Ecommerce_Website.Models;
using Ecommerce_Website.Extensions;
using System.Linq;

namespace Ecommerce_Website.Controllers
{
    public class CartController : Controller
    {
        // Show cart
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();
            return View(cart);
        }

        // Add product to cart
        public IActionResult Add(int id)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();

            // Find the product
            var product = HomeController._products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                // Check if product already in cart
                var existingItem = cart.FirstOrDefault(c => c.Product?.Id == id);
                if (existingItem != null)
                {
                    existingItem.Quantity++; // increment quantity
                }
                else
                {
                    cart.Add(new CartItem { Product = product, Quantity = 1 }); // add new item
                }
            }

            // Save cart back to session
            HttpContext.Session.SetObject("cart", cart);
            return RedirectToAction("Index");
        }

        // Remove product
        public IActionResult Remove(int id)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();
            cart.RemoveAll(c => c.Product?.Id == id);
            HttpContext.Session.SetObject("cart", cart);
            return RedirectToAction("Index");
        }

        // Update quantity
        public IActionResult Update(int id, int quantity)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.Product?.Id == id);
            if (item != null)
                item.Quantity = quantity;

            HttpContext.Session.SetObject("cart", cart);
            return RedirectToAction("Index");
        }
    }
}
