using System;
using System.Collections.Generic;

namespace ShopForHome.API.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public decimal OrderTotal { get; set; }

    public string OrderStatus { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public int ShippingAddressId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Address ShippingAddress { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
