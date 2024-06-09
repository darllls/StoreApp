using System;
using System.Collections.Generic;

namespace DataContextMetadata.Models;

public partial class Dwattributecolumn
{
    public int DwAttributeColumnId { get; set; }

    public string? DwAttributeColumnName { get; set; }

    public string? DwAttributeColumnDatatype { get; set; }

    public virtual ICollection<Factmetric> Factmetrics { get; set; } = new List<Factmetric>();

    public virtual ICollection<Transformation> Transformations { get; set; } = new List<Transformation>();
}
