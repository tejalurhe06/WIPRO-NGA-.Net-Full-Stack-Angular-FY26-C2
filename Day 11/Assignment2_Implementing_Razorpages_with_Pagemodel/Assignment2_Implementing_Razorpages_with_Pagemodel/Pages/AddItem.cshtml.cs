using Assignment2_Implementing_Razorpages_with_Pagemodel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment2_Implementing_Razorpages_with_Pagemodel.Pages
{
    public class AddItemModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                ItemListModel.Items.Add(new Item { Id = ItemListModel.Items.Count + 1, Name = Name });
                return RedirectToPage("ItemList");
            }
            return Page();
        }
    }
}
