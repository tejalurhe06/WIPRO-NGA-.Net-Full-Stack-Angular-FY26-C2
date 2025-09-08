using Advanced_Razor_Pages_Implementation.Data;
using Advanced_Razor_Pages_Implementation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Advanced_Razor_Pages_Implementation.Pages.Products
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; } = new Product();

        [BindProperty]
        public string CategoryNames { get; set; } // comma-separated categories

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!string.IsNullOrEmpty(CategoryNames))
            {
                var categories = CategoryNames.Split(',');
                foreach (var cat in categories)
                {
                    Product.Categories.Add(new Category { CategoryName = cat.Trim() });
                }
            }

            ProductRepository.Add(Product);
            return RedirectToPage("Index");
        }
    }
}
