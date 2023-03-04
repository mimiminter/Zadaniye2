using System;
using System.Collections.Generic;

namespace z2.Models;

public partial class Product
{
    public int Id { get; set; }

    public int? IdManufacturerCountry { get; set; }

    public int? IdMachineMark { get; set; }

    public int? IdModelCar { get; set; }

    public int? IdColor { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Color? IdColorNavigation { get; set; }

    public virtual MachineMark? IdMachineMarkNavigation { get; set; }

    public virtual ManufacturerCountry? IdManufacturerCountryNavigation { get; set; }

    public virtual ModelCar? IdModelCarNavigation { get; set; }

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();
}
