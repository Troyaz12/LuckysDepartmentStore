using Humanizer;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data.Common;

namespace XunitTestProject.Services
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryStore> _mockStore;
        private readonly Mock<ILogger<ICategoryService>> _loggerMock;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _mockStore = new Mock<ICategoryStore>();
            _loggerMock = new Mock<ILogger<ICategoryService>>();
            _categoryService = new CategoryService(_mockStore.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_NullProduct_ReturnsFail()
        {
            ProductCreateVM productCreate = null;

            var res = await _categoryService.Create(productCreate);

            Assert.False(res.IsSuccess);
            Assert.Equal("Product data not received.", res.ErrorMessage);
            _mockStore.Verify(x => x.CreateCategory(It.IsAny<ProductCreateVM>()), Times.Never);
        }

        [Fact]

        public async Task Create_ValidCategory_ReturnsSuccessWithCategoryId()
        {
            var productCreate = new ProductCreateVM();
            var expectedCategoryId = 55;
            _mockStore.Setup(x=>x.CreateCategory(productCreate)).ReturnsAsync(expectedCategoryId);

            var result = await _categoryService.Create(productCreate);

            Assert.True(result.IsSuccess);
            Assert.Equal(expectedCategoryId, result.Data);
            _mockStore.Verify(x => x.CreateCategory(productCreate),Times.Once);
            _loggerMock.VerifyNoOtherCalls();

        }

        [Fact]
        public async Task Create_DbUpdateException_ReturnsFailureAndLogsError()
        {
            var productCreate = new ProductCreateVM();
            var exception = new DbUpdateException("Database Error" + new Exception());

            _mockStore.Setup(x => x.CreateCategory(productCreate)).ThrowsAsync(exception);

            var result = await _categoryService.Create(productCreate);

            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to save category to database.", result.ErrorMessage);
            _mockStore.Verify(x => x.CreateCategory(productCreate), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v,t) => v.ToString().Contains("Failed to create category in database for")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                    Times.Once);
        }

        [Fact]
        public async Task Create_Exception_ReturnsFailureAndLogsError()
        {
            var productCreate = new ProductCreateVM();
            var exception = new Exception("Database Error" + new Exception());

            _mockStore.Setup(x => x.CreateCategory(productCreate)).ThrowsAsync(exception);

            var result = await _categoryService.Create(productCreate);

            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to create product.", result.ErrorMessage);
            _mockStore.Verify(x => x.CreateCategory(productCreate), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create category for product")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                    Times.Once);
        }

    }
}
