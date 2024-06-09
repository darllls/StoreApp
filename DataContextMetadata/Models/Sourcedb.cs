using System;
using System.Collections.Generic;

namespace DataContextMetadata.Models;

public partial class Sourcedb
{
    public int SourceDbId { get; set; }

    public string? SourceDbName { get; set; }

    public virtual ICollection<Sourcetable> Sourcetables { get; set; } = new List<Sourcetable>();
}
