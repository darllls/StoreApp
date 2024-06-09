using System;
using System.Collections.Generic;

namespace DataContextMetadata.Models;

public partial class Sourcecolumn
{
    public int SourceColumnId { get; set; }

    public int? SourceTableId { get; set; }

    public string? SourceColumnName { get; set; }

    public string? DataType { get; set; }

    public virtual Sourcetable? SourceTable { get; set; }

    public virtual ICollection<Transformation> Transformations { get; set; } = new List<Transformation>();
}
