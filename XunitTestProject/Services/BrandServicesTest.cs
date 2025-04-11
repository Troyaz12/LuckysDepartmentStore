using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace XunitTestProject.Services
{
    public class BrandServicesTest
    {
        private readonly Mock<IBrandStore> _mockStore;
        private readonly Mock<ILogger<IBrandService>> _loggerMock;
        private readonly BrandService _brandService;

        public BrandServicesTest()
        {
            _mockStore = new Mock<IBrandStore>();
            _loggerMock = new Mock<ILogger<IBrandService>>();
            _brandService = new BrandService(_mockStore.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_NullProduct_ReturnsFail()
        {
            ProductCreateVM product = null;

            var result = await _brandService.Create(product);

            Assert.False(result.IsSuccess);
            Assert.Equal("Product data not received.", result.ErrorMessage);
            _mockStore.Verify(x => x.CreateBrand(It.IsAny<ProductCreateVM>()), Times.Never);
        }

        [Fact]
        public async Task Create_ValidProduct_ReturnsSuccessWithBrandId()
        {
            // Arrange
            var product = new ProductCreateVM { /* Assume properties like Name */ };
            var expectedBrandId = 42;
            _mockStore.Setup(x => x.CreateBrand(product)).ReturnsAsync(expectedBrandId);

            // Act
            var result = await _brandService.Create(product);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedBrandId, result.Data);
            _mockStore.Verify(x => x.CreateBrand(product), Times.Once);
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_DbUpdateException_ReturnsFailureAndLogsError()
        {
            // Arrange
            var product = new ProductCreateVM { /* Assume properties */ };
            var exception = new DbUpdateException("Database error", new Exception());
            _mockStore.Setup(x => x.CreateBrand(product)).ThrowsAsync(exception);

            // Act
            var result = await _brandService.Create(product);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to save brand to database.", result.ErrorMessage);
            _mockStore.Verify(x => x.CreateBrand(product), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create brand for product")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Create_GeneralException_ReturnsFailureAndLogsError()
        {
            // Arrange
            var product = new ProductCreateVM { /* Assume properties */ };
            var exception = new Exception("Unexpected error");
            _mockStore.Setup(x => x.CreateBrand(product)).ThrowsAsync(exception);

            // Act
            var result = await _brandService.Create(product);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to create brand.", result.ErrorMessage);
            _mockStore.Verify(x => x.CreateBrand(product), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create brand for product")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

    }
}
