using LuckysDepartmentStore.Data.Configurations;
using LuckysDepartmentStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace LuckysDepartmentStore.Data
{
    public class LuckysContext : IdentityDbContext<ApplicationUser>
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

            builder.Entity<Carts>()
            .Property(c => c.Price)
            .HasColumnType("decimal(18,2)");

            builder.Entity<CustomerOrderItem>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Payment>()
                .Property(p => p.Total)
                .HasColumnType("decimal(18,2)");


            builder.Entity<ApplicationUser>()
                .HasMany(a => a.ShippingAddress)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
               .HasMany(a => a.PaymentOptions)
               .WithOne(b => b.User)
               .HasForeignKey(b => b.UserId);

            builder.Entity<ApplicationUser>()
              .HasOne(a => a.Consumer)
              .WithOne(b => b.User)
              .HasForeignKey<Consumer>(b => b.UserId);

            builder.Entity<ApplicationUser>()
             .HasMany(a => a.CustomerOrder)
             .WithOne(b => b.User)
             .HasForeignKey(b => b.UserId)
             .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CustomerOrder>()
             .HasOne(co => co.Customer)
             .WithMany()
             .HasForeignKey(co => co.ShippingAddressID)
             .OnDelete(DeleteBehavior.NoAction);

            builder.ApplyConfiguration(new ColorProductConfiguration());

        }

        public DbSet<Category> Categories { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public DbSet<ColorProduct> ColorProducts { get; set; }
        public DbSet<ShippingAddress> ShippingAddress { get; set; }
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
        public DbSet<Consumer> Consumer { get; set; }

    }
}
