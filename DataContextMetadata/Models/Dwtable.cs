using System;
using System.Collections.Generic;

namespace DataContextMetadata.Models;

public partial class Dwtable
{
    public int DwTableId { get; set; }

    public int? DataLoadHistoryId { get; set; }

    public string? DwTableName { get; set; }

    public virtual Dataloadhistory? DataLoadHistory { get; set; }

    public virtual ICollection<Dimension> Dimensions { get; set; } = new List<Dimension>();

    public virtual ICollection<Fact> Facts { get; set; } = new List<Fact>();
}
