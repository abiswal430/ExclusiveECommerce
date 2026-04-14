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
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ PRODUCT PRICE
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // ✅ PRODUCT DISCOUNT
            modelBuilder.Entity<Product>()
                .Property(p => p.Discount)
                .HasPrecision(18, 2);

            // ✅ CART PRICE
            modelBuilder.Entity<Cart>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            // ✅ ORDER TOTAL
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            // 🔥 FIX: ORDER ITEM PRICE (VERY IMPORTANT)
            modelBuilder.Entity<OrderItem>()
                .Property(o => o.Price)
                .HasPrecision(18, 2);
        }
    }
}