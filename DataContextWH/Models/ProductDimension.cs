using System;
using System.Collections.Generic;

namespace DataContextWH.Models;

public partial class ProductDimension
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? BkProductId { get; set; }

    public string? ProductCategoryConcat { get; set; }

    public string? Brand { get; set; }

    public virtual ICollection<OrderDetailsFact> OrderDetailsFacts { get; set; } = new List<OrderDetailsFact>();

    public virtual ICollection<SupplyDetailsFact> SupplyDetailsFacts { get; set; } = new List<SupplyDetailsFact>();
}
