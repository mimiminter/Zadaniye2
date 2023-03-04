using System;
using System.Collections.Generic;

namespace z2.Models;

public partial class ModelCar
{
    public int Id { get; set; }

    public string? Model { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
