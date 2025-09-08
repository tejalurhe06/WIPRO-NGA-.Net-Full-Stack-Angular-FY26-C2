using Microsoft.AspNetCore.Mvc;
using CustomerFeedbackPortalMVC.Models;

namespace CustomerFeedbackPortalMVC.Controllers
{
    public class FeedbackController : Controller
    {
        // In-memory storage for demo
        private static List<Feedback> feedbacks = new List<Feedback>();

        // Display Feedback Form
        public IActionResult Index()
        {
            return View();
        }

        // Handle form submission
        [HttpPost]
        public IActionResult Submit(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", feedback);
            }

            feedbacks.Add(feedback);
            TempData["Message"] = "Feedback submitted successfully!";
            return RedirectToAction("Index");
        }

        // Display all submitted feedback
        public IActionResult FeedbackList()
        {
            return View(feedbacks);
        }
    }
}
