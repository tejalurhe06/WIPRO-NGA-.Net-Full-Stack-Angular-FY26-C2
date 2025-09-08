﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Recap.Model
{
    public class CustomerModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public string Message { get; set; }

        public void OnPost()
        {
            Message = $"{Name}, information will be sent to {Email}";
        }
    }
}
