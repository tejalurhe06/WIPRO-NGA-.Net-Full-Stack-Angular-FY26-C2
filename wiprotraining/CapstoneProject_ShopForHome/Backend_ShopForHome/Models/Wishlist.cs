﻿using System;
using System.Collections.Generic;

namespace ShopForHome.API.Models;

public partial class Wishlist
{
    public int WishlistId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public DateTime AddedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
