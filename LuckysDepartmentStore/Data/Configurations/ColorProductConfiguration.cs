using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckysDepartmentStore.Data.Configurations
{
    public class ColorProductConfiguration : IEntityTypeConfiguration<ColorProduct>
    {
        public void Configure(EntityTypeBuilder<ColorProduct> builder)
        {
            builder.HasOne<Product>()
                   .WithMany()
                   .HasForeignKey(cp => cp.ProductID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
