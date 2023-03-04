using System;
using System.Collections.Generic;

namespace z2.Models;

public partial class Color
{
    public int Id { get; set; }

    public string? Colors { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
