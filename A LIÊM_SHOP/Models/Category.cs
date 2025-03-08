﻿using System;
using System.Collections.Generic;

namespace A_LIÊM_SHOP.Models;

public partial class Category
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
