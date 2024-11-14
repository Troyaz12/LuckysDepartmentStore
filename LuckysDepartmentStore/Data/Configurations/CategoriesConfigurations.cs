using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckysDepartmentStore.Data.Configurations
{
    public class CategoriesConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "Jeans",
                    CategoryDescription = "Jeans"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Shirts",
                    CategoryDescription = "Shirts"
                },
                new Category
                {
                    CategoryID = 3,
                    CategoryName = "Shoes",
                    CategoryDescription = "Shoes"
                }
            );
        }
    }
}
