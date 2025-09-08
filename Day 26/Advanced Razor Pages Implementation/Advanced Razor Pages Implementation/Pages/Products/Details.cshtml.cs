using Advanced_Razor_Pages_Implementation.Data;
using Advanced_Razor_Pages_Implementation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Advanced_Razor_Pages_Implementation.Pages.Products
{
    public class DetailsModel : PageModel
    {
        public Product Product { get; set; }

        public IActionResult OnGet(int id)
        {
            Product = ProductRepository.GetById(id);
            if (Product == null)
                return NotFound();

            return Page();
        }
    }
}
