using Microsoft.AspNetCore.Mvc;

namespace EcommerceRoutingDemo.Controllers
{
    public class ProductsController : Controller
    {
        // Complex route: /Products/Electronics/101
        public IActionResult Details(string category, int id)
        {
            ViewBag.Category = category;
            ViewBag.ProductId = id;
            return View();
        }

        // Custom constraint: /Products/Filter/Electronics/100-500
        public IActionResult Filter(string category, string priceRange)
        {
            ViewBag.Category = category;
            ViewBag.PriceRange = priceRange;
            return View();
        }
    }
}
