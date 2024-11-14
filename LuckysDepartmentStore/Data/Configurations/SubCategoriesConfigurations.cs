using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckysDepartmentStore.Data.Configurations
{
    public class SubCategoriesConfigurations : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasData(
                 new SubCategory
                 {
                     SubCategoryID = 1,
                     SubCategoryName = "Mens",
                     SubCategoryDescription = "Mens products."
                 },
                new SubCategory
                {
                    SubCategoryID = 2,
                    SubCategoryName = "Womens",
                    SubCategoryDescription = "Womens products."
                },
                new SubCategory
                {
                    SubCategoryID = 3,
                    SubCategoryName = "Boys",
                    SubCategoryDescription = "Boys youths"
                },
                new SubCategory
                {
                    SubCategoryID = 4,
                    SubCategoryName = "Girls",
                    SubCategoryDescription = "Girls youths"
                },
                new SubCategory
                {
                    SubCategoryID = 5,
                    SubCategoryName = "Boys Infant",
                    SubCategoryDescription = "Boys infant products"
                },
                new SubCategory
                {
                    SubCategoryID = 6,
                    SubCategoryName = "Girls Infant",
                    SubCategoryDescription = "Girls infant products"
                }
             );
        }
    }
}
