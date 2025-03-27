using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckysDepartmentStore.Models;

namespace XunitTestProject.Stores
{
    public class ColorStoreTest
    {
        private LuckysContext GetInMemoryDbContext()
        {
            // Create an in-memory database with a unique name per test
            var options = new DbContextOptionsBuilder<LuckysContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new LuckysContext(options);

            context.SaveChanges();
            return context;
        }

        private LuckysContext GetTestSetup()
        {
            var context = GetInMemoryDbContext();
            context.Colors.Add(new Color { ColorID = 1, Name = "Red"});

            context.Sizes.Add(new Sizes { SizesID = 1, Size = "L" });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void AddColorCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ColorsStore(context);
            
            // Act
            var ColorID = repository.AddColor("Yellow");


            // Assert
            Assert.NotNull(ColorID);
        }

        [Fact]
        public void GetColorNameCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ColorsStore(context);

            // Act
            var ColorName = repository.GetColorName(1);

            // Assert
            Assert.NotNull(ColorName);
            Assert.Equal("Red",ColorName.Result);
        }

        [Fact]
        public void GetSizeNameCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ColorsStore(context);

            // Act
            var ColorName = repository.GetSizeName(1);

            // Assert
            Assert.NotNull(ColorName);
            Assert.Equal("L", ColorName.Result);
        }

        [Fact]
        public void AddSizeCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ColorsStore(context);

            // Act
            var ColorID = repository.CreateSize("L");


            // Assert
            Assert.NotNull(ColorID);
        }
    }
}
