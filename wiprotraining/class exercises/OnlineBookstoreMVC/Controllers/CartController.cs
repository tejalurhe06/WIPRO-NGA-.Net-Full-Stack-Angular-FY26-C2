using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineBookstoreMVC.Models;

namespace OnlineBookstoreMVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetString("Cart");
            var items = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart);
            return View(items);
        }

        public IActionResult AddToCart(int id, string title, decimal price)
        {
            var cart = HttpContext.Session.GetString("Cart");
            var items = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart);

            var item = items.FirstOrDefault(x => x.BookId == id);
            if (item != null) item.Quantity++;
            else items.Add(new CartItem { BookId = id, Title = title, Price = price, Quantity = 1 });

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(items));
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var cart = HttpContext.Session.GetString("Cart");
            var items = string.IsNullOrEmpty(cart) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cart);

            var item = items.FirstOrDefault(x => x.BookId == id);
            if (item != null) items.Remove(item);

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(items));
            return RedirectToAction("Index");
        }
    }
}
