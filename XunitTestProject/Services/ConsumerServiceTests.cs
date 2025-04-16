using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Service;
using Microsoft.Extensions.Logging;
using Moq;
using AutoMapper;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using LuckysDepartmentStore.Models;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using LuckysDepartmentStore.Models.DTO.Consumer;
using LuckysDepartmentStore.Utilities;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System;

namespace XunitTestProject.Services
{
    public class ConsumerServiceTests
    {
        private readonly Mock<ICustomerStore> _customerStoreMock;
        private readonly Mock<IPaymentStore> _paymentStoreMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<IConsumerService>> _loggerMock;
        private readonly ConsumerService _consumerService;

        public ConsumerServiceTests()
        {
            _customerStoreMock = new Mock<ICustomerStore>();
            _paymentStoreMock = new Mock<IPaymentStore>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<IConsumerService>>();
            _consumerService = new ConsumerService(_mapperMock.Object, _customerStoreMock.Object, _paymentStoreMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_ShippingAddress_Success()
        {
            ShippingAddress shippingAddress = new ShippingAddress();
            
            ShippingAddressVM shippingAddressVM = new ShippingAddressVM
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Address1 = "add1",
                Address2 = "add2",
                City = "city",
                State = "TX",
                ZipCode = 12345,
                UserId = "UserId"

            };
            
            _customerStoreMock.Setup(x => x.CreateCustomerShippingAddress(shippingAddress));

            var wasCreated = await _consumerService.CreateShippingAddress(shippingAddressVM);

            Assert.True(wasCreated.IsSuccess);
            Assert.True(wasCreated.Data);
        }

        [Fact]
        public async Task Create_ShippingAddressNull_Fail()
        {
            ShippingAddress shippingAddress = new ShippingAddress();

            ShippingAddressVM shippingAddressVM = null;

            _customerStoreMock.Setup(x => x.CreateCustomerShippingAddress(shippingAddress));

            var wasCreated = await _consumerService.CreateShippingAddress(shippingAddressVM);

            Assert.True(!wasCreated.IsSuccess);
            Assert.Equal("Did not receive an address.", wasCreated.ErrorMessage);
            
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Received null shipping address.")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Create_ShippingAddressDbException_Fail()
        {
            ShippingAddress shippingAddress = new ShippingAddress();
            ShippingAddressVM shippingAddressVM = new ShippingAddressVM
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Address1 = "add1",
                Address2 = "add2",
                City = "city",
                State = "TX",
                ZipCode = 12345,
                UserId = "UserId"

            };

            var exception = new DbUpdateException("Database error", new Exception());

            _customerStoreMock
                 .Setup(x => x.CreateCustomerShippingAddress(It.Is<ShippingAddress>(sa =>
                    sa.FirstName == shippingAddressVM.FirstName &&
                    sa.LastName == shippingAddressVM.LastName &&
                    sa.Address1 == shippingAddressVM.Address1 &&
                    sa.Address2 == shippingAddressVM.Address2 &&
                    sa.City == shippingAddressVM.City &&
                    sa.State == shippingAddressVM.State &&
                    sa.ZipCode == shippingAddressVM.ZipCode &&
                    sa.UserId == shippingAddressVM.UserId)))
                .ThrowsAsync(exception);

            var wasCreated = await _consumerService.CreateShippingAddress(shippingAddressVM);

            Assert.False(wasCreated.IsSuccess);
            Assert.Equal("Unable to save address to database.", wasCreated.ErrorMessage);
            _customerStoreMock.Verify(x => x.CreateCustomerShippingAddress(It.IsAny<ShippingAddress>()), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create shipping address for")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Create_ShippingAddressException_Fail()
        {
            ShippingAddress shippingAddress = new ShippingAddress();
            ShippingAddressVM shippingAddressVM = new ShippingAddressVM
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Address1 = "add1",
                Address2 = "add2",
                City = "city",
                State = "TX",
                ZipCode = 12345,
                UserId = "UserId"

            };

            var exception = new Exception("Database error", new Exception());

            _customerStoreMock
                 .Setup(x => x.CreateCustomerShippingAddress(It.Is<ShippingAddress>(sa =>
                    sa.FirstName == shippingAddressVM.FirstName &&
                    sa.LastName == shippingAddressVM.LastName &&
                    sa.Address1 == shippingAddressVM.Address1 &&
                    sa.Address2 == shippingAddressVM.Address2 &&
                    sa.City == shippingAddressVM.City &&
                    sa.State == shippingAddressVM.State &&
                    sa.ZipCode == shippingAddressVM.ZipCode &&
                    sa.UserId == shippingAddressVM.UserId)))
                .ThrowsAsync(exception);

            var wasCreated = await _consumerService.CreateShippingAddress(shippingAddressVM);

            Assert.False(wasCreated.IsSuccess);
            Assert.Equal("Unable to save address.", wasCreated.ErrorMessage);
            _customerStoreMock.Verify(x => x.CreateCustomerShippingAddress(It.IsAny<ShippingAddress>()), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create shipping address for")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Get_ShippingAddress_SuccessEmptyList()
        {
            string userId = "userTest";

            _customerStoreMock
                 .Setup(x => x.GetShippingAddress(It.IsAny<string>()))
                 .ReturnsAsync(new List<ShippingAddressDTO>());

            var result = await _consumerService.GetShippingAddress(userId);

            Assert.True(result.IsSuccess);
            _customerStoreMock.Verify(x => x.GetShippingAddress(userId), Times.Once());
        }

        [Fact]
        public async Task GetShippingAddress_HasAddresses_ReturnsMappedList()
        {
            // Arrange
            string userId = "test-user";
            var shippingAddresses = new List<ShippingAddressDTO>
            {
                new ShippingAddressDTO
                {
                    ShippingAddressID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Address1 = "123 Main St",
                    City = "City",
                    State = "TX",
                    ZipCode = 12345,
                    UserId = userId
                }
            };
            _customerStoreMock
                .Setup(x => x.GetShippingAddress(userId))
                .ReturnsAsync(shippingAddresses);

            var expectedVMs = new List<ShippingAddressVM>
            {
                new ShippingAddressVM
                {
                    ShippingAddressID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Address1 = "123 Main St",
                    City = "City",
                    State = "TX",
                    ZipCode = 12345, // Adjust if string
                    UserId = userId
                }
            };
            _mapperMock
                .Setup(x => x.Map<List<ShippingAddressVM>>(shippingAddresses))
                .Returns(expectedVMs);

            // Act
            var result = await _consumerService.GetShippingAddress(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal(expectedVMs[0].FirstName, result.Data[0].FirstName);
            Assert.Equal(expectedVMs[0].LastName, result.Data[0].LastName);
            // Add more assertions as needed
            _customerStoreMock.Verify(x => x.GetShippingAddress(userId), Times.Once());
            _mapperMock.Verify(x => x.Map<List<ShippingAddressVM>>(shippingAddresses), Times.Once());
        }

        [Fact]
        public async Task Get_ShippingAddressNoUserID_Fail()
        {
            string userId = null;

            _customerStoreMock
                 .Setup(x => x.GetShippingAddress(It.IsAny<string>()))
                 .ReturnsAsync(new List<ShippingAddressDTO>());

            var result = await _consumerService.GetShippingAddress(It.IsAny<string>());

            Assert.False(result.IsSuccess);
            Assert.Equal("UserId not recieved.", result.ErrorMessage);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("No UserID provided.")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Get_ShippingAddressException_Fail()
        {
            string userId = "testUser";
            var exception = new Exception("Database error", new Exception());

            _customerStoreMock
                 .Setup(x => x.GetShippingAddress(It.IsAny<string>()))
                 .ThrowsAsync(exception);

            var result = await _consumerService.GetShippingAddress(userId);

            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to get address.", result.ErrorMessage);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to get shipping address for")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Create_Payment_Success()
        {
            PaymentOptions paymentOption = new PaymentOptions();

            PaymentOptionsVM paymentOptionVM = new PaymentOptionsVM
            {
                FirstName = "FirstName",
                LastName = "LastName",
                BillingAddress1 = "add1",
                BillingAddress2 = "add2",
                City = "city",
                State = "TX",
                ZipCode = 12345,
                UserId = "UserId"

            };

            var expectedVMs = new List<PaymentOptions>
            {
                new PaymentOptions
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    BillingAddress1 = "add1",
                    BillingAddress2 = "add2",
                    City = "city",
                    State = "TX",
                    ZipCode = 12345,
                    UserId = "UserId"
                }
            };

            _mapperMock
               .Setup(x => x.Map<List<PaymentOptions>>(paymentOptionVM))
               .Returns(expectedVMs);


            _paymentStoreMock.Setup(x => x.CreatePaymentOption(It.IsAny<PaymentOptions>()));

            var wasCreated = await _consumerService.CreatePaymentOption(paymentOptionVM);

            Assert.True(wasCreated.IsSuccess);
            Assert.True(wasCreated.Data);
        }

        [Fact]
        public async Task Create_PaymentOptionNull_Fail()
        {
            PaymentOptionsVM paymentOptionVM = null;

            _paymentStoreMock.Setup(x => x.CreatePaymentOption(It.IsAny<PaymentOptions>()));

            var wasCreated = await _consumerService.CreatePaymentOption(paymentOptionVM);

            Assert.False(wasCreated.IsSuccess);
            Assert.Equal("Payment data not recieved.", wasCreated.ErrorMessage);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("No payment option provided.")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Create_PaymentDbException_Fail()
        {
            var exception = new DbUpdateException("Database error", new Exception());
            PaymentOptions paymentOption = new PaymentOptions();

            PaymentOptionsVM paymentOptionVM = new PaymentOptionsVM
            {
                FirstName = "FirstName",
                LastName = "LastName",
                BillingAddress1 = "add1",
                BillingAddress2 = "add2",
                City = "city",
                State = "TX",
                ZipCode = 12345,
                UserId = "UserId"

            };

            var expectedVMs = new List<PaymentOptions>
            {
                new PaymentOptions
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    BillingAddress1 = "add1",
                    BillingAddress2 = "add2",
                    City = "city",
                    State = "TX",
                    ZipCode = 12345,
                    UserId = "UserId"
                }
            };

            _mapperMock
               .Setup(x => x.Map<List<PaymentOptions>>(paymentOptionVM))
               .Returns(expectedVMs);

            _paymentStoreMock.Setup(x => x.CreatePaymentOption(It.IsAny<PaymentOptions>()))
                .ThrowsAsync(exception);
            

            var wasCreated = await _consumerService.CreatePaymentOption(paymentOptionVM);

            Assert.False(wasCreated.IsSuccess);
            Assert.Equal("Unable to add payment to database.", wasCreated.ErrorMessage);
            _paymentStoreMock.Verify(x => x.CreatePaymentOption(It.IsAny<PaymentOptions>()), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create payment option")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Create_PaymentException_Fail()
        {
            var exception = new Exception("Database error", new Exception());
            PaymentOptions paymentOption = new PaymentOptions();

            PaymentOptionsVM paymentOptionVM = new PaymentOptionsVM
            {
                FirstName = "FirstName",
                LastName = "LastName",
                BillingAddress1 = "add1",
                BillingAddress2 = "add2",
                City = "city",
                State = "TX",
                ZipCode = 12345,
                UserId = "UserId"

            };

            var expectedVMs = new List<PaymentOptions>
            {
                new PaymentOptions
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    BillingAddress1 = "add1",
                    BillingAddress2 = "add2",
                    City = "city",
                    State = "TX",
                    ZipCode = 12345,
                    UserId = "UserId"
                }
            };

            _mapperMock
               .Setup(x => x.Map<List<PaymentOptions>>(paymentOptionVM))
               .Returns(expectedVMs);

            _paymentStoreMock.Setup(x => x.CreatePaymentOption(It.IsAny<PaymentOptions>()))
                .ThrowsAsync(exception);


            var wasCreated = await _consumerService.CreatePaymentOption(paymentOptionVM);

            Assert.False(wasCreated.IsSuccess);
            Assert.Equal("Unable to add payment data.", wasCreated.ErrorMessage);
            _paymentStoreMock.Verify(x => x.CreatePaymentOption(It.IsAny<PaymentOptions>()), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create payment option")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Get_PaymentOptionsNullUserId_LogsErrorAndReturnsFailure()
        {
            // Arrange
            string userId = null;

            // Act
            var result = await _consumerService.GetPaymentOptions(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("UserId not recieved.", result.ErrorMessage);
            _paymentStoreMock.Verify(
                x => x.GetPaymentOptions(It.IsAny<string>()),
                Times.Never(),
                "GetPaymentOptions should not be called for null userId.");
            _mapperMock.Verify(
                x => x.Map<List<PaymentOptionsVM>>(It.IsAny<object>()),
                Times.Never(),
                "Mapper should not be called for null userId.");
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to get payment options")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once(),
                "Logger should log an error for null userId.");
        }

        [Fact]
        public async Task Get_PaymentOptionsValidUserIdEmptyList_ReturnsSuccessWithEmptyList()
        {
            // Arrange
            string userId = "test-user";
            _paymentStoreMock
                .Setup(x => x.GetPaymentOptions(userId))
                .ReturnsAsync(new List<PaymentOptionsDTO>());

            _mapperMock
                .Setup(x => x.Map<List<PaymentOptionsVM>>(It.IsAny<List<PaymentOptionsDTO>>()))
                .Returns((List<PaymentOptionsDTO> input) => new List<PaymentOptionsVM>());

            // Act
            var result = await _consumerService.GetPaymentOptions(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
            _paymentStoreMock.Verify(x => x.GetPaymentOptions(userId), Times.Once());
            _mapperMock.Verify(x => x.Map<List<PaymentOptionsVM>>(It.IsAny<List<PaymentOptionsDTO>>()), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never(),
                "Logger should not log errors for successful case.");
        }

        [Fact]
        public async Task Get_PaymentOptionsValidUserIdNullList_ReturnsSuccessWithNull()
        {
            // Arrange
            string userId = "test-user";
            _paymentStoreMock
                .Setup(x => x.GetPaymentOptions(userId))
                .ReturnsAsync((List<PaymentOptionsDTO>)null);

            _mapperMock
                .Setup(x => x.Map<List<PaymentOptionsVM>>(It.IsAny<List<PaymentOptionsDTO>>()))
                .Returns((List<PaymentOptionsDTO> input) => input == null ? null : new List<PaymentOptionsVM>());

            // Act
            var result = await _consumerService.GetPaymentOptions(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Data);
            _paymentStoreMock.Verify(x => x.GetPaymentOptions(userId), Times.Once());
            _mapperMock.Verify(x => x.Map<List<PaymentOptionsVM>>(null), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never());
        }

        [Fact]
        public async Task GetPaymentOptions_ValidUserIdWithPayments_ReturnsSuccessWithMappedList()
        {
            // Arrange
            string userId = "test-user";
            var paymentOptions = new List<PaymentOptionsDTO>
            {
                new PaymentOptionsDTO { PaymentOptionsID = 1, CvcCode = 123, AccountNumber = "123456456" },
                new PaymentOptionsDTO { PaymentOptionsID = 2, CvcCode = 111, AccountNumber = "144446456"}
            };
            _paymentStoreMock
                .Setup(x => x.GetPaymentOptions(userId))
                .ReturnsAsync(paymentOptions);

            var expectedVMs = new List<PaymentOptionsVM>
            {
                new PaymentOptionsVM { PaymentOptionsID = 1, CvcCode = 123, AccountNumber = "123456456" },
                new PaymentOptionsVM { PaymentOptionsID = 2, CvcCode = 111, AccountNumber = "144446456"}
            };
            _mapperMock
                .Setup(x => x.Map<List<PaymentOptionsVM>>(paymentOptions))
                .Returns(expectedVMs);

            // Act
            var result = await _consumerService.GetPaymentOptions(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            Assert.Contains(result.Data, vm => vm.PaymentOptionsID == 1 && vm.CvcCode == 123);
            Assert.Contains(result.Data, vm => vm.PaymentOptionsID == 2 && vm.CvcCode == 111);
            _paymentStoreMock.Verify(x => x.GetPaymentOptions(userId), Times.Once());
            _mapperMock.Verify(x => x.Map<List<PaymentOptionsVM>>(paymentOptions), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never());
        }

        [Fact]
        public async Task Get_PaymentOptionsStoreThrowsException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            string userId = "test-user";
            var exception = new Exception("Payment store error");
            _paymentStoreMock
                .Setup(x => x.GetPaymentOptions(userId))
                .ThrowsAsync(exception);

            // Act
            var result = await _consumerService.GetPaymentOptions(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to get address.", result.ErrorMessage);
            _paymentStoreMock.Verify(x => x.GetPaymentOptions(userId), Times.Once());
            _mapperMock.Verify(
                x => x.Map<List<PaymentOptionsVM>>(It.IsAny<object>()),
                Times.Never());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to get payment options")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once(),
                "Logger should log an error for store exception.");
        }

        [Fact]
        public async Task Delete_ShippingRecordNullId_ReturnsFailure()
        {
            // Arrange
            int id = 0;

            // Act
            var result = await _consumerService.DeleteShippingRecord(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Shipping data not recieved.", result.ErrorMessage);
            _customerStoreMock.Verify(
                x => x.GetShippingAddressByID(It.IsAny<int>()),
                Times.Never(),
                "GetShippingAddressByID should not be called for null ID.");
            _customerStoreMock.Verify(
                x => x.DeleteShippingAddress(It.IsAny<ShippingAddress>()),
                Times.Never(),
                "DeleteShippingAddress should not be called for null ID.");            
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Shipping data not recieved.")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Delete_ShippingRecordNonExistentId_ReturnsFailure()
        {
            // Arrange
            int id = 999;
            _customerStoreMock
                .Setup(x => x.GetShippingAddressByID(id))
                .ReturnsAsync((ShippingAddress)null);

            // Act
            var result = await _consumerService.DeleteShippingRecord(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("An error occured. Cannot find delete product.", result.ErrorMessage);
            _customerStoreMock.Verify(x => x.GetShippingAddressByID(id), Times.Once());
            _customerStoreMock.Verify(
                x => x.DeleteShippingAddress(It.IsAny<ShippingAddress>()),
                Times.Never(),
                "DeleteShippingAddress should not be called for non-existent ID.");
            _loggerMock.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Shipping data not found.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task Delete_ShippingRecordValidId_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            var shippingAddress = new ShippingAddress
            {
                ShippingAddressID = id,
                FirstName = "John",
                LastName = "Doe",
                Address1 = "123 Main St",
                City = "City",
                State = "TX",
                ZipCode = 12345,
                UserId = "user1"
            };
            _customerStoreMock
                .Setup(x => x.GetShippingAddressByID(id))
                .ReturnsAsync(shippingAddress);
            _customerStoreMock
                .Setup(x => x.DeleteShippingAddress(shippingAddress))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _consumerService.DeleteShippingRecord(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(id, result.Data.ShippingAddressID);
            Assert.Equal("John", result.Data.FirstName);
            _customerStoreMock.Verify(x => x.GetShippingAddressByID(id), Times.Once());
            _customerStoreMock.Verify(x => x.DeleteShippingAddress(shippingAddress), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never());
        }

        [Fact]
        public async Task Delete_ShippingRecordDbUpdateException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            int id = 1;
            var shippingAddress = new ShippingAddress { ShippingAddressID = id, FirstName = "John" };
            var exception = new DbUpdateException("Database error", new Exception());
            _customerStoreMock
                .Setup(x => x.GetShippingAddressByID(id))
                .ReturnsAsync(shippingAddress);
            _customerStoreMock
                .Setup(x => x.DeleteShippingAddress(shippingAddress))
                .ThrowsAsync(exception);

            // Act
            var result = await _consumerService.DeleteShippingRecord(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to delete shipping record in database.", result.ErrorMessage);
            _customerStoreMock.Verify(x => x.GetShippingAddressByID(id), Times.Once());
            _customerStoreMock.Verify(x => x.DeleteShippingAddress(shippingAddress), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to delete shipping record")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task Delete_ShippingRecordGeneralException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            int id = 1;
            var exception = new Exception("Unexpected error");
            _customerStoreMock
                .Setup(x => x.GetShippingAddressByID(id))
                .ThrowsAsync(exception);

            // Act
            var result = await _consumerService.DeleteShippingRecord(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to delete shipping record.", result.ErrorMessage);
            _customerStoreMock.Verify(x => x.GetShippingAddressByID(id), Times.Once());
            _customerStoreMock.Verify(
                x => x.DeleteShippingAddress(It.IsAny<ShippingAddress>()),
                Times.Never());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to delete shipping record")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task EditShippingRecord_NullShippingAddress_ReturnsFailure()
        {
            // Arrange
            ShippingAddressVM shippingAddress = null;

            // Act
            var result = await _consumerService.EditShippingRecord(shippingAddress);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No shipping address selected.", result.ErrorMessage);
            _customerStoreMock.Verify(
                x => x.UpdateShippingAddress(It.IsAny<ShippingAddressVM>()),
                Times.Never(),
                "UpdateShippingAddress should not be called for null shipping address.");
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Shipping data not recieved.")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task EditShippingRecord_NoRowsAffected_ReturnsFailure()
        {
            // Arrange
            var shippingAddress = new ShippingAddressVM
            {
                ShippingAddressID = 999,
                FirstName = "John",
                LastName = "Doe",
                Address1 = "123 Main St",
                City = "City",
                State = "TX",
                ZipCode = 12345,
                UserId = "user1"
            };
            _customerStoreMock
                .Setup(x => x.UpdateShippingAddress(shippingAddress))
                .ReturnsAsync(0);

            // Act
            var result = await _consumerService.EditShippingRecord(shippingAddress);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Shipping address could not be updated.", result.ErrorMessage);
            _customerStoreMock.Verify(x => x.UpdateShippingAddress(shippingAddress), Times.Once());
            _loggerMock.Verify(
               x => x.Log(
                   LogLevel.Warning,
                   It.IsAny<EventId>(),
                   It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("No row found to be updated.")),
                   null,
                   It.IsAny<Func<It.IsAnyType, Exception, string>>()),
               Times.Once());
        }

        [Fact]
        public async Task EditShippingRecord_SuccessfulUpdate_ReturnsSuccess()
        {
            // Arrange
            var shippingAddress = new ShippingAddressVM
            {
                ShippingAddressID = 1,
                FirstName = "John",
                LastName = "Doe",
                Address1 = "123 Main St",
                City = "City",
                State = "TX",
                ZipCode = 12345,
                UserId = "user1"
            };
            _customerStoreMock
                .Setup(x => x.UpdateShippingAddress(shippingAddress))
                .ReturnsAsync(1);

            // Act
            var result = await _consumerService.EditShippingRecord(shippingAddress);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(shippingAddress.ShippingAddressID, result.Data.ShippingAddressID);
            Assert.Equal(shippingAddress.FirstName, result.Data.FirstName);
            Assert.Equal(shippingAddress.UserId, result.Data.UserId);
            _customerStoreMock.Verify(x => x.UpdateShippingAddress(shippingAddress), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never());
        }

        [Fact]
        public async Task EditShippingRecord_DbUpdateException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            var shippingAddress = new ShippingAddressVM
            {
                ShippingAddressID = 1,
                FirstName = "John",
                LastName = "Doe",
                Address1 = "123 Main St",
                City = "City",
                State = "TX",
                ZipCode = 12345,
                UserId = "user1"
            };
            var exception = new DbUpdateException("Database error", new Exception());
            _customerStoreMock
                .Setup(x => x.UpdateShippingAddress(shippingAddress))
                .ThrowsAsync(exception);

            // Act
            var result = await _consumerService.EditShippingRecord(shippingAddress);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to edit shipping record in database.", result.ErrorMessage);
            _customerStoreMock.Verify(x => x.UpdateShippingAddress(shippingAddress), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to edit shipping records")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task EditShippingRecord_GeneralException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            var shippingAddress = new ShippingAddressVM
            {
                ShippingAddressID = 1,
                FirstName = "John",
                LastName = "Doe",
                Address1 = "123 Main St",
                City = "City",
                State = "TX",
                ZipCode = 12345,
                UserId = "user1"
            };
            var exception = new Exception("Unexpected error");
            _customerStoreMock
                .Setup(x => x.UpdateShippingAddress(shippingAddress))
                .ThrowsAsync(exception);

            // Act
            var result = await _consumerService.EditShippingRecord(shippingAddress);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to edit shipping record.", result.ErrorMessage);
            _customerStoreMock.Verify(x => x.UpdateShippingAddress(shippingAddress), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to edit shipping records")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task EditPaymentRecord_NullPaymentOptions_ReturnsFailure()
        {
            // Arrange
            PaymentOptionsVM paymentOptions = null;

            // Act
            var result = await _consumerService.EditPaymentRecord(paymentOptions);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No payment option selected.", result.ErrorMessage);
            _paymentStoreMock.Verify(
                x => x.EditPaymentOption(It.IsAny<PaymentOptionsVM>()),
                Times.Never(),
                "EditPaymentOption should not be called for null payment options.");
            _loggerMock.Verify(
              x => x.Log(
                  LogLevel.Warning,
                  It.IsAny<EventId>(),
                  It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Payment data not recieved.")),
                  null,
                  It.IsAny<Func<It.IsAnyType, Exception, string>>()),
              Times.Once());
        }

        [Fact]
        public async Task EditPaymentRecord_NoRowsAffected_ReturnsFailure()
        {
            // Arrange
            var paymentOptions = new PaymentOptionsVM
            {
                PaymentOptionsID = 999,
                AccountNumber = "1234",
                UserId = "user1"
            };
            _paymentStoreMock
                .Setup(x => x.EditPaymentOption(paymentOptions))
                .ReturnsAsync(0);

            // Act
            var result = await _consumerService.EditPaymentRecord(paymentOptions);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Could not update payment option.", result.ErrorMessage);
            _paymentStoreMock.Verify(x => x.EditPaymentOption(paymentOptions), Times.Once());
            _loggerMock.Verify(
             x => x.Log(
                 LogLevel.Warning,
                 It.IsAny<EventId>(),
                 It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Payment option not found.")),
                 null,
                 It.IsAny<Func<It.IsAnyType, Exception, string>>()),
             Times.Once());
        }


        [Fact]
        public async Task EditPaymentRecord_SuccessfulUpdate_ReturnsSuccess()
        {
            // Arrange
            var paymentOptions = new PaymentOptionsVM
            {
                PaymentOptionsID = 1,
                AccountNumber = "1234",
                UserId = "user1"
            };
            _paymentStoreMock
                .Setup(x => x.EditPaymentOption(paymentOptions))
                .ReturnsAsync(1);

            // Act
            var result = await _consumerService.EditPaymentRecord(paymentOptions);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(paymentOptions.PaymentOptionsID, result.Data.PaymentOptionsID);
            Assert.Equal(paymentOptions.AccountNumber, result.Data.AccountNumber);
            Assert.Equal(paymentOptions.UserId, result.Data.UserId);
            _paymentStoreMock.Verify(x => x.EditPaymentOption(paymentOptions), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never());
        }


        [Fact]
        public async Task EditPaymentRecord_DbUpdateException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            var paymentOptions = new PaymentOptionsVM
            {
                PaymentOptionsID = 1,
                AccountNumber = "1234",
                UserId = "user1"
            };
            var exception = new DbUpdateException("Database error", new Exception());
            _paymentStoreMock
                .Setup(x => x.EditPaymentOption(paymentOptions))
                .ThrowsAsync(exception);

            // Act
            var result = await _consumerService.EditPaymentRecord(paymentOptions);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to edit payment option in database.", result.ErrorMessage);
            _paymentStoreMock.Verify(x => x.EditPaymentOption(paymentOptions), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to edit payment records")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task EditPaymentRecord_GeneralException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            var paymentOptions = new PaymentOptionsVM
            {
                PaymentOptionsID = 1,
                AccountNumber = "1234",
                UserId = "user1"
            };
            var exception = new Exception("Unexpected error");
            _paymentStoreMock
                .Setup(x => x.EditPaymentOption(paymentOptions))
                .ThrowsAsync(exception);

            // Act
            var result = await _consumerService.EditPaymentRecord(paymentOptions);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to edit payment option.", result.ErrorMessage);
            _paymentStoreMock.Verify(x => x.EditPaymentOption(paymentOptions), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to edit payment records")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }






        [Fact]
        public async Task DeletePaymentRecord_InvalidId_LogsWarningAndReturnsFailure()
        {
            // Arrange
            int id = 0;

            // Act
            var result = await _consumerService.DeletePaymentRecord(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid payment ID.", result.ErrorMessage);
            _paymentStoreMock.Verify(
                x => x.GetPaymentOptionByID(It.IsAny<int>()),
                Times.Never(),
                "GetPaymentOptionByID should not be called for invalid ID.");
            _paymentStoreMock.Verify(
                x => x.DeletePaymentOption(It.IsAny<PaymentOptions>()),
                Times.Never(),
                "DeletePaymentOption should not be called for invalid ID.");
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Invalid payment ID")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once(),
                "Logger should log a warning for invalid ID.");
        }

        [Fact]
        public async Task DeletePaymentRecord_NonExistentId_LogsWarningAndReturnsFailure()
        {
            // Arrange
            int id = 999;
            _paymentStoreMock
                .Setup(x => x.GetPaymentOptionByID(id))
                .ReturnsAsync((PaymentOptions)null);

            // Act
            var result = await _consumerService.DeletePaymentRecord(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("An error occured. Cannot find delete payment.", result.ErrorMessage);
            _paymentStoreMock.Verify(x => x.GetPaymentOptionByID(id), Times.Once());
            _paymentStoreMock.Verify(
                x => x.DeletePaymentOption(It.IsAny<PaymentOptions>()),
                Times.Never(),
                "DeletePaymentOption should not be called for non-existent ID.");
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Could not find payment")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task DeletePaymentRecord_ValidId_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            var paymentRecord = new PaymentOptions
            {
                PaymentOptionsID = id,
                AccountNumber = "1234",
                UserId = "user1"
            };
            _paymentStoreMock
                .Setup(x => x.GetPaymentOptionByID(id))
                .ReturnsAsync(paymentRecord);
            _paymentStoreMock
                .Setup(x => x.DeletePaymentOption(paymentRecord))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _consumerService.DeletePaymentRecord(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(id, result.Data.PaymentOptionsID);
            Assert.Equal("1234", result.Data.AccountNumber);
            _paymentStoreMock.Verify(x => x.GetPaymentOptionByID(id), Times.Once());
            _paymentStoreMock.Verify(x => x.DeletePaymentOption(paymentRecord), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never(),
                "Logger should not be called for successful deletion.");
        }

        [Fact]
        public async Task DeletePaymentRecord_DbUpdateException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            int id = 1;
            var paymentRecord = new PaymentOptions { PaymentOptionsID = id, AccountNumber = "1234" };
            var exception = new DbUpdateException("Database error", new Exception());
            _paymentStoreMock
                .Setup(x => x.GetPaymentOptionByID(id))
                .ReturnsAsync(paymentRecord);
            _paymentStoreMock
                .Setup(x => x.DeletePaymentOption(paymentRecord))
                .ThrowsAsync(exception);

            // Act
            var result = await _consumerService.DeletePaymentRecord(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to delete payment in database.", result.ErrorMessage);
            _paymentStoreMock.Verify(x => x.GetPaymentOptionByID(id), Times.Once());
            _paymentStoreMock.Verify(x => x.DeletePaymentOption(paymentRecord), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to delete payment record")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task DeletePaymentRecord_GeneralException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            int id = 1;
            var exception = new Exception("Unexpected error");
            _paymentStoreMock
                .Setup(x => x.GetPaymentOptionByID(id))
                .ThrowsAsync(exception);

            // Act
            var result = await _consumerService.DeletePaymentRecord(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to delete payment.", result.ErrorMessage);
            _paymentStoreMock.Verify(x => x.GetPaymentOptionByID(id), Times.Once());
            _paymentStoreMock.Verify(
                x => x.DeletePaymentOption(It.IsAny<PaymentOptions>()),
                Times.Never());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to delete payment record")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

    }
}
