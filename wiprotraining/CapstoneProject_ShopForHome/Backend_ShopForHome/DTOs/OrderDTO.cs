using System.ComponentModel.DataAnnotations;
namespace ShopForHome.API.DTOs
{
    public class OrderItemDTO
    {
        [Required]
        public int ProductId { get; set; }

        [StringLength(150)]
        public string ProductName { get; set; }

        [Range(0.01, 999999.99,
            ErrorMessage = "Unit price must be greater than 0.")]
        public decimal UnitPrice { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        public decimal TotalPrice => UnitPrice * Quantity;
    }


    public class OrderDTO
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal OrderTotal { get; set; }

        [Required, StringLength(20)]
        [RegularExpression("^(Pending|Processing|Shipped|Delivered|Cancelled)$",
            ErrorMessage = "Invalid order status.")]
        public string OrderStatus { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }


    public class CreateOrderRequest
    {
        [Required]
        public int AddressId { get; set; } // Must match one of the user's saved addresses

        [StringLength(20, ErrorMessage = "Coupon code cannot exceed 20 characters.")]
        [RegularExpression(@"^[A-Z0-9\-]*$",
            ErrorMessage = "Coupon code can only contain uppercase letters, numbers, and dashes.")]
        public string? CouponCode { get; set; }
    }

}