using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace fragrance.Models;

public partial class YourDbContext : DbContext
{
    public YourDbContext()
    {
    }

    public YourDbContext(DbContextOptions<YourDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FavoriteItem> FavoriteItems { get; set; }

    public virtual DbSet<Fragrance> Fragrances { get; set; }

    public virtual DbSet<FragranceImage> FragranceImages { get; set; }

    public virtual DbSet<FragranceReview> FragranceReviews { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=millisoft-project;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoriteItem>(entity =>
        {
            entity.HasKey(e => e.FavoriteItemId).HasName("favorite_items_pkey");

            entity.ToTable("favorite_items");

            entity.Property(e => e.FavoriteItemId).HasColumnName("favorite_item_id");
            entity.Property(e => e.FavoriteItemDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("favorite_item_date");
            entity.Property(e => e.FavoriteItemFragranceId).HasColumnName("favorite_item_fragrance_id");
            entity.Property(e => e.FavoriteItemUserId).HasColumnName("favorite_item_user_id");

            entity.HasOne(d => d.FavoriteItemFragrance).WithMany(p => p.FavoriteItems)
                .HasForeignKey(d => d.FavoriteItemFragranceId)
                .HasConstraintName("favorite_items_favorite_item_fragrance_id_fkey");

            entity.HasOne(d => d.FavoriteItemUser).WithMany(p => p.FavoriteItems)
                .HasForeignKey(d => d.FavoriteItemUserId)
                .HasConstraintName("favorite_items_favorite_item_user_id_fkey");
        });

        modelBuilder.Entity<Fragrance>(entity =>
        {
            entity.HasKey(e => e.FragranceId).HasName("fragrances_pkey");

            entity.ToTable("fragrances");

            entity.Property(e => e.FragranceId).HasColumnName("fragrance_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.FragranceBrand)
                .HasMaxLength(20)
                .HasColumnName("fragrance_brand");
            entity.Property(e => e.FragranceDesc).HasColumnName("fragrance_desc");
            entity.Property(e => e.FragranceGender).HasColumnName("fragrance_gender");
            entity.Property(e => e.FragranceImage).HasColumnName("fragrance_image");
            entity.Property(e => e.FragranceLine)
                .HasMaxLength(20)
                .HasColumnName("fragrance_line");
            entity.Property(e => e.FragranceLongDesc).HasColumnName("fragrance_long_desc");
            entity.Property(e => e.FragranceOldPrice).HasColumnName("fragrance_old_price");
            entity.Property(e => e.FragrancePrice).HasColumnName("fragrance_price");
            entity.Property(e => e.FragranceStock)
                .HasDefaultValue(0)
                .HasColumnName("fragrance_stock");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<FragranceImage>(entity =>
        {
            entity.HasKey(e => e.FragranceImageId).HasName("fragrance_images_pkey");

            entity.ToTable("fragrance_images");

            entity.Property(e => e.FragranceImageId).HasColumnName("fragrance_image_id");
            entity.Property(e => e.FragranceId).HasColumnName("fragrance_id");
            entity.Property(e => e.FragranceImageUrl).HasColumnName("fragrance_image_url");

            entity.HasOne(d => d.Fragrance).WithMany(p => p.FragranceImages)
                .HasForeignKey(d => d.FragranceId)
                .HasConstraintName("fragrance_images_fragrance_id_fkey");
        });

        modelBuilder.Entity<FragranceReview>(entity =>
        {
            entity.HasKey(e => e.FragranceReviewId).HasName("fragrance_reviews_pkey");

            entity.ToTable("fragrance_reviews");

            entity.Property(e => e.FragranceReviewId).HasColumnName("fragrance_review_id");
            entity.Property(e => e.FragranceReviewDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fragrance_review_date");
            entity.Property(e => e.FragranceReviewFragranceId).HasColumnName("fragrance_review_fragrance_id");
            entity.Property(e => e.FragranceReviewRating).HasColumnName("fragrance_review_rating");
            entity.Property(e => e.FragranceReviewText).HasColumnName("fragrance_review_text");
            entity.Property(e => e.FragranceReviewTitle)
                .HasMaxLength(50)
                .HasColumnName("fragrance_review_title");
            entity.Property(e => e.FragranceReviewUserId).HasColumnName("fragrance_review_user_id");

            entity.HasOne(d => d.FragranceReviewFragrance).WithMany(p => p.FragranceReviews)
                .HasForeignKey(d => d.FragranceReviewFragranceId)
                .HasConstraintName("fk_fragrance_review_fragrance_id");

            entity.HasOne(d => d.FragranceReviewUser).WithMany(p => p.FragranceReviews)
                .HasForeignKey(d => d.FragranceReviewUserId)
                .HasConstraintName("fragrance_reviews_fragrance_review_user_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Pending'::character varying")
                .HasColumnName("order_status");
            entity.Property(e => e.OrderTotalPrice).HasColumnName("order_total_price");
            entity.Property(e => e.OrderUserId).HasColumnName("order_user_id");

            entity.HasOne(d => d.OrderUser).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderUserId)
                .HasConstraintName("orders_order_user_id_fkey");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("order_tems_pkey");

            entity.ToTable("order_items");

            entity.Property(e => e.OrderItemId)
                .HasDefaultValueSql("nextval('order_tems_order_item_id_seq'::regclass)")
                .HasColumnName("order_item_id");
            entity.Property(e => e.OrderItemFragranceId).HasColumnName("order_item_fragrance_id");
            entity.Property(e => e.OrderItemOrderId).HasColumnName("order_item_order_id");
            entity.Property(e => e.OrderItemQuantity).HasColumnName("order_item_quantity");
            entity.Property(e => e.OrderItemUnitPrice).HasColumnName("order_item_unit_price");

            entity.HasOne(d => d.OrderItemFragrance).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderItemFragranceId)
                .HasConstraintName("order_tems_order_item_fragrance_id_fkey");

            entity.HasOne(d => d.OrderItemOrder).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderItemOrderId)
                .HasConstraintName("order_tems_order_item_order_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserEmail, "unique_user_email").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .HasColumnName("user_email");
            entity.Property(e => e.UserGender).HasColumnName("user_gender");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(60)
                .HasColumnName("user_password");
            entity.Property(e => e.UserRole)
                .HasMaxLength(10)
                .HasDefaultValueSql("'customer'::character varying")
                .HasColumnName("user_role");
            entity.Property(e => e.UserSurname)
                .HasMaxLength(20)
                .HasColumnName("user_surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
