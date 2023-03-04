using System;
using System.Collections.Generic;

namespace z2.Models;

public partial class Purchase
{
    public int Id { get; set; }

    public int? IdProduct { get; set; }

    public int? IdClient { get; set; }

    public DateOnly? Data { get; set; }

    public int? IdPaymentType { get; set; }

    public virtual Client? IdClientNavigation { get; set; }

    public virtual PaymentType? IdPaymentTypeNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}
