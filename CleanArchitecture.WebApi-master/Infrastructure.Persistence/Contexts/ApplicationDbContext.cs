using System;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost,1433;Database=CleanArchitectureApplicationDb;User Id=sa;Password=Password789;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.NameMenu).IsUnicode(false);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.nameCode)
                    .HasName("PK__Menu__4BB86E8D74B52EE3");

                entity.Property(e => e.nameCode).IsUnicode(false);

                entity.Property(e => e.nameMenu).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
