using System;
using System.Collections.Generic;

namespace DataContextWH.Models;

public partial class OrderDetailsFact
{
    public int OrderDetailsId { get; set; }

    public int? ProductId { get; set; }

    public int? EmployeeId { get; set; }

    public int? CustomerTypeId { get; set; }

    public int? OrderCount { get; set; }

    public int? PeriodStartId { get; set; }

    public int? PeriodEndId { get; set; }

    public decimal? AverageCustomerSum { get; set; }

    public decimal? AverageSalesQuantityChangePercentage { get; set; }

    public virtual CustomerTypeDimension? CustomerType { get; set; }

    public virtual EmployeeDimension? Employee { get; set; }

    public virtual DateDimension? PeriodEnd { get; set; }

    public virtual DateDimension? PeriodStart { get; set; }

    public virtual ProductDimension? Product { get; set; }
}
