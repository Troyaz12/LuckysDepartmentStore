using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.Net;
using LuckysDepartmentStore.Models.ViewModels.Consumer;

namespace XunitTestProject.Stores
{
    public class CustomerStoreTests
    {
        private LuckysContext GetInMemoryDbContext()
        {
            // Create an in-memory database with a unique name per test
            var options = new DbContextOptionsBuilder<LuckysContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new LuckysContext(options);
           
            return context;
        }

        private LuckysContext GetTestSetup()
        {
            var context = GetInMemoryDbContext();
            context.ShippingAddress.Add(new ShippingAddress
            {
                FirstName = "Troy",
                LastName = "Gugler",
                Address1 = "2345 Elm Street",
                Address2 = "Apt.213",
                City = "Richardson",
                State = "TX",
                ZipCode = 12345,
                ShippingAddressID = 1,
                UserId = "1265"
            });
            context.SaveChangesAsync();

            return context;
        }

        [Fact]
        public async Task AddCustomerOrderCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new CustomerStore(context);

            string userID = "1263";
            var customerOrder = new CustomerOrder();
            customerOrder.PaymentID = 1531231;
            customerOrder.ShippingID = 123456;
            customerOrder.UserId = userID;
            // Act
            await repository.SaveOrder(customerOrder);

            var totalOrders = context.CustomerOrders.Count();


            // Assert
            Assert.NotNull(totalOrders);
            Assert.Equal(1, totalOrders);
        }

        [Fact]
        public async Task IsOrderValidCorrect()
        {
            var context = GetTestSetup();
            var repository = new CustomerStore(context);

            // Setup test data
            var testUserName = "testuser@example.com";
            var testUser = new ApplicationUser
            {
                Id = "1263",
                UserName = testUserName
            };
            context.Users.Add(testUser);

            var shippingAddress = new ShippingAddress
            {
                User = testUser,
                Address1 = "1154 Renner Rd",
                City = "Richardson",
                FirstName = "Troy",
                LastName = "Gugler",
                State= "TX"
            };

            var shipping = new Shipping
            {
                User = testUser,
                Address1 = "1154 Renner Rd",
                City = "Richardson",
                FirstName = "Troy",
                LastName = "Gugler",
                state = "TX"
            };

            context.ShippingAddress.Add(shippingAddress);

            var customerOrder = new CustomerOrder
            {
                CustomerOrderID = 1,
                ShippingID = shipping.ShippingID,
                CustomerShippingData = shipping,
                UserId = testUser.Id,
                CreatedDate = DateTime.Now,
                PaymentID = 1531231
            };
            context.CustomerOrders.Add(customerOrder);
            await context.SaveChangesAsync();  // Use async since we're dealing with EF async operations

            // Act
            var result = await repository.IsOrderValid(1, testUserName);  // Pass UserName, not UserId

            // Assert
            Assert.True(result);  // No need to check NotNull since it's a bool return
        }

        [Fact]
        public async Task GetShippingAddressCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new CustomerStore(context);
           
            // Act
            var address = await repository.GetShippingAddress("1265");

            // Assert
            Assert.NotNull(address);
            Assert.Single(address);
        }

        [Fact]
        public async Task CreateShippingAddressCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new CustomerStore(context);

           var shipping = new ShippingAddress
            {
                FirstName = "Troy",
                LastName = "Gugler",
                Address1 = "2345 Elm Street",
                Address2 = "Apt.213",
                City = "Richardson",
                State = "TX",
                ZipCode = 12345,
                ShippingAddressID = 2,
                UserId = "1265"
            };

            // Act
            await repository.CreateCustomerShippingAddress(shipping);

            var count = context.ShippingAddress.Count();

            // Assert
            Assert.NotNull(count);
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task GetShippingAddressByIDCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new CustomerStore(context);

            // Act
            var address = await repository.GetShippingAddressByID(1);

            // Assert
            Assert.NotNull(address);
            Assert.Equal("2345 Elm Street", address.Address1);
        }

        [Fact]
        public async Task DeleteShippingAddressCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new CustomerStore(context);

            var shippingAddress = context.ShippingAddress.Find(1);

            // Act
            await repository.DeleteShippingAddress(shippingAddress);

            var shippingAddressSearch = context.ShippingAddress.Find(1);


            // Assert
            Assert.Null(shippingAddressSearch);
        }

        [Fact]
        public async Task UpdateShippingAddressCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new CustomerStore(context);

            var newShippingAddress = new ShippingAddressVM
            {
                FirstName = "Bob",
                LastName = "Smith",
                Address1 = "2345 Elm Street",
                Address2 = "Apt.213",
                City = "Richardson",
                State = "TX",
                ZipCode = 12345,
                ShippingAddressID = 1,
                UserId = "1265"
            };

            // Act
            var rowsEffected = await repository.UpdateShippingAddress(newShippingAddress);
            var shippingAddressSearch = context.ShippingAddress.FindAsync(1);

            // Assert
            Assert.NotNull(shippingAddressSearch);
            Assert.Equal(1, rowsEffected);
            Assert.Equal("Bob", shippingAddressSearch.Result.FirstName);
            Assert.Equal("Smith", shippingAddressSearch.Result.LastName);
        }
    }
}
