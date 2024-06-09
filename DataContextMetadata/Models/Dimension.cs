using System;
using System.Collections.Generic;

namespace DataContextMetadata.Models;

public partial class Dimension
{
    public int DimensionId { get; set; }

    public int? DwTableId { get; set; }

    public string? Keyname { get; set; }

    public string? DimensionName { get; set; }

    public string? DimensionType { get; set; }

    public virtual Dwtable? DwTable { get; set; }
}
