using System;
using System.Collections.Generic;

namespace z2.Models;

public partial class PaymentType
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();
}
