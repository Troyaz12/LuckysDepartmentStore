using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Utilities;
using Moq;
using LuckysDepartmentStore.Models.DTO.Products;
using AutoMapper;
using LuckysDepartmentStore.Models.ViewModels.Discount;

namespace XunitTestProject.Stores
{
    public class DiscountStoreTests
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

        private (LuckysContext context, Mock<IUtility> utilityMock) GetTestSetup()
        {
            var context = GetInMemoryDbContext();

            context.Discounts.Add(
                new Discount
                {
                    DiscountID = 1,
                    DiscountPercent = .50m,
                    DiscountAmount = 1.00m,
                    DiscountActive = true,
                    CreatedDate = DateTime.Now,                    
                    DiscountDescription = "Sale On Jeans",                    
                    CategoryID = 2,
                    DiscountTag = "Jeans Sale"
                });

            context.Discounts.Add(
               new Discount
               {
                   DiscountID = 2,
                   DiscountPercent = 0,
                   DiscountAmount = 5.00m,
                   DiscountActive = true,
                   CreatedDate = DateTime.Now,
                   DiscountDescription = "Sale On Womens Clothes",
                   CategoryID = 2,
                   DiscountTag = "Womens Clothes"
               });

            context.Categories.Add(new Category { CategoryID = 2, CategoryName = "Mens", CategoryDescription = "mens clothes" });

            context.SaveChanges();

            var utilityMock = new Mock<IUtility>();
            utilityMock.Setup(u => u.MapEditProduct(It.IsAny<ProductDTO>())).Returns((ProductDTO dto) => new ProductEditVM { ProductID = dto.ProductID, ProductName = dto.ProductName });
            utilityMock.Setup(u => u.BytesToImage(It.IsAny<byte[]>())).Returns("mockedImageString");
            utilityMock.Setup(u => u.DefaultImage()).Returns("mockedDefaultImage");

            return (context, utilityMock);
        }

        [Fact]
        public async Task DiscountWithTagCorrect()
        {
            // Arrange
            var (context, utilityMock) = GetTestSetup();
            var repository = new DiscountStore(context, utilityMock.Object);

            // Act
            var allDiscounts = await repository.DiscountWithTag();

            // Assert
            Assert.NotNull(allDiscounts);
            Assert.Single(allDiscounts);
        }

        [Fact]
        public async Task GetDiscountByIDCorrect()
        {
            // Arrange
            var (context, utilityMock) = GetTestSetup();
            var repository = new DiscountStore(context, utilityMock.Object);

            // Act
            var discount = await repository.GetDiscountByID(1);

            // Assert
            Assert.NotNull(discount);
            Assert.Equal(1, discount.DiscountID); // Confirm it’s the right discount
            Assert.Equal("Sale On Jeans", discount.DiscountDescription); // Assuming Name is a property
        }

        [Fact]
        public async Task DeleteDiscountsCorrect()
        {
            // Arrange
            var (context, utilityMock) = GetTestSetup();
            var repository = new DiscountStore(context, utilityMock.Object);

            var discountDelete = context.Discounts.Find(1);

            // Act
            await repository.DeleteDiscounts(discountDelete);

            var discount = context.Discounts.Find(1);

            // Assert
            Assert.Null(discount);            
        }

        [Fact]
        public async Task GetDiscountsWithSelectionsCorrect()
        {
            // Arrange
            var (context, utilityMock) = GetTestSetup();
            var repository = new DiscountStore(context, utilityMock.Object);

            // Act
            var discount = await repository.GetDiscountsWithSelections(1);

            // Assert
            Assert.NotNull(discount);
            Assert.Equal("Mens",discount.CategorySelection);
        }

        [Fact]
        public async Task UpdateDiscountCorrect()
        {
            // Arrange
            var (context, utilityMock) = GetTestSetup();
            var repository = new DiscountStore(context, utilityMock.Object);

            var discountChange = 
                new DiscountEditVM
                {
                    DiscountID = 1,
                    DiscountPercent = .50m,
                    DiscountAmount = 1.00m,
                    DiscountActive = false,
                    CreatedDate = DateTime.Now,
                    DiscountDescription = "Sale On Jeans",
                    CategoryID = 2,
                    DiscountTag = "Jeans Sale"
                };

            // Act
            var discount = await repository.UpdateDiscount(discountChange);

            var changedDiscount = await context.Discounts.FindAsync(1);

            // Assert
            Assert.NotNull(discount);
            Assert.False(changedDiscount.DiscountActive);
        }

        [Fact]
        public async Task AddDiscountCorrect()
        {
            // Arrange
            var (context, utilityMock) = GetTestSetup();
            var repository = new DiscountStore(context, utilityMock.Object);

            var newDiscount = new Discount
            {                
                DiscountPercent = .50m,
                DiscountAmount = 1.00m,
                DiscountActive = true,
                CreatedDate = DateTime.Now,
                DiscountDescription = "Sale On Coats",
                CategoryID = 2,
                DiscountTag = "Coats Sale"
            };

            // Act
            await repository.AddDiscount(newDiscount);

            var updateDiscount = await context.Discounts.FindAsync(2);

            var allDiscounts = await context.Discounts.ToListAsync();

            // Assert
            Assert.NotNull(updateDiscount);
            Assert.Equal("Sale On Coats", updateDiscount.DiscountDescription);
            Assert.Equal(2, allDiscounts.Count);
        }

        [Fact]
        public async Task GetAllDiscountsCorrect()
        {
            // Arrange
            var (context, utilityMock) = GetTestSetup();
            var repository = new DiscountStore(context, utilityMock.Object);

            var discounts = await repository.GetAllDiscounts();

            // Assert
            Assert.NotNull(discounts);
            Assert.Equal(1, discounts.Count);
        }

        [Fact]
        public async Task GetDiscountByTagsCorrect()
        {
            // Arrange
            var (context, utilityMock) = GetTestSetup();
            var repository = new DiscountStore(context, utilityMock.Object);

            List<string> tags = new List<string>();

            tags.Add("Jeans Sale");
            tags.Add("Womens Clothes");

            var discounts = await repository.GetDiscountByTags(tags);

            // Assert
            Assert.NotNull(discounts);
            Assert.Equal(2, discounts.Count);
        }
    }
}
