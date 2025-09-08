using Microsoft.AspNetCore.Mvc;

namespace Core_MVC_Demo.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var product = new { Id = 100, Name = "Sample Product" };

            ViewBag.prdid = product.Id;
            ViewBag.prdname = product.Name;
            return View(product);
        }
    }
}
