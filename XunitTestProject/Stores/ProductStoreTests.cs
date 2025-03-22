using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoMapper;
using LuckysDepartmentStore.Utilities;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Data.Stores;
using Microsoft.CodeAnalysis;

namespace XunitTestProject.Stores
{
    public class ProductStoreTests
    {
        private LuckysContext GetInMemoryDbContext()
        {
            // Create an in-memory database with a unique name per test
            var options = new DbContextOptionsBuilder<LuckysContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new LuckysContext(options);
            // Seed initial data if needed
            context.Products.Add(new Product { ProductID = 1, Price = 5.00m, Description = "test 1", ProductName = "Test1", CategoryID = 1, SubCategoryID = 1, DiscountID = 0, DiscountTag = "mens summer Clothes", SearchWords = "mens shirt", BrandID = 1 });
            context.Products.Add(new Product { ProductID = 2, Price = 15.99m, Description = "test 2", ProductName = "Test2", CategoryID = 1, SubCategoryID = 1, DiscountID = 0, DiscountTag = "", SearchWords = "mens shirt", BrandID = 2 });
            context.Products.Add(new Product { ProductID = 3, Price = 45.99m, Description = "test 3", ProductName = "Test3", CategoryID = 1, SubCategoryID = 2, DiscountID = 0, DiscountTag = "mens summer Clothes", SearchWords = "mens shorts", BrandID = 1 });
            context.Products.Add(new Product { ProductID = 4, Price = 20.00m, Description = "test 4", ProductName = "Test4", CategoryID = 1, SubCategoryID = 1, DiscountID = 0, DiscountTag = "", SearchWords = "mens shirt" , BrandID = 3 });
            context.Products.Add(new Product { ProductID = 5, Price = 16.00m, Description = "test 5", ProductName = "Test5", CategoryID = 2, SubCategoryID = 1, DiscountID = 0, DiscountTag = "womens summer Clothes", SearchWords = "womens shirt", BrandID = 3 });
            context.Products.Add(new Product { ProductID = 6, Price = 7.99m, Description = "test 6", ProductName = "Test6", CategoryID = 6, SubCategoryID = 1, DiscountID = 0, DiscountTag = "girls summer Clothes", SearchWords = "girls shirt", BrandID = 1 });
            context.Products.Add(new Product { ProductID = 7, Price = 2.99m, Description = "test 7", ProductName = "Test7", CategoryID = 5, SubCategoryID = 2, DiscountID = 0, DiscountTag = "boys summer Clothes", SearchWords = "boys shorts", BrandID = 3 });
            context.Products.Add(new Product { ProductID = 8, Price = 9.99m, Description = "test 8", ProductName = "Test8", CategoryID = 7, SubCategoryID = 3, DiscountID = 0, DiscountTag = "babys bottles", SearchWords = "baby bottle", BrandID = 3 });
            context.Products.Add(new Product { ProductID = 9, Price = 5.99m, Description = "test 9", ProductName = "Test9", CategoryID = 2, SubCategoryID = 5, DiscountID = 0, DiscountTag = "womens winter clothes", SearchWords = "womens jacket", BrandID = 5 });
            context.Products.Add(new Product { ProductID = 10, Price = 6.15m, Description = "test 10", ProductName = "Test10", CategoryID = 5, SubCategoryID = 7, DiscountID = 0, DiscountTag = "boys clothes", SearchWords = "boys socks", BrandID = 1 });
            context.Products.Add(new Product { ProductID = 11, Price = 7.15m, Description = "test 11", ProductName = "Test11", CategoryID = 1, SubCategoryID = 6, DiscountID = 0, DiscountTag = "mens winter Clothes", SearchWords = "mens coat", BrandID = 2 });

            // Seed Categories table
            context.Categories.Add(new Category { CategoryID = 1, CategoryName = "Mens", CategoryDescription = "mens clothes" });
            context.Categories.Add(new Category { CategoryID = 2, CategoryName = "Womens", CategoryDescription = "womens clothes" });
            context.Categories.Add(new Category { CategoryID = 3, CategoryName = "Kids", CategoryDescription = "kids" });
            context.Categories.Add(new Category { CategoryID = 5, CategoryName = "Boys", CategoryDescription = "boys clothes" });
            context.Categories.Add(new Category { CategoryID = 6, CategoryName = "Girls", CategoryDescription = "girls clothes" });
            context.Categories.Add(new Category { CategoryID = 7, CategoryName = "babys", CategoryDescription = "infant" });

            // Seed subCategories table
            context.SubCategories.Add(new SubCategory { SubCategoryID = 1, SubCategoryName = "shirt", SubCategoryDescription = "shirts" });
            context.SubCategories.Add(new SubCategory { SubCategoryID = 2, SubCategoryName = "shorts", SubCategoryDescription = "shorts" });
            context.SubCategories.Add(new SubCategory { SubCategoryID = 3, SubCategoryName = "bottles", SubCategoryDescription = "bottles" });
            context.SubCategories.Add(new SubCategory { SubCategoryID = 5, SubCategoryName = "jackets", SubCategoryDescription = "jackets" });
            context.SubCategories.Add(new SubCategory { SubCategoryID = 6, SubCategoryName = "coats", SubCategoryDescription = "coats" });
            context.SubCategories.Add(new SubCategory { SubCategoryID = 7, SubCategoryName = "socks", SubCategoryDescription = "socks" });

            // Seed Brand table
            context.Brand.Add(new Brand { BrandId = 1, BrandName = "Lucky's" });
            context.Brand.Add(new Brand { BrandId = 2, BrandName = "GAP" });
            context.Brand.Add(new Brand { BrandId = 3, BrandName = "H&M" });
            context.Brand.Add(new Brand { BrandId = 5, BrandName = "KUHL" });
            context.Brand.Add(new Brand { BrandId = 6, BrandName = "Nike" });
            context.Brand.Add(new Brand { BrandId = 7, BrandName = "Calven Klien" });

            // Seed subCategories table
            context.Discounts.Add(new Discount { DiscountID = 1, DiscountTag = null, SubCategoryID = 7, CategoryID = null, BrandID = null, ProductID = null, DiscountPercent = 5, DiscountAmount = 0, DiscountActive = true });
            context.Discounts.Add(new Discount { DiscountID = 2, DiscountTag = null, SubCategoryID = null, CategoryID = 7, BrandID = null, ProductID = null, DiscountPercent = 0, DiscountAmount = 10, DiscountActive = true });
            context.Discounts.Add(new Discount { DiscountID = 3, DiscountTag = null, SubCategoryID = null, CategoryID = null, BrandID = 5, ProductID = null, DiscountPercent = 10, DiscountAmount = 0, DiscountActive = true });
            context.Discounts.Add(new Discount { DiscountID = 5, DiscountTag = null, SubCategoryID = null, CategoryID = null, BrandID = null, ProductID = 6, DiscountPercent = 0, DiscountAmount = 7, DiscountActive = true });
            context.Discounts.Add(new Discount { DiscountID = 6, DiscountTag = "mens summer clothes", SubCategoryID = null, CategoryID = null, BrandID = null, ProductID = null, DiscountPercent = 10, DiscountAmount = 0, DiscountActive = true });
            context.Discounts.Add(new Discount { DiscountID = 7, DiscountTag = "womens winter clothes", SubCategoryID = null, CategoryID = null, BrandID = null, ProductID = null, DiscountPercent = 0, DiscountAmount = 15, DiscountActive = true });
            context.Discounts.Add(new Discount { DiscountID = 8, DiscountTag = "boys clothes", SubCategoryID = null, CategoryID = 5, BrandID = null, ProductID = null, DiscountPercent = 5, DiscountAmount = 0, DiscountActive = true });
            context.Discounts.Add(new Discount { DiscountID = 9, DiscountTag = null, SubCategoryID = 1, CategoryID = null, BrandID = null, ProductID = null, DiscountPercent = 5, DiscountAmount = 0, DiscountActive = false });

            context.ColorProducts.Add(new ColorProduct { ColorProductID=1, ColorID =1, Quantity =2, ProductID =4, SizeID =1});

            context.SaveChanges();
            return context;
        }

