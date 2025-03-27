using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckysDepartmentStore.Models;

namespace XunitTestProject.Stores
{
    public class SubcategoryStoreTests
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

            return context;
        }

        [Fact]
        public async Task CreateSubcategoryCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new SubcategoryStore(context);

            var subCategory = new SubCategory {SubCategoryName = "Jeans", SubCategoryDescription = "Jeans Desc"};

            // Act
            var ID = await repository.CreateSubcategory(subCategory);

            // Assert
            Assert.True(ID > 0);

        }
    }
}
