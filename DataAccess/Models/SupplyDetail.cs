using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

public partial class SupplyDetail
{
    [Key]
    [Column("SupplyDetailsID")]
    public int SupplyDetailsId { get; set; }

    [Column("SupplyID")]
    public int? SupplyId { get; set; }

    [Column("ProductID")]
    public int? ProductId { get; set; }

    public int? Amount { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PricePerUnit { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("SupplyDetails")]
    public virtual Product? Product { get; set; }

    [InverseProperty("SupplyDetails")]
    public virtual ICollection<ShipmentInvoice> ShipmentInvoices { get; set; } = new List<ShipmentInvoice>();

    [ForeignKey("SupplyId")]
    [InverseProperty("SupplyDetails")]
    public virtual Supply? Supply { get; set; }
}
