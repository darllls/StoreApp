using System;
using System.Collections.Generic;
using DataContextWH.Models;
using Microsoft.EntityFrameworkCore;

namespace DataContextWH.Data;

public partial class StoreWhContext : DbContext
{
    public StoreWhContext()
    {
    }

    public StoreWhContext(DbContextOptions<StoreWhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerTypeDimension> CustomerTypeDimensions { get; set; }

    public virtual DbSet<DateDimension> DateDimensions { get; set; }

    public virtual DbSet<EmployeeDimension> EmployeeDimensions { get; set; }

    public virtual DbSet<OrderDetailsFact> OrderDetailsFacts { get; set; }

    public virtual DbSet<ProductDimension> ProductDimensions { get; set; }

    public virtual DbSet<SuppliesDimension> SuppliesDimensions { get; set; }

    public virtual DbSet<SupplyDetailsFact> SupplyDetailsFacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Daria;Initial Catalog=StoreWH;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerTypeDimension>(entity =>
        {
            entity.HasKey(e => e.CustomerTypeId).HasName("PK__customer__3320C93975C8BEDA");

            entity.ToTable("customer_type_dimension");

            entity.Property(e => e.CustomerTypeId).HasColumnName("customer_type_id");
            entity.Property(e => e.BkCustomerTypeId).HasColumnName("bk_customer_type_id");
            entity.Property(e => e.DiscountRate)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("discount_rate");
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<DateDimension>(entity =>
        {
            entity.HasKey(e => e.DateId).HasName("PK__date_dim__51FC48658CE187D1");

            entity.ToTable("date_dimension");

            entity.Property(e => e.DateId).HasColumnName("date_id");
            entity.Property(e => e.DateDay).HasColumnName("date_day");
            entity.Property(e => e.DateMonth).HasColumnName("date_month");
            entity.Property(e => e.DateYear).HasColumnName("date_year");
        });

        modelBuilder.Entity<EmployeeDimension>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__employee__C52E0BA82A92F3DD");

            entity.ToTable("employee_dimension");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.BkEmployeeId).HasColumnName("bk_employee_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<OrderDetailsFact>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId).HasName("PK__order_de__F6FB5AE46AE53A0F");

            entity.ToTable("order_details_fact");

            entity.Property(e => e.OrderDetailsId).HasColumnName("order_details_id");
            entity.Property(e => e.AverageCustomerSum)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("average_customer_sum");
            entity.Property(e => e.AverageSalesQuantityChangePercentage)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("average_sales_quantity_change_percentage");
            entity.Property(e => e.CustomerTypeId).HasColumnName("customer_type_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.OrderCount).HasColumnName("order_count");
            entity.Property(e => e.PeriodEndId).HasColumnName("period_end_id");
            entity.Property(e => e.PeriodStartId).HasColumnName("period_start_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.CustomerType).WithMany(p => p.OrderDetailsFacts)
                .HasForeignKey(d => d.CustomerTypeId)
                .HasConstraintName("FK__order_det__custo__440B1D61");

            entity.HasOne(d => d.Employee).WithMany(p => p.OrderDetailsFacts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__order_det__emplo__4222D4EF");

            entity.HasOne(d => d.PeriodEnd).WithMany(p => p.OrderDetailsFactPeriodEnds)
                .HasForeignKey(d => d.PeriodEndId)
                .HasConstraintName("FK__order_det__perio__45F365D3");

            entity.HasOne(d => d.PeriodStart).WithMany(p => p.OrderDetailsFactPeriodStarts)
                .HasForeignKey(d => d.PeriodStartId)
                .HasConstraintName("FK__order_det__perio__44FF419A");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetailsFacts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__order_det__produ__4316F928");
        });

        modelBuilder.Entity<ProductDimension>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__product___47027DF58CEBBB52");

            entity.ToTable("product_dimension");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.BkProductId).HasColumnName("bk_product_id");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("brand");
            entity.Property(e => e.ProductCategoryConcat)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_category_concat");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_name");
        });

        modelBuilder.Entity<SuppliesDimension>(entity =>
        {
            entity.HasKey(e => e.SupplyId).HasName("PK__supplies__4870CD838CDDEFB7");

            entity.ToTable("supplies_dimension");

            entity.Property(e => e.SupplyId).HasColumnName("supply_id");
            entity.Property(e => e.BkSupplyId).HasColumnName("bk_supply_id");
            entity.Property(e => e.SupplyDateId).HasColumnName("supply_date_id");
            entity.Property(e => e.SupplyNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("supply_number");
            entity.Property(e => e.SupplyStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("supply_status");

            entity.HasOne(d => d.SupplyDate).WithMany(p => p.SuppliesDimensions)
                .HasForeignKey(d => d.SupplyDateId)
                .HasConstraintName("FK__supplies___suppl__3F466844");
        });

        modelBuilder.Entity<SupplyDetailsFact>(entity =>
        {
            entity.HasKey(e => e.SupplyDetailsId).HasName("PK__supply_d__EC782B19ADF1F1D3");

            entity.ToTable("supply_details_fact");

            entity.Property(e => e.SupplyDetailsId).HasColumnName("supply_details_id");
            entity.Property(e => e.BkSupplyDetailsId).HasColumnName("bk_supply_details_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SupplyId).HasColumnName("supply_id");
            entity.Property(e => e.UnitBuyDateDifference).HasColumnName("unit_buy_date_difference");

            entity.HasOne(d => d.Product).WithMany(p => p.SupplyDetailsFacts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__supply_de__produ__5812160E");

            entity.HasOne(d => d.Supply).WithMany(p => p.SupplyDetailsFacts)
                .HasForeignKey(d => d.SupplyId)
                .HasConstraintName("FK__supply_de__suppl__571DF1D5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
