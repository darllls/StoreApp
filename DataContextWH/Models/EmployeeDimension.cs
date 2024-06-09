using System;
using System.Collections.Generic;

namespace DataContextWH.Models;

public partial class EmployeeDimension
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? BkEmployeeId { get; set; }

    public virtual ICollection<OrderDetailsFact> OrderDetailsFacts { get; set; } = new List<OrderDetailsFact>();
}
