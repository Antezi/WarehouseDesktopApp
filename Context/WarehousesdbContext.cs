using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WarehouseDesktopApp.Models;

namespace WarehouseDesktopApp.Context;

public partial class WarehousesdbContext : DbContext
{
    public WarehousesdbContext()
    {
    }

    public WarehousesdbContext(DbContextOptions<WarehousesdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Access> Accesses { get; set; }

    public virtual DbSet<AccessToUser> AccessToUsers { get; set; }

    public virtual DbSet<Cell> Cells { get; set; }

    public virtual DbSet<DistributionCenter> DistributionCenters { get; set; }

    public virtual DbSet<DistributionCenterType> DistributionCenterTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<RightAccess> RightAccesses { get; set; }

    public virtual DbSet<RightAccessToUser> RightAccessToUsers { get; set; }

    public virtual DbSet<SizeType> SizeTypes { get; set; }

    public virtual DbSet<StatusType> StatusTypes { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<Truck> Trucks { get; set; }

    public virtual DbSet<TruckModel> TruckModels { get; set; }

    public virtual DbSet<TruckStatus> TruckStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersType> UsersTypes { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WarehouseType> WarehouseTypes { get; set; }

    public virtual DbSet<WarehousesAccessToUser> WarehousesAccessToUsers { get; set; }

    public virtual DbSet<WarehousesClass> WarehousesClasses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=37.128.207.61; Username=anton; Database=warehousesdb; Password=VeryHardPassword121");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accesses_pk");

            entity.ToTable("accesses");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<AccessToUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("access_to_users_pk");

            entity.ToTable("access_to_users");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AccessId).HasColumnName("access_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Access).WithMany(p => p.AccessToUsers)
                .HasForeignKey(d => d.AccessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("access_to_users_accesses_fk");

            entity.HasOne(d => d.User).WithMany(p => p.AccessToUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("access_to_users_users_fk");
        });

        modelBuilder.Entity<Cell>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cells_pk");

            entity.ToTable("cells");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Floor).HasColumnName("floor");
            entity.Property(e => e.IsFilled).HasColumnName("is_filled");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Shelf).HasColumnName("shelf");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.SupplieId).HasColumnName("supplie_id");
            entity.Property(e => e.Warehouse).HasColumnName("warehouse");

            entity.HasOne(d => d.SizeNavigation).WithMany(p => p.Cells)
                .HasForeignKey(d => d.Size)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cells_size_types_fk");

            entity.HasOne(d => d.Supplie).WithMany(p => p.Cells)
                .HasForeignKey(d => d.SupplieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cells_supplies_fk");

            entity.HasOne(d => d.WarehouseNavigation).WithMany(p => p.Cells)
                .HasForeignKey(d => d.Warehouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cells_fk1");
        });

        modelBuilder.Entity<DistributionCenter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("distribution_centers_pk");

            entity.ToTable("distribution_centers");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.DistributionCenters)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("distribution_centers_distribution_center_types_fk");
        });

        modelBuilder.Entity<DistributionCenterType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("distribution_center_types_pk");

            entity.ToTable("distribution_center_types");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pk");

            entity.ToTable("products");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Photopath)
                .HasColumnType("character varying")
                .HasColumnName("photopath");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_product_type_fk");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_type_pk");

            entity.ToTable("product_type");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<RightAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("right_accesses_pk");

            entity.ToTable("right_accesses");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<RightAccessToUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("right_access_to_users_pk");

            entity.ToTable("right_access_to_users");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AccessId).HasColumnName("access_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Access).WithMany(p => p.RightAccessToUsers)
                .HasForeignKey(d => d.AccessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("right_access_to_users_right_accesses_fk");

            entity.HasOne(d => d.User).WithMany(p => p.RightAccessToUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("right_access_to_users_users_fk_1");
        });

        modelBuilder.Entity<SizeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("size_types_pk");

            entity.ToTable("size_types");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<StatusType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_types_pk");

            entity.ToTable("status_types");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("supplies_pk");

            entity.ToTable("supplies");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.DeliveryEnd)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("delivery_end");
            entity.Property(e => e.DeliveryStart)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("delivery_start");
            entity.Property(e => e.DepartWarehouseId).HasColumnName("depart_warehouse_id");
            entity.Property(e => e.DestinationWarehouseId).HasColumnName("destination_warehouse_id");
            entity.Property(e => e.Product)
                .ValueGeneratedOnAdd()
                .HasColumnName("product");
            entity.Property(e => e.Size)
                .ValueGeneratedOnAdd()
                .HasColumnName("size");
            entity.Property(e => e.Status)
                .ValueGeneratedOnAdd()
                .HasColumnName("status");
            entity.Property(e => e.TruckId).HasColumnName("truck_id");

            entity.HasOne(d => d.DepartWarehouse).WithMany(p => p.SupplyDepartWarehouses)
                .HasForeignKey(d => d.DepartWarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplies_warehouses_fk");

            entity.HasOne(d => d.DestinationWarehouse).WithMany(p => p.SupplyDestinationWarehouses)
                .HasForeignKey(d => d.DestinationWarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplies_warehouses_fk_1");

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.Product)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplies_products_fk");

            entity.HasOne(d => d.SizeNavigation).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.Size)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplies_size_types_fk");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplies_fk2");

            entity.HasOne(d => d.Truck).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.TruckId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplies_trucks_fk");
        });

        modelBuilder.Entity<Truck>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("newtable_pk");

            entity.ToTable("trucks");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.Number)
                .HasColumnType("character varying")
                .HasColumnName("number");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ModelNavigation).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Model)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trucks_truck_models_fk");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trucks_truck_statuses_fk");
        });

        modelBuilder.Entity<TruckModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("truck_models_pk");

            entity.ToTable("truck_models");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<TruckStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("truck_statuses_pk");

            entity.ToTable("truck_statuses");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasMaxLength(30)
                .HasColumnName("login");
            entity.Property(e => e.Passport)
                .HasMaxLength(10)
                .HasColumnName("passport");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasColumnType("character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Photopath)
                .HasColumnType("character varying")
                .HasColumnName("photopath");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_fk0");
        });

        modelBuilder.Entity<UsersType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_type_pk");

            entity.ToTable("users_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("warehouses_pk");

            entity.ToTable("warehouses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Class).HasColumnName("class");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.ClassNavigation).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.Class)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouses_warehouses_classes_fk");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouses_warehouse_types_fk");
        });

        modelBuilder.Entity<WarehouseType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("warehouse_types_pk");

            entity.ToTable("warehouse_types");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<WarehousesAccessToUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("warehouses_access_to_user_pk");

            entity.ToTable("warehouses_access_to_user");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");

            entity.HasOne(d => d.User).WithMany(p => p.WarehousesAccessToUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouses_access_to_user_users_fk");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.WarehousesAccessToUsers)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warehouses_access_to_user_warehouses_fk");
        });

        modelBuilder.Entity<WarehousesClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("warehouses_classes_pk");

            entity.ToTable("warehouses_classes");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
