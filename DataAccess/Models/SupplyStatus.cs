using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

[Table("SupplyStatus")]
public partial class SupplyStatus
{
    [Key]
    [Column("SupplyStatusID")]
    public int SupplyStatusId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? StatusName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
