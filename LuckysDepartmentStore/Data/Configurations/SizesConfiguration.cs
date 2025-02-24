using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckysDepartmentStore.Data.Configurations
{
    public class SizesConfiguration : IEntityTypeConfiguration<Sizes>
    {
        public void Configure(EntityTypeBuilder<Sizes> builder)
        {
            builder.HasData(
                 new Sizes
                 {
                     SizesID = 1,
                     Size = "S"
                 },
                new Sizes
                {
                    SizesID = 2,
                    Size = "M"
                },
                new Sizes
                {
                    SizesID = 3,
                    Size = "L"
                },
                new Sizes
                {
                    SizesID = 4,
                    Size = "XL"
                },
                new Sizes
                {
                    SizesID = 5,
                    Size = "XXL"
                },
                new Sizes
                {
                    SizesID = 6,
                    Size = "XXXL"
                }
             );
        }
    }
}
