using System;
using System.ComponentModel.DataAnnotations;
namespace ShopForHome.API.DTOs
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [StringLength(150)]
        public string ProductName { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Url]
        [StringLength(300)]
        public string ImageUrl { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public decimal TotalPrice => Price * Quantity;
    }


    public class AddToCartRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required, Range(1, int.MaxValue,
            ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; } = 1;
    }


    public class UpdateCartItemRequest
    {
        [Required, Range(1, int.MaxValue,
            ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }

}