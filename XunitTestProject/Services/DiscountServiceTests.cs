using AutoMapper;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Discount;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace XunitTestProject.Services
{
    public class DiscountServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUtility> _utilityMock;
        private readonly Mock<IDiscountStore> _discountStoreMock;
        private readonly Mock<ILogger<IDiscountService>> _loggerMock;
        private readonly DiscountService _discountService;

        public DiscountServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _utilityMock = new Mock<IUtility>();
            _discountStoreMock = new Mock<IDiscountStore>();
            _loggerMock = new Mock<ILogger<IDiscountService>>();
            _discountService = new DiscountService(_mapperMock.Object, _utilityMock.Object, _discountStoreMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task DeleteDiscount_NotFound_Fails()
        {
            // Arrange
            int discountId = 999;
            _discountStoreMock
                .Setup(x => x.GetDiscountByID(discountId))
                .ReturnsAsync((Discount)null);

            // Act
            var result = await _discountService.DeleteDiscount(discountId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to delete discount.", result.ErrorMessage);
            _discountStoreMock.Verify(x => x.GetDiscountByID(discountId), Times.Once());
            _discountStoreMock.Verify(x => x.DeleteDiscounts(It.IsAny<Discount>()), Times.Never());
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteDiscount_ValidId_Success()
        {
            // Arrange
            int discountId = 1;
            var discount = new Discount { DiscountID = discountId, DiscountPercent = 0.1m, DiscountAmount = 0, DiscountActive =true, DiscountTag="test" };
            _discountStoreMock
                .Setup(x => x.GetDiscountByID(discountId))
                .ReturnsAsync(discount);
            _discountStoreMock
                .Setup(x => x.DeleteDiscounts(discount))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _discountService.DeleteDiscount(discountId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(discountId, result.Data);
            _discountStoreMock.Verify(x => x.GetDiscountByID(discountId), Times.Once());
            _discountStoreMock.Verify(x => x.DeleteDiscounts(discount), Times.Once());
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteDiscount_DbUpdateException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            int discountId = 1;
            var discount = new Discount { DiscountID = discountId, DiscountPercent = 0.1m, DiscountAmount = 0, DiscountActive = true, DiscountTag = "test" };
            var exception = new DbUpdateException("Database error", new Exception());
            _discountStoreMock
                .Setup(x => x.GetDiscountByID(discountId))
                .ReturnsAsync(discount);
            _discountStoreMock
                .Setup(x => x.DeleteDiscounts(discount))
                .ThrowsAsync(exception);

            // Act
            var result = await _discountService.DeleteDiscount(discountId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to delete discount in database.", result.ErrorMessage);
            _discountStoreMock.Verify(x => x.GetDiscountByID(discountId), Times.Once());
            _discountStoreMock.Verify(x => x.DeleteDiscounts(discount), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to delete discount")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task DeleteDiscount_Exception_LogsErrorAndReturnsFailure()
        {
            // Arrange
            int discountId = 1;
            var exception = new Exception("Unexpected error");
            _discountStoreMock
                .Setup(x => x.GetDiscountByID(discountId))
                .ThrowsAsync(exception);

            // Act
            var result = await _discountService.DeleteDiscount(discountId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to delete discount.", result.ErrorMessage);
            _discountStoreMock.Verify(x => x.GetDiscountByID(discountId), Times.Once());
            _discountStoreMock.Verify(x => x.DeleteDiscounts(It.IsAny<Discount>()), Times.Never());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to delete discount")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task GetDiscount_NonExistentId_ReturnsFailure()
        {
            // Arrange
            int discountId = 999;
            _discountStoreMock
                .Setup(x => x.GetDiscountsWithSelections(discountId))
                .ReturnsAsync((DiscountDTO)null);

            // Act
            var result = await _discountService.GetDiscount(discountId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to retrieve Discounts.", result.ErrorMessage);
            _discountStoreMock.Verify(x => x.GetDiscountsWithSelections(discountId), Times.Once());
            _mapperMock.VerifyNoOtherCalls();
            _utilityMock.VerifyNoOtherCalls();
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetDiscount_ValidId_ReturnsSuccessWithMappedDiscount()
        {
            // Arrange
            int discountId = 1;
            var discount = new DiscountDTO { DiscountID = discountId, DiscountPercent = 0.1m, DiscountAmount = 0, DiscountActive = true, DiscountTag = "test", DiscountArt = new byte[] { 1, 2, 3 } };
            var discountVM = new DiscountVM { DiscountID = discountId, DiscountPercent = 0.1m, DiscountAmount = 0, DiscountActive = true, DiscountTag = "test", DiscountArt = discount.DiscountArt };
            _discountStoreMock
                .Setup(x => x.GetDiscountsWithSelections(discountId))
                .ReturnsAsync(discount);
            _mapperMock
                .Setup(x => x.Map<DiscountVM>(discount))
                .Returns(discountVM);
            _utilityMock
                .Setup(x => x.BytesToImage(discount.DiscountArt))
                .Returns("base64image");

            // Act
            var result = await _discountService.GetDiscount(discountId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(discountId, result.Data.DiscountID);
            Assert.Equal("base64image", result.Data.DiscountImage);
            _discountStoreMock.Verify(x => x.GetDiscountsWithSelections(discountId), Times.Once());
            _mapperMock.Verify(x => x.Map<DiscountVM>(discount), Times.Once());
            _utilityMock.Verify(x => x.BytesToImage(discount.DiscountArt), Times.Once());
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetDiscount_Exception_LogsErrorAndReturnsFailure()
        {
            // Arrange
            int discountId = 1;
            var exception = new Exception("Store error");
            _discountStoreMock
                .Setup(x => x.GetDiscountsWithSelections(discountId))
                .ThrowsAsync(exception);

            // Act
            var result = await _discountService.GetDiscount(discountId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to retrieve discount details.", result.ErrorMessage);
            _discountStoreMock.Verify(x => x.GetDiscountsWithSelections(discountId), Times.Once());
            _mapperMock.VerifyNoOtherCalls();
            _utilityMock.VerifyNoOtherCalls();
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to get discount")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task UpdateDiscount_NullDiscount_ReturnsFailure()
        {
            // Arrange
            DiscountEditVM discount = null;

            // Act
            var result = await _discountService.UpdateDiscount(discount);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to save discount.", result.ErrorMessage);
            _discountStoreMock.Verify(x => x.UpdateDiscount(It.IsAny<DiscountEditVM>()), Times.Never());
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task UpdateDiscount_ValidDiscount_ReturnsSuccess()
        {            
            // Arrange
            var discount = new DiscountEditVM { DiscountID = 1, DiscountPercent = 0.1m };
            _discountStoreMock
                .Setup(x => x.UpdateDiscount(discount))
                .ReturnsAsync(1);

            // Act
            var result = await _discountService.UpdateDiscount(discount);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(discount, result.Data);
            _discountStoreMock.Verify(x => x.UpdateDiscount(discount), Times.Once());
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task UpdateDiscount_DbUpdateException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            var discount = new DiscountEditVM { DiscountID = 1 };
            var exception = new DbUpdateException("Database error", new Exception());
            _discountStoreMock
                .Setup(x => x.UpdateDiscount(discount))
                .ThrowsAsync(exception);

            // Act
            var result = await _discountService.UpdateDiscount(discount);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to update discount.", result.ErrorMessage);
            _discountStoreMock.Verify(x => x.UpdateDiscount(discount), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to update discount")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task UpdateDiscount_Exception_LogsErrorAndReturnsFailure()
        {
            // Arrange
            var discount = new DiscountEditVM { DiscountID = 1 };
            var exception = new Exception("Unexpected error");
            _discountStoreMock
                .Setup(x => x.UpdateDiscount(discount))
                .ThrowsAsync(exception);

            // Act
            var result = await _discountService.UpdateDiscount(discount);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to update discount.", result.ErrorMessage);
            _discountStoreMock.Verify(x => x.UpdateDiscount(discount), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to update discount")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }     

        [Fact]
        public async Task CreateAsync_ValidDiscountWithPercentAndAmount_ReturnsCreatedDiscount()
        {
            // Arrange
            var discountVM = new DiscountCreateVM
            {
                DiscountPercent = 10, // Will be divided by 100
                DiscountAmount = 500, // Will be divided by 100
                DiscountArtFile = new Mock<IFormFile>().Object
            };
            var discountEntity = new Discount
            {
                DiscountPercent = 0.1m,
                DiscountAmount = 5.0m,
                DiscountArt = new byte[] { 1, 2, 3 },
                DiscountActive = true,
                DiscountTag = "test"
            };
            _mapperMock
                .Setup(x => x.Map<Discount>(It.Is<DiscountCreateVM>(vm => vm.DiscountPercent == 0.1m && vm.DiscountAmount == 5.0m)))
                .Returns(discountEntity);
            _utilityMock
                .Setup(x => x.ImageBytes(discountVM.DiscountArtFile))
                .Returns(discountEntity.DiscountArt);
            _discountStoreMock
                .Setup(x => x.AddDiscount(discountEntity))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _discountService.CreateAsync(discountVM);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0.1m, result.DiscountPercent);
            Assert.Equal(5.0m, result.DiscountAmount);
            _mapperMock.Verify(x => x.Map<Discount>(It.IsAny<DiscountCreateVM>()), Times.Once());
            _utilityMock.Verify(x => x.ImageBytes(discountVM.DiscountArtFile), Times.Once());
            _discountStoreMock.Verify(x => x.AddDiscount(discountEntity), Times.Once());
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task CreateAsync_ValidDiscountWith1PercentValue_ReturnsCreatedDiscount()
        {
            // Arrange
            var discountVM = new DiscountCreateVM
            {
                DiscountPercent = 1,
                DiscountAmount = 0,
                DiscountArtFile = new Mock<IFormFile>().Object
            };
            var discountEntity = new Discount
            {
                DiscountPercent = 1,
                DiscountAmount = 0,
                DiscountArt = new byte[] { 1, 2, 3 },
                DiscountActive = true,
                DiscountTag = "test"
            };
            _mapperMock
                .Setup(x => x.Map<Discount>(discountVM))
                .Returns(discountEntity);
            _utilityMock
                .Setup(x => x.ImageBytes(discountVM.DiscountArtFile))
                .Returns(discountEntity.DiscountArt);
            _discountStoreMock
                .Setup(x => x.AddDiscount(discountEntity))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _discountService.CreateAsync(discountVM);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.DiscountPercent);
            Assert.Equal(0, result.DiscountAmount);
            _mapperMock.Verify(x => x.Map<Discount>(discountVM), Times.Once());
            _utilityMock.Verify(x => x.ImageBytes(discountVM.DiscountArtFile), Times.Once());
            _discountStoreMock.Verify(x => x.AddDiscount(discountEntity), Times.Once());
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]       
        public async Task CreateAsync_ValidDiscountWith10DollarValue_ReturnsCreatedDiscount()
        {
            // Arrange
            var discountVM = new DiscountCreateVM
            {
                DiscountPercent = 0,
                DiscountAmount = 10,
                DiscountArtFile = new Mock<IFormFile>().Object
            };
            var discountEntity = new Discount
            {
                DiscountPercent = 0,
                DiscountAmount = 10,
                DiscountArt = new byte[] { 1, 2, 3 },
                DiscountActive = true,
                DiscountTag = "test"
            };
            _mapperMock
                .Setup(x => x.Map<Discount>(discountVM))
                .Returns(discountEntity);
            _utilityMock
                .Setup(x => x.ImageBytes(discountVM.DiscountArtFile))
                .Returns(discountEntity.DiscountArt);
            _discountStoreMock
                .Setup(x => x.AddDiscount(discountEntity))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _discountService.CreateAsync(discountVM);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.DiscountPercent);
            Assert.Equal(10, result.DiscountAmount);
            _mapperMock.Verify(x => x.Map<Discount>(discountVM), Times.Once());
            _utilityMock.Verify(x => x.ImageBytes(discountVM.DiscountArtFile), Times.Once());
            _discountStoreMock.Verify(x => x.AddDiscount(discountEntity), Times.Once());
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task CreateAsync_DbUpdateException_LogsErrorAndThrows()
        {
            // Arrange
            var discountVM = new DiscountCreateVM { DiscountPercent = 10 };
            var exception = new DbUpdateException("Database error", new Exception());
            _mapperMock
                .Setup(x => x.Map<Discount>(It.IsAny<DiscountCreateVM>()))
                .Returns(new Discount
                {
                    DiscountPercent = 0,
                    DiscountAmount = 0,
                    DiscountArt = new byte[] { 1, 2, 3 },
                    DiscountActive = true,
                    DiscountTag = "test"
                });
            _utilityMock
                .Setup(x => x.ImageBytes(It.IsAny<IFormFile>()))
                .Returns(new byte[] { 1 });
            _discountStoreMock
                .Setup(x => x.AddDiscount(It.IsAny<Discount>()))
                .ThrowsAsync(exception);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _discountService.CreateAsync(discountVM));
            Assert.Equal("An error occurred while processing your request", ex.Message);
            _discountStoreMock.Verify(x => x.AddDiscount(It.IsAny<Discount>()), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create discount")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task CreateAsync_Exception_LogsErrorAndThrows()
        {
            // Arrange
            var discountVM = new DiscountCreateVM { DiscountPercent = 10 };
            var exception = new Exception("Unexpected error");
            _mapperMock
                .Setup(x => x.Map<Discount>(It.IsAny<DiscountCreateVM>()))
                .Throws(exception);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _discountService.CreateAsync(discountVM));
            Assert.Equal("An error occurred while processing your request", ex.Message);
            _discountStoreMock.Verify(x => x.AddDiscount(It.IsAny<Discount>()), Times.Never());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create discount")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public async Task GetActiveDiscounts_NoDiscounts_ReturnsFailure()
        {
            // Arrange
            _discountStoreMock
                .Setup(x => x.GetAllDiscounts())
                .ReturnsAsync((List<DiscountDTO>)null);

            // Act
            var result = await _discountService.GetActiveDiscounts();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to retrieve Discounts.", result.ErrorMessage);
            _discountStoreMock.Verify(x => x.GetAllDiscounts(), Times.Once());
            _mapperMock.VerifyNoOtherCalls();
            _utilityMock.VerifyNoOtherCalls();
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetActiveDiscounts_ValidDiscounts_ReturnsSuccessWithMappedList()
        {
            // Arrange
            var discounts = new List<DiscountDTO>
            {
                new DiscountDTO {  DiscountID = 1, DiscountPercent = 0.1m, DiscountAmount = 0, DiscountActive = true, DiscountTag = "test", DiscountArt = new byte[] { 1, 2, 3 }  },
                new DiscountDTO {  DiscountID = 2, DiscountPercent = 0.1m, DiscountAmount = 0, DiscountActive = true, DiscountTag = "test", DiscountArt = new byte[] { 1, 2, 3 }  }
            };
                    var discountVMs = new List<DiscountVM>
            {
                new DiscountVM { DiscountID = 1, DiscountPercent = 0.1m, DiscountAmount = 0, DiscountActive = true, DiscountTag = "test", DiscountArt = new byte[] { 1, 2, 3 } },
                new DiscountVM { DiscountID = 2, DiscountPercent = 0.1m, DiscountAmount = 0, DiscountActive = true, DiscountTag = "test", DiscountArt = new byte[] { 1, 2, 3 } }
            };
            _discountStoreMock
                .Setup(x => x.GetAllDiscounts())
                .ReturnsAsync(discounts);
            _mapperMock
                .Setup(x => x.Map<List<DiscountVM>>(discounts))
                .Returns(discountVMs);

            // Act
            var result = await _discountService.GetActiveDiscounts();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            _discountStoreMock.Verify(x => x.GetAllDiscounts(), Times.Once());
            _mapperMock.Verify(x => x.Map<List<DiscountVM>>(discounts), Times.Once());
            _utilityMock.Verify(x => x.BytesToImage(It.IsAny<byte[]>()), Times.Exactly(2));
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetActiveDiscounts_Exception_LogsErrorAndReturnsFailure()
        {
            // Arrange
            var exception = new Exception("Store error");
            _discountStoreMock
                .Setup(x => x.GetAllDiscounts())
                .ThrowsAsync(exception);

            // Act
            var result = await _discountService.GetActiveDiscounts();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Unable to retrieve Discounts.", result.ErrorMessage);
            _discountStoreMock.Verify(x => x.GetAllDiscounts(), Times.Once());
            _mapperMock.VerifyNoOtherCalls();
            _utilityMock.VerifyNoOtherCalls();
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to get active discounts")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }
    }
}
