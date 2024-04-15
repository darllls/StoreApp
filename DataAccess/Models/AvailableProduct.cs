using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

public partial class AvailableProduct
{
    [Key]
    [Column("AvailableProductID")]
    public int AvailableProductId { get; set; }

    [Column("ProductID")]
    public int? ProductId { get; set; }

    [Column("StoreID")]
    public int? StoreId { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [InverseProperty("AvailableProduct")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("ProductId")]
    [InverseProperty("AvailableProducts")]
    public virtual Product? Product { get; set; }

    [InverseProperty("AvailableProduct")]
    public virtual ICollection<ShipmentInvoice> ShipmentInvoices { get; set; } = new List<ShipmentInvoice>();

    [ForeignKey("StoreId")]
    [InverseProperty("AvailableProducts")]
    public virtual Store? Store { get; set; }
}
