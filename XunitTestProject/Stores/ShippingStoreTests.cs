using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XunitTestProject.Stores
{
    public class ShippingStoreTests
    {
        private LuckysContext GetInMemoryDbContext()
        {
            // Create an in-memory database with a unique name per test
            var options = new DbContextOptionsBuilder<LuckysContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new LuckysContext(options);
            // Seed initial data if needed
            context.ShippingAddress.Add(new ShippingAddress { ShippingAddressID = 1, FirstName = "Ted", LastName = "Tester", Address1 = "2035 East Main Street", Address2 = null, City = "Richardson", State = "TX", ZipCode = 75082, UserId = "1263" });
            context.ShippingAddress.Add(new ShippingAddress { ShippingAddressID = 2, FirstName = "Ted", LastName = "Tester", Address1 = "1135 South Elm Street", Address2 = null, City = "Dallas", State = "TX", ZipCode = 15646, UserId = "1263" });

            context.SaveChanges();
            return context;
        }

        private LuckysContext GetTestSetup()
        {
            var context = GetInMemoryDbContext();

            return context;
        }

        [Fact]
        public void GetShippingAddressCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShippingStore(context);

            string userID = "1263";

            // Act
            var addresses = repository.GetShippingAddress(userID);

            // Assert
            Assert.NotNull(addresses);
            Assert.Equal(2, addresses.Result.Count);
        }

        [Fact]
        public void AddShippingAddressCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new ShippingStore(context);

            var shipping = new Shipping();
            shipping.FirstName = "Troy";
            shipping.LastName = "Gugler";
            shipping.Address1 = "1145 W Third Street";
            shipping.Address2 = "Apt. 435";
            shipping.City = "Dallas";
            shipping.state = "TX";
            shipping.Zip = 15645;

            // Act
            repository.AddShippingAddress(shipping);

            var totalAddresses = context.Shipping.ToListAsync();

            // Assert
            Assert.NotNull(totalAddresses);
            Assert.Equal(1, totalAddresses.Result.Count);
        }



    }
}
