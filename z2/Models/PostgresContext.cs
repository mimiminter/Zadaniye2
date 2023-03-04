using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace z2.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<MachineMark> MachineMarks { get; set; }

    public virtual DbSet<ManufacturerCountry> ManufacturerCountries { get; set; }

    public virtual DbSet<ModelCar> ModelCars { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=polka2003");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fname)
                .HasMaxLength(30)
                .HasColumnName("fname");
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(50)
                .HasColumnName("home_address");
            entity.Property(e => e.Lname)
                .HasMaxLength(30)
                .HasColumnName("lname");
            entity.Property(e => e.Mname)
                .HasMaxLength(30)
                .HasColumnName("mname");
            entity.Property(e => e.NumberPhone)
                .HasMaxLength(15)
                .HasColumnName("number_phone");
            entity.Property(e => e.PassportNumber).HasColumnName("passport_number");
            entity.Property(e => e.PassportSeries).HasColumnName("passport_series");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("color_pkey");

            entity.ToTable("color");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Colors)
                .HasMaxLength(50)
                .HasColumnName("colors");
        });

        modelBuilder.Entity<MachineMark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("machine_mark_pkey");

            entity.ToTable("machine_mark");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Mark)
                .HasMaxLength(50)
                .HasColumnName("mark");
        });

        modelBuilder.Entity<ManufacturerCountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("manufacturer_country_pkey");

            entity.ToTable("manufacturer_country");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
        });

        modelBuilder.Entity<ModelCar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("model_car_pkey");

            entity.ToTable("model_car");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_type_pkey");

            entity.ToTable("payment_type");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdColor).HasColumnName("id_color");
            entity.Property(e => e.IdMachineMark).HasColumnName("id_machine_mark");
            entity.Property(e => e.IdManufacturerCountry).HasColumnName("id_manufacturer_country");
            entity.Property(e => e.IdModelCar).HasColumnName("id_model_car");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdColor)
                .HasConstraintName("product_id_color_fkey");

            entity.HasOne(d => d.IdMachineMarkNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdMachineMark)
                .HasConstraintName("product_id_machine_mark_fkey");

            entity.HasOne(d => d.IdManufacturerCountryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdManufacturerCountry)
                .HasConstraintName("product_id_manufacturer_country_fkey");

            entity.HasOne(d => d.IdModelCarNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdModelCar)
                .HasConstraintName("product_id_model_car_fkey");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchase_pkey");

            entity.ToTable("purchase");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdPaymentType).HasColumnName("id_payment_type");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("purchase_id_client_fkey");

            entity.HasOne(d => d.IdPaymentTypeNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.IdPaymentType)
                .HasConstraintName("purchase_id_payment_type_fkey");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("purchase_id_product_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
