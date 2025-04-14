using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Service;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.EntityFrameworkCore;
using LuckysDepartmentStore.Models;
using System;

namespace XunitTestProject.Services
{
    public class ColorServiceTests
    {
        private readonly Mock<IColorStore> _mockStore;
        private readonly Mock<ILogger<IColorService>> _loggerMock;
        private readonly ColorService _colorService;

        public ColorServiceTests() 
        {
            _mockStore = new Mock<IColorStore>();
            _loggerMock = new Mock<ILogger<IColorService>>();
            _colorService = new ColorService(_mockStore.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_NewColor_ReturnsSuccess()
        {
            string colorName = "Red";
            int expectedColorId = 1;
            _mockStore.Setup(x => x.AddColor(colorName)).ReturnsAsync(expectedColorId);

            var result = await _colorService.Create(colorName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedColorId, result.Data);
            _mockStore.Verify(x => x.AddColor(colorName), Times.Once());
        }

        [Fact]
        public async Task Create_ColorDbUpdateException_ReturnsFail()
        {
            string colorName = "Red";
            var exception = new DbUpdateException("Database error", new Exception());
            _mockStore.Setup(x => x.AddColor(colorName)).ThrowsAsync(exception);

            var result = await _colorService.Create(colorName);

            // Assert
            Assert.True(!result.IsSuccess);
            Assert.Equal("Failed to save color to database.", result.ErrorMessage);
            _mockStore.Verify(x => x.AddColor(colorName), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create color")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Create_ColorException_ReturnsFail()
        {
            string colorName = "Red";
            var exception = new Exception("Database error", new Exception());
            _mockStore.Setup(x => x.AddColor(colorName)).ThrowsAsync(exception);

            var result = await _colorService.Create(colorName);

            // Assert
            Assert.True(!result.IsSuccess);
            Assert.Equal("Unable to create color.", result.ErrorMessage);
            _mockStore.Verify(x => x.AddColor(colorName), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create color")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task GetColorName_ReturnsSuccess()
        {
            string colorName = "Red";
            int expectedColorId = 1;
            _mockStore.Setup(x => x.GetColorName(expectedColorId)).ReturnsAsync(colorName);

            var result = await _colorService.GetColorName(expectedColorId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(colorName, result.Data);
            _mockStore.Verify(x => x.GetColorName(expectedColorId), Times.Once());
        }

        [Fact]
        public async Task GetColorName_ExceptionColorNotFound_ReturnsFail()
        {            
            string colorName = null;
            int expectedColorId = 1;
            _mockStore.Setup(x => x.GetColorName(expectedColorId)).ReturnsAsync(colorName);

            var result = await _colorService.GetColorName(expectedColorId);

            // Assert
            Assert.True(!result.IsSuccess);
            _mockStore.Verify(x => x.GetColorName(expectedColorId), Times.Once());
            Assert.Equal("Color not found for the given ID.", result.ErrorMessage);
            _mockStore.Verify(x => x.GetColorName(expectedColorId), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to get color")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task GetColorName_Exception_ReturnsFail()
        {
            var exception = new Exception("Database error", new Exception());
            int expectedColorId = 1;
            _mockStore.Setup(x => x.GetColorName(expectedColorId)).ThrowsAsync(exception);

            var result = await _colorService.GetColorName(expectedColorId);

            // Assert
            Assert.True(!result.IsSuccess);
            _mockStore.Verify(x => x.GetColorName(expectedColorId), Times.Once());
            Assert.Equal("Unable to get color name.", result.ErrorMessage);
            _mockStore.Verify(x => x.GetColorName(expectedColorId), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to get color")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }


        [Fact]
        public async Task GetSizeName_ReturnsSuccess()
        {
            string SizeName = "Red";
            int expectedSizeId = 1;
            _mockStore.Setup(x => x.GetSizeName(expectedSizeId)).ReturnsAsync(SizeName);

            var result = await _colorService.GetSizeName(expectedSizeId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(SizeName, result.Data);
            _mockStore.Verify(x => x.GetSizeName(expectedSizeId), Times.Once());
        }

        [Fact]
        public async Task GetSizeName_ExceptionSizeNotFound_ReturnsFail()
        {            
            string SizeName = null;
            int expectedSizeId = 1;
            _mockStore.Setup(x => x.GetSizeName(expectedSizeId)).ReturnsAsync(SizeName);

            var result = await _colorService.GetSizeName(expectedSizeId);

            // Assert
            Assert.True(!result.IsSuccess);
            _mockStore.Verify(x => x.GetSizeName(expectedSizeId), Times.Once());
            Assert.Equal("Size not found for the given ID.", result.ErrorMessage);
            _mockStore.Verify(x => x.GetSizeName(expectedSizeId), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to get size")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task GetSizeName_Exception_ReturnsFail()
        {
            var exception = new Exception("Database error", new Exception());
            int expectedSizeId = 1;
            _mockStore.Setup(x => x.GetSizeName(expectedSizeId)).ThrowsAsync(exception);

            var result = await _colorService.GetSizeName(expectedSizeId);

            // Assert
            Assert.True(!result.IsSuccess);
            _mockStore.Verify(x => x.GetSizeName(expectedSizeId), Times.Once());
            Assert.Equal("Unable to get size name.", result.ErrorMessage);
            _mockStore.Verify(x => x.GetSizeName(expectedSizeId), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to get size name")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }





        [Fact]
        public async Task Create_NewSize_ReturnsSuccess()
        {
            string SizeName = "L";
            int expectedSizeId = 1;
            _mockStore.Setup(x => x.CreateSize(SizeName)).ReturnsAsync(expectedSizeId);

            var result = await _colorService.CreateSize(SizeName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedSizeId, result.Data);
            _mockStore.Verify(x => x.CreateSize(SizeName), Times.Once());
        }

        [Fact]
        public async Task Create_SizeDbUpdateException_ReturnsFail()
        {
            string SizeName = "L";
            var exception = new DbUpdateException("Database error", new Exception());
            _mockStore.Setup(x => x.CreateSize(SizeName)).ThrowsAsync(exception);

            var result = await _colorService.CreateSize(SizeName);

            // Assert
            Assert.True(!result.IsSuccess);
            Assert.Equal("Failed to save size to database.", result.ErrorMessage);
            _mockStore.Verify(x => x.CreateSize(SizeName), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create size")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Create_SizeException_ReturnsFail()
        {
            string SizeName = "L";
            var exception = new Exception("Database error", new Exception());
            _mockStore.Setup(x => x.CreateSize(SizeName)).ThrowsAsync(exception);

            var result = await _colorService.CreateSize(SizeName);

            // Assert
            Assert.True(!result.IsSuccess);
            Assert.Equal("Unable to create size.", result.ErrorMessage);
            _mockStore.Verify(x => x.CreateSize(SizeName), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Failed to create size")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
