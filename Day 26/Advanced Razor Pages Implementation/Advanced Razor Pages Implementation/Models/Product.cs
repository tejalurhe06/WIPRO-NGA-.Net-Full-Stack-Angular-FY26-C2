using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Advanced_Razor_Pages_Implementation.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Collection of Categories
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
