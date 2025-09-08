namespace Ecommerce_Website.Models
{
    public class Order
    {
        public string Username { get; set; } = " ";
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal TotalAmount{ get; set; }
    }
}