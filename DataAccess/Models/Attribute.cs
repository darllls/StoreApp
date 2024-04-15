using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

public partial class Attribute
{
    [Key]
    [Column("AttributeID")]
    public int AttributeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? AttributeName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [InverseProperty("Attribute")]
    public virtual ICollection<AttributeProduct> AttributeProducts { get; set; } = new List<AttributeProduct>();
}
