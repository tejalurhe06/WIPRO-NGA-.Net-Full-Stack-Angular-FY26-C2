using System.ComponentModel.DataAnnotations;
using OnlineBookstoreMVC.Validations;

namespace OnlineBookstoreMVC.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(50)]
        public string Author { get; set; }

        [Required, ISBNValidation]
        public string ISBN { get; set; }

        [Required, Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000.")]
        public decimal Price { get; set; }
    }
}
