using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ProductName { get; set; }

    public int? WarrantyMonths { get; set; }

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column(TypeName = "text")]
    public string? UsageInstructions { get; set; }

    public bool? RecyclingInfo { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PackageLength { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PackageWidth { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PackageHeight { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PackageWeight { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [Column("ProductCategoryID")]
    public int? ProductCategoryId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Configuration { get; set; }

    [Column("BrandID")]
    public int? BrandId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<AttributeProduct> AttributeProducts { get; set; } = new List<AttributeProduct>();

    [InverseProperty("Product")]
    public virtual ICollection<AvailableProduct> AvailableProducts { get; set; } = new List<AvailableProduct>();

    [ForeignKey("BrandId")]
    [InverseProperty("Products")]
    public virtual Brand? Brand { get; set; }

    [ForeignKey("ProductCategoryId")]
    [InverseProperty("Products")]
    public virtual ProductCategory? ProductCategory { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<SupplyDetail> SupplyDetails { get; set; } = new List<SupplyDetail>();
}
