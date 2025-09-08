using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingDemo.Controllers
{
    public class ProductsController : Controller
    {
        // URL: /Products/Electronics/101
        public IActionResult Details(string category, int id)
        {
            ViewBag.Category = category;
            ViewBag.ProductId = id;
            return View();
        }
    }
}
