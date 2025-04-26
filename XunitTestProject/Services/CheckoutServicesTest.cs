using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using LuckysDepartmentStore.Utilities;

namespace XunitTestProject.Services
{
    public class CheckoutServicesTest : IAsyncLifetime
    {
        private CheckoutService _checkoutService;
        private readonly Mock<IShippingStore> _shippingStoreMock;
        private readonly Mock<IPaymentStore> _paymentStoreMock;
        private readonly Mock<ICustomerStore> _customerStoreMock;
        private readonly Mock<ILogger<CheckoutService>> _loggerMock;
        private readonly Mock<IShoppingCartService> _shoppingCartServiceMock;
        private LuckysContext _context;
        private readonly Mock<IDbContextTransaction> _mockTransaction;


        public CheckoutServicesTest()
        {
          
            _shoppingCartServiceMock = new Mock<IShoppingCartService>();
            _shippingStoreMock = new Mock<IShippingStore>();
            _paymentStoreMock = new Mock<IPaymentStore>();
            _customerStoreMock = new Mock<ICustomerStore>();            
            _loggerMock = new Mock<ILogger<CheckoutService>>();            
           
        }

        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<LuckysContext>()
          .UseSqlite("Filename=:memory:")
          .Options;

            _context = new LuckysContext(options);
            await _context.Database.OpenConnectionAsync(); // Required!
            await _context.Database.EnsureCreatedAsync();

