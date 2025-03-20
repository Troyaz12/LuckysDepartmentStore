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
                                CategoryName = "Men's",
                                CategoryDescription = "Men's products."
                            },
                           new Category
                           {
                               CategoryID = 2,
                               CategoryName = "Women's",
                               CategoryDescription = "Women's products."
                           },
                           new Category
                           {
                               CategoryID = 3,
                               CategoryName = "Boys",
                               CategoryDescription = "Boys youths"
                           },
                           new Category
                           {
                               CategoryID = 4,
                               CategoryName = "Girls",
                               CategoryDescription = "Girls youths"
                           },
                           new Category
                           {
                               CategoryID = 5,
                               CategoryName = "Boys Infant",
                               CategoryDescription = "Boys infant products"
                           },
                           new Category
                           {
                               CategoryID = 6,
                               CategoryName = "Girls Infant",
                               CategoryDescription = "Girls infant products"
                           }
            );
        }
    }
}
