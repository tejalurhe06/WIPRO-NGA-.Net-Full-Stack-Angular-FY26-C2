
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace Core_Web_Razor_Demo.Pages
{
    public class EmpPageModel : PageModel
    {
        [BindProperty]
        public int id { get; set; }

        [BindProperty]
        public string name { get; set; }

        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string phone { get; set; }

        [BindProperty]
        public string message { get; set; }
        public void OnPost()
        {
            // These properties are automatically populated from the form
            message = $"Thanks {id} {name}, we received your message!";
        }
    }
}
