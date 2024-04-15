using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

[Table("ShipmentInvoice")]
public partial class ShipmentInvoice
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? InvoiceNumber { get; set; }

    [Column("SupplyDetailsID")]
    public int? SupplyDetailsId { get; set; }

    [Column("AvailableProductID")]
    public int? AvailableProductId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column("EmployeeID")]
    public int? EmployeeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [ForeignKey("AvailableProductId")]
    [InverseProperty("ShipmentInvoices")]
    public virtual AvailableProduct? AvailableProduct { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("ShipmentInvoices")]
    public virtual Employee? Employee { get; set; }

    [ForeignKey("SupplyDetailsId")]
    [InverseProperty("ShipmentInvoices")]
    public virtual SupplyDetail? SupplyDetails { get; set; }
}
