using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

[Table("CustomerType")]
public partial class CustomerType
{
    [Key]
    [Column("CustomerTypeID")]
    public int CustomerTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? TypeName { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? DiscountRate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [InverseProperty("CustomerType")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
