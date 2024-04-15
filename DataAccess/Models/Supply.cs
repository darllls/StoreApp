using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

public partial class Supply
{
    [Key]
    [Column("SupplyID")]
    public int SupplyId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SupplyNumber { get; set; }

    [Column("SupplierID")]
    public int? SupplierId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Sum { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SupplyDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Supplies")]
    public virtual SupplyStatus? Status { get; set; }

    [ForeignKey("SupplierId")]
    [InverseProperty("Supplies")]
    public virtual Supplier? Supplier { get; set; }

    [InverseProperty("Supply")]
    public virtual ICollection<SupplyDetail> SupplyDetails { get; set; } = new List<SupplyDetail>();
}
