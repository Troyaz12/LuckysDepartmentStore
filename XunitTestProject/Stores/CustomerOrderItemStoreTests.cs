using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XunitTestProject.Stores
{
    public class CustomerOrderItemStoreTests
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

            return context;
        }

        [Fact]
        public async Task AddCustomerOrderCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new CustomerOrderItemsStore(context);

            List<CustomerOrderItem> items = new List<CustomerOrderItem>();

            var item1 = new CustomerOrderItem { CustomerOrderItemID = 1, Price = 5.00m, Quantity = 2, ProductID = 1, CustomerOrderID = 1 };
            var item2 = new CustomerOrderItem { CustomerOrderItemID = 2, Price = 32.00m, Quantity = 3, ProductID = 22, CustomerOrderID = 17 };
            var item3 = new CustomerOrderItem { CustomerOrderItemID = 3, Price = 55.00m, Quantity = 7, ProductID = 11, CustomerOrderID = 16 };
            var item4 = new CustomerOrderItem { CustomerOrderItemID = 4, Price = 15.00m, Quantity = 9, ProductID = 21, CustomerOrderID = 41 };

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);

            // Act
            var rowsEffected = await repository.CustomerOrderItemsAdd(items);

            var allOrderItems = await context.CustomerOrderItems.ToListAsync();
            // Assert
            Assert.NotNull(rowsEffected);
            Assert.Equal(4, rowsEffected);
        }




    }
}
