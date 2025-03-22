using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Models.ViewModels.Product;

namespace XunitTestProject.Stores
{
    public class BrandStoreTests
    {
        private LuckysContext GetInMemoryDbContext()
        {
            // Create an in-memory database with a unique name per test
            var options = new DbContextOptionsBuilder<LuckysContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new LuckysContext(options);
            // Seed initial data if needed
           // context.Products.Add(new Product { ProductID = 1, Price = 5.00m, Description = "test 1", ProductName = "Test1", CategoryID = 1, SubCategoryID = 1, DiscountID = 0, DiscountTag = "mens summer Clothes", SearchWords = "mens shirt", BrandID = 1 });
           
            context.SaveChanges();
            return context;
        }

        private LuckysContext GetTestSetup()
        {
            var context = GetInMemoryDbContext();

            return context;
        }

        [Fact]
        public void CreateBrandCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new BrandStore(context);

            ProductCreateVM product = new ProductCreateVM();
            product.BrandSelection = "GAP";

            // Act
            var brandID = repository.CreateBrand(product);

            // Assert
            Assert.NotNull(brandID);
            Assert.Equal(1, brandID.Result);            
        }


    }
}