        private (LuckysContext context, Mock<IMapper> mapperMock, Mock<IUtility> utilityMock) GetTestSetup()
        {
            var context = GetInMemoryDbContext();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Product>(It.IsAny<Product>())).Returns((Product p) => p);

            mapperMock.Setup(m => m.Map<List<ProductVM>>(It.IsAny<List<ProductVmDTO>>()))
             .Returns((List<ProductVmDTO> source) => source.Select(dto => new ProductVM
             {
                 ProductID = dto.ProductID,
                 ProductName = dto.ProductName,
                 Price = dto.Price,
                 Description = dto.Description,
                 Quantity = dto.Quantity,
                 Category = dto.Category,
                 SubCategory = dto.SubCategory,
                 Brand = dto.Brand,
                 CreatedDate = dto.CreatedDate,
                 ProductPicture = dto.ProductPicture,                 
                 ProductImage = null,
                 DiscountAmount = dto.DiscountAmount,
                 DiscountPercent = dto.DiscountPercent,
                 SalePrice = dto.DiscountPercent.HasValue ? dto.Price * (1 - dto.DiscountPercent.Value) : dto.Price
             }).ToList());


            var utilityMock = new Mock<IUtility>();
            utilityMock.Setup(u => u.MapEditProduct(It.IsAny<ProductDTO>())).Returns((ProductDTO dto) => new ProductEditVM { ProductID = dto.ProductID, ProductName = dto.ProductName });
            utilityMock.Setup(u => u.BytesToImage(It.IsAny<byte[]>())).Returns("mockedImageString");
            utilityMock.Setup(u => u.DefaultImage()).Returns("mockedDefaultImage");

