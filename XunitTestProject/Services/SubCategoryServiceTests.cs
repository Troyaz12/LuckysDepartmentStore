using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Service;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckysDepartmentStore.Service.Interfaces;

namespace XunitTestProject.Services
{
    public class SubCategoryServiceTests
    {
        private readonly Mock<ISubcategoryStore> _subcategoryStoreMock;
        private readonly Mock<ILogger<ISubCategoryService>> _loggerMock;
        private readonly SubCategoryService _subCategoryService;

        public SubCategoryServiceTests()
        {
            _subcategoryStoreMock = new Mock<ISubcategoryStore>();
            _loggerMock = new Mock<ILogger<ISubCategoryService>>();
            _subCategoryService = new SubCategoryService(_subcategoryStoreMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_ValidProduct_ReturnsSuccessWithId()
        {
            // Arrange
            var product = new ProductCreateVM { SubCategorySelection = "Electronics" };
            int createdId = 1;
            _subcategoryStoreMock
                .Setup(x => x.CreateSubcategory(It.Is<SubCategory>(sc =>
                    sc.SubCategoryName == "Electronics" &&
                    sc.SubCategoryDescription == "Electronics")))
                .ReturnsAsync(createdId);

            // Act
            var result = await _subCategoryService.Create(product);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(createdId, result.Data);
            _subcategoryStoreMock.Verify(x => x.CreateSubcategory(It.Is<SubCategory>(sc =>
                sc.SubCategoryName == "Electronics" &&
                sc.SubCategoryDescription == "Electronics")), Times.Once());
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_NullProduct_LogsErrorAndReturnsFailure()
        {
            // Arrange
            ProductCreateVM product = null;

            // Act
            var result = await _subCategoryService.Create(product);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Subcategory creation failed.", result.ErrorMessage);
            _subcategoryStoreMock.Verify(x => x.CreateSubcategory(It.IsAny<SubCategory>()), Times.Never());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to create a subCategory")),
                    It.Is<NullReferenceException>(ex => ex.Message.Contains("Object reference not set")),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }      

        [Fact]
        public async Task Create_StoreThrowsException_LogsErrorAndReturnsFailure()
        {
            // Arrange
            var product = new ProductCreateVM { SubCategorySelection = "Electronics" };
            var exception = new Exception("Database error");
            _subcategoryStoreMock
                .Setup(x => x.CreateSubcategory(It.IsAny<SubCategory>()))
                .ThrowsAsync(exception);

            // Act
            var result = await _subCategoryService.Create(product);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Subcategory creation failed.", result.ErrorMessage);
            _subcategoryStoreMock.Verify(x => x.CreateSubcategory(It.IsAny<SubCategory>()), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to create a subCategory")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }


    }
}
