using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;

namespace XunitTestProject.Stores
{
    public class ShoppingCartTests
    {
        public string cartID = Guid.NewGuid().ToString();
        private LuckysContext GetInMemoryDbContext()
        {
            // Create an in-memory database with a unique name per test
            var options = new DbContextOptionsBuilder<LuckysContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new LuckysContext(options);
            // Seed initial data if needed

            context.Products.Add(new Product { ProductID = 18, Price = 5.00m, Description = "test 1", ProductName = "Test1", CategoryID = 1, SubCategoryID = 1, DiscountID = 1, DiscountTag = "mens summer Clothes", SearchWords = "mens shirt", BrandID = 1 });
            context.Products.Add(new Product { ProductID = 19, Price = 15.99m, Description = "test 2", ProductName = "Test2", CategoryID = 1, SubCategoryID = 1, DiscountID = 1, DiscountTag = "", SearchWords = "mens shirt", BrandID = 2 });
            context.Products.Add(new Product { ProductID = 3, Price = 45.99m, Description = "test 3", ProductName = "Test3", CategoryID = 2, SubCategoryID = 2, DiscountID = 0, DiscountTag = "mens summer Clothes", SearchWords = "mens shorts", BrandID = 1 });

            context.Carts.Add(new Carts { CartID = cartID, ProductID = 18, Price = 10.0m, Quantity = 12, CreatedDate = DateTime.Now, ProductName = "Jeans" });
            context.Carts.Add(new Carts { CartID = cartID, ProductID = 19, Price = 10.0m, Quantity = 12, CreatedDate = DateTime.Now, ProductName = "Shirt" });

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

            // Seed Categories table
            context.CustomerOrderItems.Add(new CustomerOrderItem { CustomerOrderItemID = 1, ProductID = 18, CustomerOrderID = 1, Price = 5.00m, Quantity = 5 });
            context.CustomerOrderItems.Add(new CustomerOrderItem { CustomerOrderItemID = 2, ProductID = 19, CustomerOrderID = 1, Price = 15.99m, Quantity = 2 });
            context.CustomerOrderItems.Add(new CustomerOrderItem { CustomerOrderItemID = 3, ProductID = 3, CustomerOrderID = 1, Price = 45.99m, Quantity = 3 });

            // Seed subCategories table
            context.Discounts.Add(new Discount { DiscountID = 1, DiscountTag = null, SubCategoryID = 7, CategoryID = null, BrandID = null, ProductID = null, DiscountPercent = 5, DiscountAmount = 0, DiscountActive = true });


            context.SaveChanges();
            return context;
        }

        private LuckysContext GetTestSetup()
        {
            var context = GetInMemoryDbContext();

            return context;
        }

        [Fact]
        public async Task CheckForItemInCartCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);
            var cartID = context.Carts.Find(1);
            var prod1 = new CartItemsDTO { ProductID = 18, Price = 10.0m, Quantity = 12, CreatedDate = DateTime.Now };

            // Act
            var addresses = repository.CheckForItemInCart(prod1, cartID.CartID);

            var allItems = await context.Carts.ToListAsync();
            // Assert
            Assert.NotNull(addresses);
        }

        [Fact]
        public async Task AddItemToNewCartCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);
            var prod1 = new Carts { ProductID = 20, Price = 10.0m, Quantity = 3, CreatedDate = DateTime.Now, ProductName = "Shirt", CartID = cartID };

            // Act
            var rowsEffected = await repository.AddItemToNewCart(prod1);

            // Assert
            Assert.NotNull(rowsEffected);
            Assert.Equal(1, rowsEffected);
        }

        [Fact]
        public async Task UpdateCartItemQuantityCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);
            var prod1 = await context.Carts.FindAsync(1);
            int newQuantity = 15;

            // Act
            var rowsEffected = await repository.UpdateCartItemQuantity(prod1, newQuantity);
            var prod1Updated = await context.Carts.FindAsync(1);

            // Assert
            Assert.NotNull(rowsEffected);
            Assert.Equal(1, rowsEffected);
            Assert.Equal(15, prod1Updated.Quantity);
        }

        [Fact]
        public async Task GetCartItemsCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);          
            // Act
            var cartItems = await repository.GetCartDataOnlyItems(cartID);
            
            // Assert
            Assert.NotNull(cartItems);
            Assert.Equal(2, cartItems.Count);
        }

        [Fact]
        public async Task RemoveFromCartCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);
            var prod1 = await context.Carts.FindAsync(1);

            // Act
            var rowsEffected = await repository.RemoveFromCart(prod1);
            var prod1Removed = await context.Carts.FindAsync(1);

            // Assert
            Assert.Null(prod1Removed);
        }

        [Fact]
        public async Task GetCartItemsAllCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);
            // Act
            var cartItems = await repository.GetCartItemsAllData(cartID);

            // Assert
            Assert.NotNull(cartItems);
            Assert.Equal(2, cartItems.Count);
            Assert.Equal(2, cartItems.Count);
        }

        [Fact]
        public async Task GetCartItemCountCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);
            // Act
            var cartItems = await repository.GetCartItemCount(cartID);

            // Assert
            Assert.NotNull(cartItems);
            Assert.Equal(24, cartItems);
        }

        [Fact]
        public async Task GetCartTotalCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);
            // Act
            var total = await repository.GetCartTotal(cartID);

            // Assert
            Assert.NotNull(total);
            Assert.Equal(240, total);
        }

        [Fact]
        public async Task CalculateOrderTotalCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);
            // Act
            var total = await repository.CalculateOrderTotal(1);

            // Assert
            Assert.NotNull(total);
            Assert.Equal(194.95m, total);
        }

        [Fact]
        public async Task MigrateCartCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);

            var itemsToUpdate = context.Carts.ToList();

            // Act
            await repository.MigrateCart("tgugler", itemsToUpdate);

            var items = context.Carts.ToList();

            // Assert
            Assert.NotNull(items);
            Assert.Equal("tgugler", items[0].CartID);
        }

        [Fact]
        public async Task GetCartItemsAllDataByIDCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);

            // Act
            var item = await repository.GetCartItemsAllDataByID(1);


            // Assert
            Assert.NotNull(item);
        }

        [Fact]
        public async Task RemoveCartItemCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);

            // Act
            var item = await repository.RemoveCartItem(1);


            var itemRemoval = context.Carts.Find(1);

            // Assert
            Assert.Null(itemRemoval);
            Assert.True(item);
        }

        [Fact]
        public async Task EditCartItemCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShoppingCartStore(context);

            var itemEdit = new CartItemEdit { ID = 1, Quantity = 99  };

            // Act
            var item = await repository.EditCartItem(itemEdit);


            var itemCheck = context.Carts.Find(1);

            // Assert
            Assert.NotNull(itemCheck);
            Assert.Equal(99,itemCheck.Quantity);
        }               
    }
}
