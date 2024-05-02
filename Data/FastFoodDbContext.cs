using FastFoodEFC.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodEFC.Data
{
    public class FastFoodDbContext : DbContext
    {
        public FastFoodDbContext(DbContextOptions<FastFoodDbContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithMany(cust => cust.Carts)
                .HasForeignKey(c => c.CustId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.FoodItem)
                .WithMany(food => food.Carts)
                .HasForeignKey(c => c.FoodId);

            modelBuilder.Entity<FoodItem>()
                .HasOne(f => f.Category)
                .WithMany(cat => cat.FoodItems)
                .HasForeignKey(f => f.CatId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(cust => cust.Orders)
                .HasForeignKey(o => o.CustId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.FoodItem)
                .WithMany(food => food.Orders)
                .HasForeignKey(o => o.FoodId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(ord => ord.Payments)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Role>()
                .HasOne(r => r.Admin)
                .WithMany(admin => admin.Roles)
                .HasForeignKey(r => r.AdminId);
        }
    }
}