            return (context, mapperMock, utilityMock);

        }

        [Fact]
        public void GetProductById_ReturnsCorrectProduct()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            // Act
            var product = repository.GetItem(1);

            // Assert
            Assert.NotNull(product);
            Assert.Equal("Test1", product.Result.ProductName);
            Assert.Equal(5.00m, product.Result.Price);
        }

        [Fact]
        public void ProductWithDiscountTagsCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            List<string> tags = new List<string>();
            tags.Add("boys clothes");
            tags.Add("mens summer Clothes");
          
            // Act
            var product = repository.ProductsWithDiscountsByTag(tags);
            // Assert
            Assert.NotNull(product);
            Assert.Equal(product.Result.Count,3);
          
        }

        [Fact]
        public void GetProductsSearchCategoryCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);           

                // Act
                var product = repository.GetProductsSearch("Mens", null,null,null);
                // Assert
                Assert.NotNull(product);
                Assert.Equal(product.Result.Count, 5);
           
        }

        [Fact]
        public void GetProductsSearchSubCategoryCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

           
            // Act
            var product = repository.GetProductsSearch(null, "socks", null, null);
            // Assert
            Assert.NotNull(product);
            Assert.Equal(product.Result.Count, 1);
           
        }

        [Fact]
        public void GetProductsSearchBrandCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);
          
            // Act
            var product = repository.GetProductsSearch(null, null, "GAP", null);
            // Assert
            Assert.NotNull(product);
            Assert.Equal(product.Result.Count, 2);
        }

        [Fact]
        public void GetProductsSearchStringCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);
           
            // Act
            var product = repository.GetProductsSearch(null, null, null, "mens shirt");
            // Assert
            Assert.NotNull(product);
            Assert.Equal(product.Result.Count, 4);
        }

        [Fact]
        public void GetProductsByIDSearchCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            // Act
            var product = repository.GetProductDiscountByIDSearch(6);
            // Assert
            Assert.NotNull(product);
            Assert.Equal(product.Result.Count, 1);
            Assert.Equal(product.Result[0].DiscountAmount, 7);

        }

        [Fact]
        public void GetProductSearchStringCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            string searchString = "coat men";
          
            // Act
            var product = repository.GetProductSearchString(searchString);
            // Assert
            Assert.NotNull(product);
            Assert.Equal(product.Result.Count, 1);
            Assert.Equal(product.Result[0].Price, 7.15m);
        }
      
        [Fact]
        public void GetAllCategories()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            // Act
            var categories = repository.Categories();
           
            // Assert
            Assert.NotNull(categories);
            Assert.Equal(6, categories.Result.Count);
        }

        [Fact]
        public void GetAllSubCategories()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            // Act
            var subCategories = repository.SubCategories();

            // Assert
            Assert.NotNull(subCategories);
            Assert.Equal(6, subCategories.Result.Count);
        }

        [Fact]
        public void GetAllBrands()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            // Act
            var brands = repository.Brand();

            // Assert
            Assert.NotNull(brands);
            Assert.Equal(6, brands.Result.Count);
        }

        [Fact]
        public void AllProductAllInfoNoDiscountCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            // Act
            var products = repository.AllProductAllInfoNoDiscount();

            // Assert
            Assert.NotNull(products);
            Assert.Equal(11, products.Result.Count);
        }

        [Fact]
        public void ProductsWithDiscountsSelectionCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            // Act
            var products = repository.ProductsWithDiscountsSelection();

            // Assert
            Assert.NotNull(products);
            Assert.Equal(9, products.Result.Count);
        }

        [Fact]
        public void ProductsWithDiscountsByTagCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            int productID = 2;

            // Act
            var products = repository.GetProductByIDNoDiscount(productID);

            // Assert
            Assert.NotNull(products);
        }

        [Fact]
        public void ProductByIDCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            int productID = 2;

            // Act
            var products = repository.ProductByID(productID);

            // Assert
            Assert.NotNull(products);
        }

        [Fact]
        public async Task UpdateProductDescriptionCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            var product = repository.ProductByID(4);
            List<ColorProduct> colorAdd = new List<ColorProduct>();
            List<ColorProduct> colorRemove = new List<ColorProduct>();
            product.Result.Description = "New";

            // Act
            await repository.UpdateProduct(product.Result, colorAdd, colorRemove);

            var productChanged = repository.ProductByID(4);

            // Assert
            Assert.NotNull(productChanged);
            Assert.Equal("New", productChanged.Result.Description);
        }

        [Fact]
        public async Task UpdateProductAddColorCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            var product = repository.ProductByID(4);
            List<ColorProduct> colorAdd = new List<ColorProduct>();
            List<ColorProduct> colorRemove = new List<ColorProduct>();

            var newColor = new ColorProduct { ColorProductID = 2, ColorID = 1, Quantity = 2, ProductID = 4, SizeID = 1 };
            colorAdd.Add(newColor);

            // Act
            await repository.UpdateProduct(product.Result, colorAdd, colorRemove);

            var productChanged = repository.ProductByID(4);

            var color = context.ColorProducts.Find(2);

            // Assert
            Assert.NotNull(productChanged);
            Assert.NotNull(color);
        }
        [Fact]
        public async Task UpdateProductRemoveColorCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            var product = repository.ProductByID(4);
            List<ColorProduct> colorAdd = new List<ColorProduct>();
            List<ColorProduct> colorRemove = new List<ColorProduct>();
            colorRemove.Add(context.ColorProducts.Find(1));


            // Act
            await repository.UpdateProduct(product.Result, colorAdd, colorRemove);
            var color = context.ColorProducts.Find(2);

            // Assert
            Assert.Null(color);
        }

        [Fact]
        public async Task UpdateProductAddRemoveColorCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            var product = repository.ProductByID(4);
            List<ColorProduct> colorAdd = new List<ColorProduct>();
            List<ColorProduct> colorRemove = new List<ColorProduct>();
            colorRemove.Add(context.ColorProducts.Find(1));

            var newColor = new ColorProduct { ColorProductID = 2, ColorID = 1, Quantity = 2, ProductID = 4, SizeID = 1 };
            colorAdd.Add(newColor);

            // Act
            await repository.UpdateProduct(product.Result, colorAdd, colorRemove);
            var color = context.ColorProducts.Find(2);

            var colorProducts = context.ColorProducts.ToList();

            // Assert
            Assert.NotNull(color);
            Assert.Equal(1,colorProducts.Count);
        }

        [Fact]
        public void DeleteProductCorrect()
        {
            // Arrange
            var (context, mapperMock, utilityMock) = GetTestSetup();
            var repository = new ProductStore(context, mapperMock.Object, utilityMock.Object);

            int productID = 1;

            // Act
            var product = repository.DeleteProduct(productID);
            // Assert
            Assert.True(product.Result);
            Assert.Equal(10, context.Products.Count());
        }
    }
}
