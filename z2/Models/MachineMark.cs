using System;
using System.Collections.Generic;

namespace z2.Models;

public partial class MachineMark
{
    public int Id { get; set; }

    public string? Mark { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
