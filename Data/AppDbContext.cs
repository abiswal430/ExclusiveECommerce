using Microsoft.EntityFrameworkCore;
using ExclusiveMVC.Models;

namespace ExclusiveMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ PRODUCT PRICE
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // ✅ PRODUCT DISCOUNT (FIXED)
            modelBuilder.Entity<Product>()
                .Property(p => p.Discount)
                .HasPrecision(18, 2);

            // ✅ CART PRICE (FIXED)
            modelBuilder.Entity<Cart>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            // ✅ ORDER TOTAL
            modelBuilder.Entity<Cart>()
            .Property(c => c.Price)
            .HasColumnType("decimal(18,2)");
        }
    }
}