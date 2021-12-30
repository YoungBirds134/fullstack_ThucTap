using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Persistence.Contexts
{
    public partial class LangCodeApiContext : DbContext
    {
        public LangCodeApiContext()
        {
        }

        public LangCodeApiContext(DbContextOptions<LangCodeApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionInFunctions> ActionInFunctions { get; set; }
        public virtual DbSet<Actions> Actions { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AttributeOptionValues> AttributeOptionValues { get; set; }
        public virtual DbSet<AttributeOptions> AttributeOptions { get; set; }
        public virtual DbSet<AttributeValueDateTimes> AttributeValueDateTimes { get; set; }
        public virtual DbSet<AttributeValueDecimals> AttributeValueDecimals { get; set; }
        public virtual DbSet<AttributeValueInts> AttributeValueInts { get; set; }
        public virtual DbSet<AttributeValueText> AttributeValueText { get; set; }
        public virtual DbSet<AttributeValueVarchars> AttributeValueVarchars { get; set; }
        public virtual DbSet<Attributes> Attributes { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Functions> Functions { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<ProductInCategories> ProductInCategories { get; set; }
        public virtual DbSet<ProductTranslations> ProductTranslations { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost,1433;Database=LangCodeApi;User Id=sa;Password=Password789;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionInFunctions>(entity =>
            {
                entity.HasKey(e => new { e.FunctionId, e.ActionId });

                entity.Property(e => e.FunctionId).IsUnicode(false);

                entity.Property(e => e.ActionId).IsUnicode(false);

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.ActionInFunctions)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActionInFunctions_Actions");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.ActionInFunctions)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActionInFunctions_Functions");
            });

            modelBuilder.Entity<Actions>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AttributeOptionValues>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.AttributeOptionValues)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("FK_AttributeOptionValues_AttributeOptions");
            });

            modelBuilder.Entity<AttributeOptions>(entity =>
            {
                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.AttributeOptions)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_AttributeOptions_Attributes");
            });

            modelBuilder.Entity<AttributeValueDateTimes>(entity =>
            {
                entity.Property(e => e.LanguageId).IsUnicode(false);

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.AttributeValueDateTimes)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_AttributeValueDateTimes_Attributes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AttributeValueDateTimes)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_AttributeValueDateTimes_Products");
            });

            modelBuilder.Entity<AttributeValueDecimals>(entity =>
            {
                entity.Property(e => e.LanguageId).IsUnicode(false);

                entity.Property(e => e.Value).IsUnicode(false);

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.AttributeValueDecimals)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_AttributeValueDecimals_Attributes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AttributeValueDecimals)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_AttributeValueDecimals_Products");
            });

            modelBuilder.Entity<AttributeValueInts>(entity =>
            {
                entity.Property(e => e.LanguageId).IsUnicode(false);

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.AttributeValueInts)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_AttributeValueInts_Attributes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AttributeValueInts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_AttributeValueInts_Products");
            });

            modelBuilder.Entity<AttributeValueText>(entity =>
            {
                entity.Property(e => e.LanguageId).IsUnicode(false);

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.AttributeValueText)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_AttributeValueText_Attributes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AttributeValueText)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_AttributeValueText_Products");
            });

            modelBuilder.Entity<AttributeValueVarchars>(entity =>
            {
                entity.Property(e => e.LanguageId).IsUnicode(false);

                entity.Property(e => e.Value).IsUnicode(false);

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.AttributeValueVarchars)
                    .HasForeignKey(d => d.AttributeId)
                    .HasConstraintName("FK_AttributeValueVarchars_Attributes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AttributeValueVarchars)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_AttributeValueVarchars_Products");
            });

            modelBuilder.Entity<Attributes>(entity =>
            {
                entity.Property(e => e.BackendType).IsUnicode(false);

                entity.Property(e => e.Code).IsUnicode(false);
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.SeoAlias).IsUnicode(false);
            });

            modelBuilder.Entity<Functions>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.ParentId).IsUnicode(false);
            });

            modelBuilder.Entity<Languages>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.OrderId });

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Products");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.CustomerPhone).IsUnicode(false);
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ActionId).IsUnicode(false);

                entity.Property(e => e.FunctionId).IsUnicode(false);

                entity.HasOne(d => d.Action)
                    .WithMany()
                    .HasForeignKey(d => d.ActionId)
                    .HasConstraintName("FK_Permissions_Actions");

                entity.HasOne(d => d.Function)
                    .WithMany()
                    .HasForeignKey(d => d.FunctionId)
                    .HasConstraintName("FK_Permissions_Functions");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Permissions_AspNetRoles");
            });

            modelBuilder.Entity<ProductInCategories>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CategoryId });

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductInCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInCategories_Categories");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInCategories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInCategories_Products");
            });

            modelBuilder.Entity<ProductTranslations>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.LanguageId });

                entity.Property(e => e.LanguageId).IsUnicode(false);

                entity.Property(e => e.SeoAlias).IsUnicode(false);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.ProductTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductTranslations_Languages");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductTranslations)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductTranslations_Products");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Sku).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
