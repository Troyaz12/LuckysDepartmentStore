using LuckysDepartmentStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LuckysDepartmentStore.Data
{
    public class LuckysContext : IdentityDbContext
    {
        public LuckysContext(DbContextOptions<LuckysContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<Discount>()
                .Property(d => d.DiscountAmount)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Discount>()
              .Property(d => d.DiscountPercent)
              .HasColumnType("decimal(18,2)");

            builder.Entity<Product>()
           .Property(d => d.Price)
           .HasColumnType("decimal(18,2)");

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Customer)
                .WithOne(b => b.User)
                .HasForeignKey<Customer>(b => b.UserId);
        }

        public DbSet<Category> Categories { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public DbSet<ColorProduct> ColorProducts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOrderItem> CustomerOrderItems { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<PaymentOptions> PaymentOptions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Carts> Carts { get; set; }

    }
}
