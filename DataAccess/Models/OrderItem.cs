using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

public partial class OrderItem
{
    [Key]
    [Column("OrderItemsID")]
    public int OrderItemsId { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

    [Column("AvailableProductID")]
    public int? AvailableProductId { get; set; }

    public int? Amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [ForeignKey("AvailableProductId")]
    [InverseProperty("OrderItems")]
    public virtual AvailableProduct? AvailableProduct { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order? Order { get; set; }
}
