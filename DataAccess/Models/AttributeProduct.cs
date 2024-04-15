using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

public partial class AttributeProduct
{
    [Key]
    [Column("AttributeProductID")]
    public int AttributeProductId { get; set; }

    [Column("AttributeID")]
    public int? AttributeId { get; set; }

    [Column("ProductID")]
    public int? ProductId { get; set; }

    [Column("AttributeValueID")]
    public int? AttributeValueId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [ForeignKey("AttributeId")]
    [InverseProperty("AttributeProducts")]
    public virtual Attribute? Attribute { get; set; }

    [ForeignKey("AttributeValueId")]
    [InverseProperty("AttributeProducts")]
    public virtual AttributeValue? AttributeValue { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("AttributeProducts")]
    public virtual Product? Product { get; set; }
}
