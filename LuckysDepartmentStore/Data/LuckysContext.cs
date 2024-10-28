using Lucky_sDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Data
{
    public class LuckysContext : DbContext
    {
        public LuckysContext(DbContextOptions<LuckysContext> options)
        : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
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
