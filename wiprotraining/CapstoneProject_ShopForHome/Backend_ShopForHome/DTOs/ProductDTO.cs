using System.ComponentModel.DataAnnotations;
namespace ShopForHome.API.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Url]
        [StringLength(300)]
        public string ImageUrl { get; set; }

        [Range(0, 5)]
        public decimal? AverageRating { get; set; }

        public int CategoryId { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; } // convenience only
    }


    public class CreateProductDTO
    {
        [Required, StringLength(150, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required, Range(0.01, 999999.99,
            ErrorMessage = "Price must be greater than 0 and realistic.")]
        public decimal Price { get; set; }

        [Required, Range(0, int.MaxValue,
            ErrorMessage = "Stock quantity cannot be negative.")]
        public int StockQuantity { get; set; }

        [Url]
        [StringLength(300)]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }


    public class UpdateProductDTO : CreateProductDTO
    {
        [Required]
        public int ProductId { get; set; }
    }
}