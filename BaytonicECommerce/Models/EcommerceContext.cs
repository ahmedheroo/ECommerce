using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BaytonicECommerce.Models
{
    public partial class EcommerceContext : DbContext
    {
        public EcommerceContext()
        {
        }

        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<GovernmentShipping> GovernmentShippings { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<NewsLetter> NewsLetters { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<VwproductCategory> VwproductCategories { get; set; }
        public virtual DbSet<VwproductWithCategoryName> VwproductWithCategoryNames { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=ECommerce;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ImgUrl).HasColumnName("ImgURl");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Cart_Product");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ImgUrl).HasColumnName("ImgURL");

                entity.Property(e => e.NameA).IsRequired();

                entity.Property(e => e.NameE).IsRequired();

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Category_Category");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("Coupon");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.GeneratedById).HasColumnName("GeneratedByID");

                entity.Property(e => e.NameA).IsRequired();

                entity.Property(e => e.NameE).IsRequired();

                entity.Property(e => e.Percent).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpToValue).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<GovernmentShipping>(entity =>
            {
                entity.ToTable("GovernmentShipping");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.ShippingPrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Message1)
                    .IsRequired()
                    .HasColumnName("Message");

                entity.Property(e => e.SeenByUserId)
                    .HasMaxLength(450)
                    .HasColumnName("SeenByUserID");
            });

            modelBuilder.Entity<NewsLetter>(entity =>
            {
                entity.ToTable("NewsLetter");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientMail).IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DeliveryDate).HasColumnType("date");

                entity.Property(e => e.DicountValue)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DiscountCouponId).HasColumnName("DiscountCouponID");

                entity.Property(e => e.GovShippingId).HasColumnName("GovShippingID");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.GovShipping)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.GovShippingId)
                    .HasConstraintName("FK_Order_GovernmentShipping");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_AspNetUsers");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("OrderProduct");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderProduct_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderProduct_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasDefaultValueSql("((0))");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.FirstImgUrl).HasColumnName("FirstImgURL");

                entity.Property(e => e.NameA).IsRequired();

                entity.Property(e => e.NameE).IsRequired();

                entity.Property(e => e.Offer).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_Category");
            });

            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.ToTable("ProductDescription");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DescriptionA).IsRequired();

                entity.Property(e => e.DescriptionE).IsRequired();

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDescriptions)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductDescription_Product");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ImgUrl)
                    .IsRequired()
                    .HasColumnName("ImgURL");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
            });

            modelBuilder.Entity<VwproductCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VWProduct&Category");

                entity.Property(e => e.CatNameA).IsRequired();

                entity.Property(e => e.CatNameE).IsRequired();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NameA).IsRequired();

                entity.Property(e => e.NameE).IsRequired();

                entity.Property(e => e.Offer).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<VwproductWithCategoryName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VWProductWithCategoryName");

                entity.Property(e => e.CatNameA).IsRequired();

                entity.Property(e => e.CatNameE).IsRequired();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NameA).IsRequired();

                entity.Property(e => e.NameE).IsRequired();

                entity.Property(e => e.Offer).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<WishList>(entity =>
            {
                entity.ToTable("WishList");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ImgUrl).HasColumnName("ImgURl");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_WishList_Product");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
