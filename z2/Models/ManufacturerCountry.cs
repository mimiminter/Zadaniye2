using System;
using System.Collections.Generic;

namespace z2.Models;

public partial class ManufacturerCountry
{
    public int Id { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
