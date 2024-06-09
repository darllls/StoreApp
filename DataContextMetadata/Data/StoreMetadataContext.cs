using System;
using System.Collections.Generic;
using DataContextMetadata.Models;
using Microsoft.EntityFrameworkCore;

namespace DataContextMetadata.Data;

public partial class StoreMetadataContext : DbContext
{
    public StoreMetadataContext()
    {
    }

    public StoreMetadataContext(DbContextOptions<StoreMetadataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dataloadhistory> Dataloadhistories { get; set; }

    public virtual DbSet<Dimension> Dimensions { get; set; }

    public virtual DbSet<Dimensionattribute> Dimensionattributes { get; set; }

    public virtual DbSet<Dwattributecolumn> Dwattributecolumns { get; set; }

    public virtual DbSet<Dwtable> Dwtables { get; set; }

    public virtual DbSet<Fact> Facts { get; set; }

    public virtual DbSet<Factmetric> Factmetrics { get; set; }

    public virtual DbSet<Sourcecolumn> Sourcecolumns { get; set; }

    public virtual DbSet<Sourcedb> Sourcedbs { get; set; }

    public virtual DbSet<Sourcetable> Sourcetables { get; set; }

    public virtual DbSet<Transformation> Transformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Daria;Initial Catalog=StoreMetadata;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dataloadhistory>(entity =>
        {
            entity.HasKey(e => e.DataLoadHistoryId).HasName("PK__dataload__C5EB248A83D0823C");

            entity.ToTable("dataloadhistory");

            entity.Property(e => e.DataLoadHistoryId).HasColumnName("data_load_history_id");
            entity.Property(e => e.AffectedTableCount).HasColumnName("affected_table_count");
            entity.Property(e => e.LoadDatetime)
                .HasColumnType("datetime")
                .HasColumnName("load_datetime");
            entity.Property(e => e.LoadRows).HasColumnName("load_rows");
            entity.Property(e => e.LoadTime).HasColumnName("load_time");
            entity.Property(e => e.SourceTableCount).HasColumnName("source_table_count");
        });

        modelBuilder.Entity<Dimension>(entity =>
        {
            entity.HasKey(e => e.DimensionId).HasName("PK__dimensio__AC0A9BA476E8B6BE");

            entity.ToTable("dimension");

            entity.Property(e => e.DimensionId).HasColumnName("dimension_id");
            entity.Property(e => e.DimensionName).HasColumnName("dimension_name");
            entity.Property(e => e.DimensionType).HasColumnName("dimension_type");
            entity.Property(e => e.DwTableId).HasColumnName("dw_table_id");
            entity.Property(e => e.Keyname).HasColumnName("keyname");

            entity.HasOne(d => d.DwTable).WithMany(p => p.Dimensions)
                .HasForeignKey(d => d.DwTableId)
                .HasConstraintName("FK__dimension__dw_ta__3E52440B");
        });

        modelBuilder.Entity<Dimensionattribute>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dimensionattributes");

            entity.Property(e => e.DimensionId).HasColumnName("dimension_id");
            entity.Property(e => e.DwAttributeColumnId).HasColumnName("dw_attribute_column_id");

            entity.HasOne(d => d.Dimension).WithMany()
                .HasForeignKey(d => d.DimensionId)
                .HasConstraintName("FK__dimension__dw_at__403A8C7D");

            entity.HasOne(d => d.DwAttributeColumn).WithMany()
                .HasForeignKey(d => d.DwAttributeColumnId)
                .HasConstraintName("FK__dimension__dw_at__412EB0B6");
        });

        modelBuilder.Entity<Dwattributecolumn>(entity =>
        {
            entity.HasKey(e => e.DwAttributeColumnId).HasName("PK__dwattrib__E0F203B8F2B418EF");

            entity.ToTable("dwattributecolumn");

            entity.Property(e => e.DwAttributeColumnId).HasColumnName("dw_attribute_column_id");
            entity.Property(e => e.DwAttributeColumnDatatype).HasColumnName("dw_attribute_column_datatype");
            entity.Property(e => e.DwAttributeColumnName).HasColumnName("dw_attribute_column_name");
        });

        modelBuilder.Entity<Dwtable>(entity =>
        {
            entity.HasKey(e => e.DwTableId).HasName("PK__dwtable__DDCACE3C4D1CB103");

            entity.ToTable("dwtable");

            entity.Property(e => e.DwTableId).HasColumnName("dw_table_id");
            entity.Property(e => e.DataLoadHistoryId).HasColumnName("data_load_history_id");
            entity.Property(e => e.DwTableName).HasColumnName("dw_table_name");

            entity.HasOne(d => d.DataLoadHistory).WithMany(p => p.Dwtables)
                .HasForeignKey(d => d.DataLoadHistoryId)
                .HasConstraintName("FK__dwtable__data_lo__3B75D760");
        });

        modelBuilder.Entity<Fact>(entity =>
        {
            entity.HasKey(e => e.FactId).HasName("PK__fact__4BCDFB430D3CDABF");

            entity.ToTable("fact");

            entity.Property(e => e.FactId).HasColumnName("fact_id");
            entity.Property(e => e.DwTableId).HasColumnName("dw_table_id");
            entity.Property(e => e.FactType).HasColumnName("fact_type");
            entity.Property(e => e.Keyname).HasColumnName("keyname");

            entity.HasOne(d => d.DwTable).WithMany(p => p.Facts)
                .HasForeignKey(d => d.DwTableId)
                .HasConstraintName("FK__fact__dw_table_i__440B1D61");
        });

        modelBuilder.Entity<Factmetric>(entity =>
        {
            entity.HasKey(e => e.FactMetricId).HasName("PK__factmetr__2E777D24504619B1");

            entity.ToTable("factmetric");

            entity.Property(e => e.FactMetricId).HasColumnName("fact_metric_id");
            entity.Property(e => e.DwAttributeColumnId).HasColumnName("dw_attribute_column_id");
            entity.Property(e => e.FactId).HasColumnName("fact_id");

            entity.HasOne(d => d.DwAttributeColumn).WithMany(p => p.Factmetrics)
                .HasForeignKey(d => d.DwAttributeColumnId)
                .HasConstraintName("FK__factmetri__dw_at__47DBAE45");

            entity.HasOne(d => d.Fact).WithMany(p => p.Factmetrics)
                .HasForeignKey(d => d.FactId)
                .HasConstraintName("FK__factmetri__fact___46E78A0C");
        });

        modelBuilder.Entity<Sourcecolumn>(entity =>
        {
            entity.HasKey(e => e.SourceColumnId).HasName("PK__sourceco__F1534DE4D5B766D9");

            entity.ToTable("sourcecolumn");

            entity.Property(e => e.SourceColumnId).HasColumnName("source_column_id");
            entity.Property(e => e.DataType).HasColumnName("data_type");
            entity.Property(e => e.SourceColumnName).HasColumnName("source_column_name");
            entity.Property(e => e.SourceTableId).HasColumnName("source_table_id");

            entity.HasOne(d => d.SourceTable).WithMany(p => p.Sourcecolumns)
                .HasForeignKey(d => d.SourceTableId)
                .HasConstraintName("FK__sourcecol__sourc__4F7CD00D");
        });

        modelBuilder.Entity<Sourcedb>(entity =>
        {
            entity.HasKey(e => e.SourceDbId).HasName("PK__sourcedb__474C64535A4CB826");

            entity.ToTable("sourcedb");

            entity.Property(e => e.SourceDbId).HasColumnName("source_db_id");
            entity.Property(e => e.SourceDbName).HasColumnName("source_db_name");
        });

        modelBuilder.Entity<Sourcetable>(entity =>
        {
            entity.HasKey(e => e.SourceTableId).HasName("PK__sourceta__99C3346C0BE5BD1C");

            entity.ToTable("sourcetable");

            entity.Property(e => e.SourceTableId).HasColumnName("source_table_id");
            entity.Property(e => e.Keyname).HasColumnName("keyname");
            entity.Property(e => e.SourceDbId).HasColumnName("source_db_id");
            entity.Property(e => e.SourceTableName).HasColumnName("source_table_name");

            entity.HasOne(d => d.SourceDb).WithMany(p => p.Sourcetables)
                .HasForeignKey(d => d.SourceDbId)
                .HasConstraintName("FK__sourcetab__sourc__4CA06362");
        });

        modelBuilder.Entity<Transformation>(entity =>
        {
            entity.HasKey(e => e.TransformationId).HasName("PK__transfor__F9B3FCAC357AAE04");

            entity.ToTable("transformation");

            entity.Property(e => e.TransformationId).HasColumnName("transformation_id");
            entity.Property(e => e.DwAttributeColumnId).HasColumnName("dw_attribute_column_id");
            entity.Property(e => e.SourceColumnId).HasColumnName("source_column_id");
            entity.Property(e => e.TransformationRule).HasColumnName("transformation_rule");

            entity.HasOne(d => d.DwAttributeColumn).WithMany(p => p.Transformations)
                .HasForeignKey(d => d.DwAttributeColumnId)
                .HasConstraintName("FK__transform__dw_at__52593CB8");

            entity.HasOne(d => d.SourceColumn).WithMany(p => p.Transformations)
                .HasForeignKey(d => d.SourceColumnId)
                .HasConstraintName("FK__transform__sourc__534D60F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
