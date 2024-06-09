using System;
using System.Collections.Generic;

namespace DataContextWH.Models;

public partial class DateDimension
{
    public int DateId { get; set; }

    public int? DateYear { get; set; }

    public int? DateMonth { get; set; }

    public int? DateDay { get; set; }

    public virtual ICollection<OrderDetailsFact> OrderDetailsFactPeriodEnds { get; set; } = new List<OrderDetailsFact>();

    public virtual ICollection<OrderDetailsFact> OrderDetailsFactPeriodStarts { get; set; } = new List<OrderDetailsFact>();

    public virtual ICollection<SuppliesDimension> SuppliesDimensions { get; set; } = new List<SuppliesDimension>();
}
