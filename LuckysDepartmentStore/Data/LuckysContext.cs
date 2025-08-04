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
             .HasOne(co => co.CustomerShippingData)
             .WithMany()
             .HasForeignKey(co => co.ShippingID)
             .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CustomerOrder>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Shipping)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Brand>()
            .Property(c => c.Created)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<Carts>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<Category>()
           .Property(c => c.CreatedDate)
           .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<Color>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<ColorProduct>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<Consumer>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<CustomerOrderItem>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<Discount>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<Order>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<Payment>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<PaymentOptions>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<Product>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<Rating>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<Shipping>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<ShippingAddress>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<SubCategory>()
            .Property(c => c.CreatedDate)
            .HasDefaultValueSql("SYSDATETIME()");

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
