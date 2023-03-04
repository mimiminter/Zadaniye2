using System;
using System.Collections.Generic;

namespace z2.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? Fname { get; set; }

    public string? Mname { get; set; }

    public string? Lname { get; set; }

    public int? PassportSeries { get; set; }

    public int? PassportNumber { get; set; }

    public string? HomeAddress { get; set; }

    public string? NumberPhone { get; set; }

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();
}
