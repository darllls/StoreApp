using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

[Table("City")]
public partial class City
{
    [Key]
    [Column("CityID")]
    public int CityId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [InverseProperty("City")]
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();

    [InverseProperty("City")]
    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
