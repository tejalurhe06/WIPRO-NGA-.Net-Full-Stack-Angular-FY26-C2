﻿using System;
using System.Collections.Generic;

namespace ShopForHome.API.Models;

public partial class Cart
{
    public Guid CartId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User User { get; set; } = null!;
}
