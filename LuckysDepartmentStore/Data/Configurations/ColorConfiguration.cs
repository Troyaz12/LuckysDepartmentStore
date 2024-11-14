using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Data.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Color> builder)
        {
            builder.HasData(
                new Color
                {
                    ColorID = 1,
                    Name="Red"
                },
                new Color
                {
                    ColorID = 2,
                    Name = "Green"
                },
                new Color
                {
                    ColorID = 3,
                    Name = "Blue"
                }
            );
        }
    }
}
