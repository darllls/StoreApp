using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Models;

public partial class ProductCategory
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CategoryName { get; set; }

    [Column("CategoryParentID")]
    public int? CategoryParentId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [ForeignKey("CategoryParentId")]
    [InverseProperty("InverseCategoryParent")]
    public virtual ProductCategory? CategoryParent { get; set; }

    [InverseProperty("CategoryParent")]
    public virtual ICollection<ProductCategory> InverseCategoryParent { get; set; } = new List<ProductCategory>();

    [InverseProperty("ProductCategory")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
