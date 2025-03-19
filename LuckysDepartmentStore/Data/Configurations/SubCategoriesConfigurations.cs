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
                   SubCategoryName = "Jeans",
                   SubCategoryDescription = "Jeans"
               },
               new SubCategory
               {
                   SubCategoryID = 2,
                   SubCategoryName = "Shirts",
                   SubCategoryDescription = "Shirts"
               },
               new SubCategory
               {
                   SubCategoryID = 3,
                   SubCategoryName = "Shoes",
                   SubCategoryDescription = "Shoes"
               }
           );
        }
    }
}
