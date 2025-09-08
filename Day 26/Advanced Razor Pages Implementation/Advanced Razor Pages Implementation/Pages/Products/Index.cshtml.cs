using Advanced_Razor_Pages_Implementation.Data;
using Advanced_Razor_Pages_Implementation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Advanced_Razor_Pages_Implementation.Pages.Products
{
    public class IndexModel : PageModel
    {
        public List<Product> Products { get; set; }

        public void OnGet()
        {
            Products = ProductRepository.GetAll();
        }
    }
}
