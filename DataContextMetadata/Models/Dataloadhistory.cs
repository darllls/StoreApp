using System;
using System.Collections.Generic;

namespace DataContextMetadata.Models;

public partial class Dataloadhistory
{
    public int DataLoadHistoryId { get; set; }

    public int? LoadRows { get; set; }

    public DateTime? LoadDatetime { get; set; }

    public int? AffectedTableCount { get; set; }

    public int? SourceTableCount { get; set; }

    public int? LoadTime { get; set; }

    public virtual ICollection<Dwtable> Dwtables { get; set; } = new List<Dwtable>();
}
