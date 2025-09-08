using System;
using System.Collections.Generic;

namespace ShopForHome.API.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public decimal? AverageRating { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
