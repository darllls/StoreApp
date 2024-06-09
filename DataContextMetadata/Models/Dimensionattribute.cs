using System;
using System.Collections.Generic;

namespace DataContextMetadata.Models;

public partial class Dimensionattribute
{
    public int? DimensionId { get; set; }

    public int? DwAttributeColumnId { get; set; }

    public virtual Dimension? Dimension { get; set; }

    public virtual Dwattributecolumn? DwAttributeColumn { get; set; }
}
