using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

public partial class Store
{
    [Key]
    [Column("StoreID")]
    public int StoreId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? StoreName { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Address { get; set; }

    [Column("CityID")]
    public int? CityId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [InverseProperty("Store")]
    public virtual ICollection<AvailableProduct> AvailableProducts { get; set; } = new List<AvailableProduct>();

    [ForeignKey("CityId")]
    [InverseProperty("Stores")]
    public virtual City? City { get; set; }

    [InverseProperty("Store")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
