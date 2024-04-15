using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

[Table("AttributeValue")]
public partial class AttributeValue
{
    [Key]
    [Column("AttributeValueID")]
    public int AttributeValueId { get; set; }

    [Column("AttributeValue")]
    [StringLength(255)]
    [Unicode(false)]
    public string? AttributeValue1 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [InverseProperty("AttributeValue")]
    public virtual ICollection<AttributeProduct> AttributeProducts { get; set; } = new List<AttributeProduct>();
}
