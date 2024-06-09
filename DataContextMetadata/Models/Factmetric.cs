using System;
using System.Collections.Generic;

namespace DataContextMetadata.Models;

public partial class Factmetric
{
    public int FactMetricId { get; set; }

    public int? FactId { get; set; }

    public int? DwAttributeColumnId { get; set; }

    public string? Description { get; set; }

    public virtual Dwattributecolumn? DwAttributeColumn { get; set; }

    public virtual Fact? Fact { get; set; }
}
