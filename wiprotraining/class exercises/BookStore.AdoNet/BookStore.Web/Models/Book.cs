using System;
using System.ComponentModel.DataAnnotations;


namespace BookStore.Web.Models
{
    public class Book
    {
        public int Id { get; set; }


        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;


        [Required, StringLength(150)]
        public string Author { get; set; } = string.Empty;


        [StringLength(100)]
        public string? Genre { get; set; }


        [StringLength(20)]
        public string? ISBN { get; set; }


        [Range(0, 999999)]
        public decimal Price { get; set; }


        [Range(0, int.MaxValue)]
        public int Stock { get; set; }


        [DataType(DataType.Date)]
        public DateTime? PublishedDate { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}