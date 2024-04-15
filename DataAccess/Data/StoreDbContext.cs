using System;
using System.Collections.Generic;
using DataContext.Models;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Data;

public partial class StoreDbContext : DbContext
{
    public StoreDbContext()
    {
    }

    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Models.Attribute> Attributes { get; set; }

    public virtual DbSet<AttributeProduct> AttributeProducts { get; set; }

    public virtual DbSet<AttributeValue> AttributeValues { get; set; }

    public virtual DbSet<AvailableProduct> AvailableProducts { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerType> CustomerTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ShipmentInvoice> ShipmentInvoices { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<SupplyDetail> SupplyDetails { get; set; }

    public virtual DbSet<SupplyStatus> SupplyStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Daria;Initial Catalog=StoreDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Attribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK__Attribut__C189298AB01A4EB6");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<AttributeProduct>(entity =>
        {
            entity.HasKey(e => e.AttributeProductId).HasName("PK__Attribut__5EBAA81E27947614");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeProducts).HasConstraintName("FK__Attribute__Attri__6B24EA82");

            entity.HasOne(d => d.AttributeValue).WithMany(p => p.AttributeProducts).HasConstraintName("FK__Attribute__Attri__6D0D32F4");

            entity.HasOne(d => d.Product).WithMany(p => p.AttributeProducts).HasConstraintName("FK__Attribute__Produ__6C190EBB");
        });

        modelBuilder.Entity<AttributeValue>(entity =>
        {
            entity.HasKey(e => e.AttributeValueId).HasName("PK__Attribut__335E2256115F187C");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<AvailableProduct>(entity =>
        {
            entity.HasKey(e => e.AvailableProductId).HasName("PK__Availabl__B28842F5E12FFB60");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Product).WithMany(p => p.AvailableProducts).HasConstraintName("FK__Available__Produ__71D1E811");

            entity.HasOne(d => d.Store).WithMany(p => p.AvailableProducts).HasConstraintName("FK__Available__Store__72C60C4A");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brands__DAD4F3BE00160FCC");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__City__F2D21A9664E2CB94");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B897CC095D");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CustomerType).WithMany(p => p.Customers).HasConstraintName("FK__Customer__Custom__5165187F");
        });

        modelBuilder.Entity<CustomerType>(entity =>
        {
            entity.HasKey(e => e.CustomerTypeId).HasName("PK__Customer__958B614C66429E98");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF19EE06201");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Store).WithMany(p => p.Employees).HasConstraintName("FK__Employees__Store__5629CD9C");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF3097367F");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasConstraintName("FK__Orders__Customer__5AEE82B9");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders).HasConstraintName("FK__Orders__Employee__5BE2A6F2");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemsId).HasName("PK__OrderIte__D5BB2535753D65EA");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AvailableProduct).WithMany(p => p.OrderItems).HasConstraintName("FK__OrderItem__Avail__787EE5A0");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasConstraintName("FK__OrderItem__Order__778AC167");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED6D07882F");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products).HasConstraintName("FK__Products__BrandI__66603565");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products).HasConstraintName("FK__Products__Produc__656C112C");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__ProductC__19093A2B55156C56");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CategoryParent).WithMany(p => p.InverseCategoryParent).HasConstraintName("FK__ProductCa__Categ__60A75C0F");
        });

        modelBuilder.Entity<ShipmentInvoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shipment__3214EC27892EFA54");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AvailableProduct).WithMany(p => p.ShipmentInvoices).HasConstraintName("FK__ShipmentI__Avail__123EB7A3");

            entity.HasOne(d => d.Employee).WithMany(p => p.ShipmentInvoices).HasConstraintName("FK__ShipmentI__Emplo__1332DBDC");

            entity.HasOne(d => d.SupplyDetails).WithMany(p => p.ShipmentInvoices).HasConstraintName("FK__ShipmentI__Suppl__114A936A");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F0E142B70824");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.City).WithMany(p => p.Stores).HasConstraintName("FK__Stores__CityID__48CFD27E");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE66694E6A8449D");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.City).WithMany(p => p.Suppliers).HasConstraintName("FK__Suppliers__CityI__7D439ABD");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.SupplyId).HasName("PK__Supplies__7CDD6C8EA0FC81AB");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Status).WithMany(p => p.Supplies).HasConstraintName("FK__Supplies__Status__06CD04F7");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Supplies).HasConstraintName("FK__Supplies__Suppli__05D8E0BE");
        });

        modelBuilder.Entity<SupplyDetail>(entity =>
        {
            entity.HasKey(e => e.SupplyDetailsId).HasName("PK__SupplyDe__87DA8D1701DA539D");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Product).WithMany(p => p.SupplyDetails).HasConstraintName("FK__SupplyDet__Produ__0C85DE4D");

            entity.HasOne(d => d.Supply).WithMany(p => p.SupplyDetails).HasConstraintName("FK__SupplyDet__Suppl__0B91BA14");
        });

        modelBuilder.Entity<SupplyStatus>(entity =>
        {
            entity.HasKey(e => e.SupplyStatusId).HasName("PK__SupplySt__78E7FF78BC9B7A25");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
