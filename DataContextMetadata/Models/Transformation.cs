using System;
using System.Collections.Generic;

namespace DataContextMetadata.Models;

public partial class Transformation
{
    public int TransformationId { get; set; }

    public int? DwAttributeColumnId { get; set; }

    public int? SourceColumnId { get; set; }

    public string? TransformationRule { get; set; }

    public virtual Dwattributecolumn? DwAttributeColumn { get; set; }

    public virtual Sourcecolumn? SourceColumn { get; set; }
}
