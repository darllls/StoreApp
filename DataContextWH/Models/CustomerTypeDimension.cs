using System;
using System.Collections.Generic;

namespace DataContextWH.Models;

public partial class CustomerTypeDimension
{
    public int CustomerTypeId { get; set; }

    public string? TypeName { get; set; }

    public int? BkCustomerTypeId { get; set; }

    public decimal? DiscountRate { get; set; }

    public virtual ICollection<OrderDetailsFact> OrderDetailsFacts { get; set; } = new List<OrderDetailsFact>();
}
