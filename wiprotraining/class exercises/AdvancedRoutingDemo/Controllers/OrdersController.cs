using Microsoft.AspNetCore.Mvc;

namespace AdvancedRoutingDemo.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Details(Guid orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
}