            _checkoutService = new CheckoutService(
               _context,
               _shoppingCartServiceMock.Object,
               _shippingStoreMock.Object,
               _paymentStoreMock.Object,
               _customerStoreMock.Object,
               _loggerMock.Object
               );
        }
        public async Task DisposeAsync()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }            
        }

        [Fact]
        public async Task Order_Success_CommitsTransactionAndReturnsOrderIds()
        {
            // Arrange
            var order = new Order { UserId = "user1", AccountNumber = "1234", CvcCode = 123, /* Set other properties */ };
            var orderId = new OrderIds { PaymentID=1, CustomerID=1, CustomerOrderID=1, ShippingID=1 };
            
            var payment = new Payment { PaymentID = 1 };
            var customerOrder = new CustomerOrder { CustomerOrderID = 1 };
            var cart = "dsfa123456";            
            
            _paymentStoreMock.Setup(p => p.SavePayment(It.IsAny<Payment>()))
                .Callback<Payment>(p => p.PaymentID = 1)
                .Returns(Task.CompletedTask);

            _shippingStoreMock.Setup(s => s.AddShippingAddress(It.IsAny<Shipping>()))
                .Callback<Shipping>(p => p.ShippingID = 1)
                .Returns(Task.CompletedTask);

            _customerStoreMock.Setup(c => c.SaveOrder(It.IsAny<CustomerOrder>()))
             .Callback<CustomerOrder>(p => p.CustomerOrderID = 1)
                .Returns(Task.CompletedTask);

            _shoppingCartServiceMock.Setup(c => c.GetCart()).Returns(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(cart));
            _shoppingCartServiceMock.Setup(c => c.CreateOrder(cart, It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<decimal>.Success(100.00m));

            _paymentStoreMock.Setup(p => p.UpdatePayment(It.IsAny<Payment>())).ReturnsAsync(1);

            // Act
            var result = await _checkoutService.Order(order);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(payment.PaymentID, result.Data.PaymentID);
            Assert.Equal(customerOrder.CustomerOrderID, result.Data.CustomerOrderID);
            Assert.Equal(1, result.Data.ShippingID);            
        }

        [Fact]
        public async Task Order_Exception_RollsBackAndLogsError()
        {
            await InitializeAsync(); // Sets up _context, _checkoutService, etc.

            var order = new Order
            {
                UserId = "user1",
                AccountNumber = "1234",
                CvcCode = 123,
                BillingAddress1 = "123 St",
                City = "City",
                state = "ST",
                Zip = 12345,
                FirstNameShipping = "John",
                LastNameShipping = "Doe",
                Address1 = "Shipping St",
                Address2 = "",
            };

            var payment = new Payment { PaymentID = 1 };
            var customerOrder = new CustomerOrder { CustomerOrderID = 1 };
            var cart = "cart123";
            var failedTotal = LuckysDepartmentStore.Utilities.ExecutionResult<decimal>.Failure("Cart is empty");

            _paymentStoreMock.Setup(p => p.SavePayment(It.IsAny<Payment>()));
            _shippingStoreMock.Setup(s => s.AddShippingAddress(It.IsAny<Shipping>()));
            _customerStoreMock.Setup(c => c.SaveOrder(It.IsAny<CustomerOrder>()));
            _shoppingCartServiceMock.Setup(c => c.GetCart()).Returns(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(cart));

            _shoppingCartServiceMock.Setup(c => c.CreateOrder(cart, customerOrder.CustomerOrderID)).ReturnsAsync(failedTotal);

            _paymentStoreMock.Setup(p => p.UpdatePayment(It.IsAny<Payment>())).ReturnsAsync(0);

            // Act
            var result = await _checkoutService.Order(order);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Order failed.", result.ErrorMessage);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to process order")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);

            // Verify nothing was committed (optional: assert db tables are empty or unchanged)
            var payments = await _context.Payments.ToListAsync();
            Assert.Empty(payments); // Assuming SavePayment writes to context
        }

        [Fact]
        public async Task Order_OrderFails_RollsBackAndLogsError()
        {
            await InitializeAsync(); // Sets up _context, _checkoutService, etc.

            var order = new Order
            {
                UserId = "user1",
                AccountNumber = "1234",
                CvcCode = 123,
                BillingAddress1 = "123 St",
                City = "City",
                state = "ST",
                Zip = 12345,
                FirstNameShipping = "John",
                LastNameShipping = "Doe",
                Address1 = "Shipping St",
                Address2 = "",
            };

            var payment = new Payment { PaymentID = 1 };
            var customerOrder = new CustomerOrder { CustomerOrderID = 1 };
            var cart = "cart123";

            _paymentStoreMock.Setup(p => p.SavePayment(It.IsAny<Payment>()));
            _shippingStoreMock.Setup(s => s.AddShippingAddress(It.IsAny<Shipping>()));
            _customerStoreMock.Setup(c => c.SaveOrder(It.IsAny<CustomerOrder>()));

            _shoppingCartServiceMock.Setup(c => c.GetCart()).Returns(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(cart));
            _shoppingCartServiceMock.Setup(c => c.CreateOrder(cart, It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<decimal>.Failure("Mocked failure"));

            _paymentStoreMock.Setup(p => p.UpdatePayment(It.IsAny<Payment>())).ReturnsAsync(0);

            // Act
            var result = await _checkoutService.Order(order);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to calculate order total: Mocked failure", result.ErrorMessage);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to process order")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);

            // Verify nothing was committed (optional: assert db tables are empty or unchanged)
            var payments = await _context.Payments.ToListAsync();
            Assert.Empty(payments); // Assuming SavePayment writes to context
        }

        [Fact]
        public async Task Order_PaymentFails_RollsBackAndLogsError()
        {
            await InitializeAsync(); // Sets up _context, _checkoutService, etc.

            var order = new Order
            {
                UserId = "user1",
                AccountNumber = "1234",
                CvcCode = 123,
                BillingAddress1 = "123 St",
                City = "City",
                state = "ST",
                Zip = 12345,
                FirstNameShipping = "John",
                LastNameShipping = "Doe",
                Address1 = "Shipping St",
                Address2 = "",
            };

            var payment = new Payment { PaymentID = 1 };
            var customerOrder = new CustomerOrder { CustomerOrderID = 1 };
            var cart = "cart123";

            _paymentStoreMock.Setup(p => p.SavePayment(It.IsAny<Payment>()));

            _shippingStoreMock.Setup(s => s.AddShippingAddress(It.IsAny<Shipping>()));
            _customerStoreMock.Setup(c => c.SaveOrder(It.IsAny<CustomerOrder>()));

            _shoppingCartServiceMock.Setup(c => c.GetCart()).Returns(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(cart));
            _shoppingCartServiceMock.Setup(c => c.CreateOrder(cart, It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<decimal>.Success(100.00m));

            _paymentStoreMock.Setup(p => p.UpdatePayment(It.IsAny<Payment>())).ReturnsAsync(0);

            // Act
            var result = await _checkoutService.Order(order);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to update payment total.", result.ErrorMessage);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed update payment for order")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);

            // Verify nothing was committed (optional: assert db tables are empty or unchanged)
            var payments = await _context.Payments.ToListAsync();
            Assert.Empty(payments); // Assuming SavePayment writes to context
        }

        [Fact]
        public async Task Order_DBException_RollsBackAndLogsError()
        {
            await InitializeAsync(); // Sets up _context, _checkoutService, etc.

            var order = new Order
            {
                UserId = "user1",
                AccountNumber = "1234",
                CvcCode = 123,
                BillingAddress1 = "123 St",
                City = "City",
                state = "ST",
                Zip = 12345,
                FirstNameShipping = "John",
                LastNameShipping = "Doe",
                Address1 = "Shipping St",
                Address2 = "",
            };

            var exception = new DbUpdateException("Database Error" + new Exception());
            var payment = new Payment { PaymentID = 1 };
            var customerOrder = new CustomerOrder { CustomerOrderID = 1 };
            var cart = "cart123";
        
            _paymentStoreMock.Setup(p => p.SavePayment(It.IsAny<Payment>()));
            _paymentStoreMock.Setup(p => p.UpdatePayment(It.IsAny<Payment>()))
                .ThrowsAsync(exception);

            _shippingStoreMock.Setup(s => s.AddShippingAddress(It.IsAny<Shipping>()));
            _customerStoreMock.Setup(c => c.SaveOrder(It.IsAny<CustomerOrder>()));

            _shoppingCartServiceMock.Setup(c => c.GetCart()).Returns(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(cart));
            _shoppingCartServiceMock.Setup(c => c.CreateOrder(cart, It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<decimal>.Success(100.00m));

            // Act
            var result = await _checkoutService.Order(order);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to create order.", result.ErrorMessage);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to save order to the database")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);

            // Verify nothing was committed (optional: assert db tables are empty or unchanged)
            var payments = await _context.Payments.ToListAsync();
            Assert.Empty(payments); // Assuming SavePayment writes to context
        }

        [Fact]
        public async Task IsValid_Success_ReturnsIfOrderIsValid()
        {
          _customerStoreMock.Setup(c => c.IsOrderValid(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);
         
            // Act
            var result = await _checkoutService.IsValid(1,"test");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Data);        
        }

        [Fact]
        public async Task IsValid_Fail_ReturnsIfOrderIsValid()
        {
            _customerStoreMock.Setup(c => c.IsOrderValid(It.IsAny<int>(), It.IsAny<string>()))
                  .ReturnsAsync(false);

            // Act
            var result = await _checkoutService.IsValid(1, "test");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.Data);
        }

        [Fact]
        public async Task IsValid_Exception_ReturnsError()
        {
            var exception = new DbUpdateException("Database Error" + new Exception());

            _customerStoreMock.Setup(c => c.IsOrderValid(It.IsAny<int>(), It.IsAny<string>()))
                  .ThrowsAsync(exception);

            // Act
            var result = await _checkoutService.IsValid(1, "test");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to check validation.", result.ErrorMessage);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to validate order ")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
