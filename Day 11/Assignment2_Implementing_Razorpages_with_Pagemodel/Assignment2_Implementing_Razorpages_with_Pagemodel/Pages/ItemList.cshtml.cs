using Assignment2_Implementing_Razorpages_with_Pagemodel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment2_Implementing_Razorpages_with_Pagemodel.Pages
{
    public class ItemListModel : PageModel
    {
        public static List<Item> Items = new List<Item>();

        public void OnGet()
        {

        }
    }
}
