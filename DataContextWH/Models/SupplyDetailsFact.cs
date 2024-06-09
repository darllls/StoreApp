using System;
using System.Collections.Generic;

namespace DataContextWH.Models;

public partial class SupplyDetailsFact
{
    public int SupplyDetailsId { get; set; }

    public int? SupplyId { get; set; }

    public int? ProductId { get; set; }

    public int? UnitBuyDateDifference { get; set; }

    public int? BkSupplyDetailsId { get; set; }

    public virtual ProductDimension? Product { get; set; }

    public virtual SuppliesDimension? Supply { get; set; }
}
