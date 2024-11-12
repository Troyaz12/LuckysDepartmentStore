using LuckysDepartmentStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id= "6770720c-9f8f-4525-9241-c05a8ac4f861",
                    Name="Customer",
                    NormalizedName = "CUSTOMER"
                }, 
                new IdentityRole
                {
                    Id = "720cd9c8-9418-4305-b2b9-c42017f5c8e4",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }

           );

            var hasher = new PasswordHasher<IdentityUser>();

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "d601656b-5848-4236-96d5-d722d471089d",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    UserName = "admin@localhost.com",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "720cd9c8-9418-4305-b2b9-c42017f5c8e4",
                    UserId = "d601656b-5848-4236-96d5-d722d471089d"
                });
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

    }
}
