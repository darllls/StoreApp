using System;
using System.Collections.Generic;

namespace DataContextMetadata.Models;

public partial class Sourcetable
{
    public int SourceTableId { get; set; }

    public int? SourceDbId { get; set; }

    public string? SourceTableName { get; set; }

    public string? Keyname { get; set; }

    public virtual Sourcedb? SourceDb { get; set; }

    public virtual ICollection<Sourcecolumn> Sourcecolumns { get; set; } = new List<Sourcecolumn>();
}
