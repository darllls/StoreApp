using System;
using System.Collections.Generic;

namespace DataContextMetadata.Models;

public partial class Fact
{
    public int FactId { get; set; }

    public int? DwTableId { get; set; }

    public string? Keyname { get; set; }

    public string? FactType { get; set; }

    public virtual Dwtable? DwTable { get; set; }

    public virtual ICollection<Factmetric> Factmetrics { get; set; } = new List<Factmetric>();
}
