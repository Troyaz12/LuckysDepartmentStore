using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckysDepartmentStore.Data.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "6770720c-9f8f-4525-9241-c05a8ac4f861",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new IdentityRole
                {
                    Id = "720cd9c8-9418-4305-b2b9-c42017f5c8e4",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }

           );
        }
    }
}
