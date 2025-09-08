using System;
using System.ComponentModel.DataAnnotations;
namespace ShopForHome.API.DTOs
{
    public class AdminProductDTO
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
        public string CategoryName { get; set; }

        // Admin-specific properties
        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        public string Status => IsActive ? "Active" : "Inactive";

        public string StockStatus => StockQuantity == 0 ? "Out of Stock" :
                                     StockQuantity < 10 ? "Low Stock" : "In Stock";
    }

}