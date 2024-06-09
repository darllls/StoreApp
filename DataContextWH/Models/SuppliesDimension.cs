using System;
using System.Collections.Generic;

namespace DataContextWH.Models;

public partial class SuppliesDimension
{
    public int SupplyId { get; set; }

    public int? SupplyDateId { get; set; }

    public string? SupplyStatus { get; set; }

    public int? BkSupplyId { get; set; }

    public string? SupplyNumber { get; set; }

    public virtual DateDimension? SupplyDate { get; set; }

    public virtual ICollection<SupplyDetailsFact> SupplyDetailsFacts { get; set; } = new List<SupplyDetailsFact>();
}
