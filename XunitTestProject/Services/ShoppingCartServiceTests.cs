using AutoMapper;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;
using LuckysDepartmentStore.Service;
using Moq;
using LuckysDepartmentStore.Utilities;
using System;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace XunitTestProject.Services
{
    public class ShoppingCartServiceTests
    {
        private LuckysContext _context;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextMock;
        private readonly Mock<IUtility> _utilityMock;
        private readonly Mock<IColorService> _colorServiceMock;
        private readonly Mock<IShoppingCartStore> _shoppingCartStoreMock;
        private readonly Mock<ICustomerOrderItemsStore> _customerOrderItemsStoreMock;
        private readonly Mock<ILogger<ShoppingCartService>> _loggerMock;
        private ShoppingCartService _service;

        public ShoppingCartServiceTests()
        {
            //_contextMock = new Mock<LuckysContext>();
            _mapperMock = new Mock<IMapper>();
            _httpContextMock = new Mock<IHttpContextAccessor>();
            _utilityMock = new Mock<IUtility>();
            _colorServiceMock = new Mock<IColorService>();
            _shoppingCartStoreMock = new Mock<IShoppingCartStore>();
            _customerOrderItemsStoreMock = new Mock<ICustomerOrderItemsStore>();
            _loggerMock = new Mock<ILogger<ShoppingCartService>>();

            // Mock HttpContext and Session
            var httpContext = new DefaultHttpContext();
            var sessionMock = new Mock<ISession>();
            httpContext.Session = sessionMock.Object;
            _httpContextMock.Setup(x => x.HttpContext).Returns(httpContext);
        }

        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<LuckysContext>()
          .UseSqlite("Filename=:memory:")
          .Options;

            _context = new LuckysContext(options);
            await _context.Database.OpenConnectionAsync(); // Required!
            await _context.Database.EnsureCreatedAsync();

            _service = new ShoppingCartService(
                _context,
                _mapperMock.Object,
                _httpContextMock.Object,
                _utilityMock.Object,
                _colorServiceMock.Object,
                _shoppingCartStoreMock.Object,
                _customerOrderItemsStoreMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task AddToCartAsync_NewItem_AddsItemSuccessfully()
        {
            await InitializeAsync();
            // Arrange
            var product = new CartItemsDTO
            {
                ProductID = 1,
                Quantity = 2,
                Price = 10.00m,
                ProductName = "Test Product",
                ProductImage = "image.png",
                ColorSelection = 1,
                SizeSelection = 1
            };
            var cartId = "cart123";
            var cartItem = new Carts
            {
                ProductID = 1,
                CartID = cartId,
                Quantity = 2,
                Price = 10.00m,
                ProductName = "Test Product",
                ProductPicture = new byte[] { 1, 2, 3 },
                Color = 1,
                Size = 1,
                CreatedDate = DateTime.Now
            };
            var cartVM = new CartsVM { ProductID = 1, Quantity = 2, Price = 10.00m };

            _shoppingCartStoreMock.Setup(x => x.CheckForItemInCart(product, cartId))
                .ReturnsAsync((Carts)null);
            _utilityMock.Setup(x => x.StringToBytes(product.ProductImage))
                .Returns(new byte[] { 1, 2, 3 });
            _shoppingCartStoreMock.Setup(x => x.AddItemToNewCart(It.IsAny<Carts>()))
                .ReturnsAsync(1);
            _mapperMock.Setup(x => x.Map<CartsVM>(It.IsAny<Carts>()))
                .Returns(cartVM);

            // Act
            var result = await _service.AddToCartAsync(product, cartId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(cartVM, result.Data);
            _shoppingCartStoreMock.Verify(x => x.AddItemToNewCart(It.IsAny<Carts>()), Times.Once());
        }

        [Fact]
        public async Task AddToCartAsync_ExistingItem_UpdatesQuantity()
        {
            await InitializeAsync();

            var product = new CartItemsDTO { ProductID = 1, Quantity = 1, Price = 10.00m };
            var cartId = "cart123";
            var existingCartItem = new Carts { ProductID = 1, CartID = cartId, Quantity = 2, Price = 10.00m };
            var cartVM = new CartsVM { ProductID = 1, Quantity = 3, Price = 10.00m };

            _shoppingCartStoreMock.Setup(x => x.CheckForItemInCart(product, cartId))
                .ReturnsAsync(existingCartItem);
            _shoppingCartStoreMock.Setup(x => x.UpdateCartItemQuantity(existingCartItem, 3))
                .ReturnsAsync(1);
            _mapperMock.Setup(x => x.Map<CartsVM>(existingCartItem))
                .Returns(cartVM);

            // Act
            var result = await _service.AddToCartAsync(product, cartId);

            Assert.True(result.IsSuccess);
            Assert.Equal(cartVM, result.Data);
            _shoppingCartStoreMock.Verify(x => x.UpdateCartItemQuantity(existingCartItem, 3), Times.Once());
            _shoppingCartStoreMock.Verify(x => x.AddItemToNewCart(It.IsAny<Carts>()), Times.Never());
        }

        [Fact]
        public async Task AddToCartAsync_DbUpdateException_ReturnsFailure()
        {
            await InitializeAsync();

            var exception = new DbUpdateException("Database error", new Exception());

            var product = new CartItemsDTO { ProductID = 1, Quantity = 1 };
            var cartId = "cart123";
            _shoppingCartStoreMock.Setup(x => x.CheckForItemInCart(product, cartId))
                .ReturnsAsync((Carts)null);
            _utilityMock.Setup(x => x.StringToBytes(It.IsAny<string>()))
                .Returns(new byte[] { 1, 2, 3 });
            _shoppingCartStoreMock.Setup(x => x.AddItemToNewCart(It.IsAny<Carts>()))
                .ThrowsAsync(exception);

            // Act
            var result = await _service.AddToCartAsync(product, cartId);

            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to add to cart.", result.ErrorMessage);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to add to cart to database.")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task AddToCartAsync_GeneralException_ReturnsFailure()
        {
            await InitializeAsync();

            var exception = new Exception("error", new Exception());

            var product = new CartItemsDTO { ProductID = 1, Quantity = 1 };
            var cartId = "cart123";
            _shoppingCartStoreMock.Setup(x => x.CheckForItemInCart(product, cartId))
                .ReturnsAsync((Carts)null);
            _utilityMock.Setup(x => x.StringToBytes(It.IsAny<string>()))
                .Returns(new byte[] { 1, 2, 3 });
            _shoppingCartStoreMock.Setup(x => x.AddItemToNewCart(It.IsAny<Carts>()))
                .ThrowsAsync(exception);

            // Act
            var result = await _service.AddToCartAsync(product, cartId);

            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to add to cart.", result.ErrorMessage);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to add to cart.")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task EmpyCart_NoItems_ReturnsFailure()
        {
            await InitializeAsync();

            var cartId = "cart123";

            _shoppingCartStoreMock.Setup(x => x.GetCartDataOnlyItems(cartId))
                .ReturnsAsync((List<Carts>)null);

            // Act
            var result = await _service.EmptyCart(cartId);

            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to get cart items.", result.ErrorMessage);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to get cart items.")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task EmpyCart_GeneralException_ReturnsFailure()
        {
            await InitializeAsync();

            var exception = new Exception("error", new Exception());

            var cartId = "cart123";

            _shoppingCartStoreMock.Setup(x => x.GetCartDataOnlyItems(cartId))
                .ThrowsAsync(exception);

            // Act
            var result = await _service.EmptyCart(cartId);

            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to remove cart item.", result.ErrorMessage);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to add to cart.")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task EmptyCart_RemoveFromCart_EmptiesCart()
        {
            await InitializeAsync();

            var cartId = "cart123";
            var cartItems = new List<Carts>
            {
                new Carts { ProductID = 1, CartID = cartId },
                new Carts { ProductID = 2, CartID = cartId }
            };
            _shoppingCartStoreMock.Setup(x => x.GetCartDataOnlyItems(cartId))
                .ReturnsAsync(cartItems);
            _shoppingCartStoreMock.Setup(x => x.RemoveFromCart(It.IsAny<Carts>()))
                .ReturnsAsync(1);

            // Act
            var result = await _service.EmptyCart(cartId);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data);
            _shoppingCartStoreMock.Verify(x => x.RemoveFromCart(It.IsAny<Carts>()), Times.Exactly(2));
        }


        [Fact]
        public async Task GetCartItems_ValidCartId_ReturnsCartItems()
        {
            await InitializeAsync();

            var cartId = "cart123";
            var cartItems = new List<CartsDTO>
            {
                new CartsDTO { ProductID = 1, CartID = cartId, Quantity = 1, Price = 10.00m, ProductPicture = new byte[] { 1, 2, 3 } }
            };
            var cartVMs = new List<CartsVM>
            {
                new CartsVM { ProductID = 1, Quantity = 1, Price = 10.00m, ProductPicture = new byte[] { 1, 2, 3 } }
            };
            var shoppingCartVM = new ShoppingCartVM { cartsVMs = cartVMs, CartTotal = 10.00m, CartSum = 1 };

            _shoppingCartStoreMock.Setup(x => x.GetCartItemsAllData(cartId))
                .ReturnsAsync(cartItems);
            _mapperMock.Setup(x => x.Map<List<CartsVM>>(cartItems))
                .Returns(cartVMs);
            _utilityMock.Setup(x => x.BytesToImage(It.IsAny<byte[]>()))
                .Returns("image.png");
            _utilityMock.Setup(x => x.CalculateSalePrice(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(10.00m);
            _utilityMock.Setup(x => x.CalculateItemSubtotal(It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(10.00m);
            _colorServiceMock.Setup(x => x.GetSizeName(It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success("Medium"));
            _colorServiceMock.Setup(x => x.GetColorName(It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success("Blue"));

            // Act
            var result = await _service.GetCartItems(cartId);

          
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Data.cartsVMs.Count);
            Assert.Equal(10.00m, result.Data.CartTotal);
            Assert.Equal(1, result.Data.CartSum);
        }

        [Fact]
        public async Task GetCartItems_EmptyCart_ReturnsEmptyCart()
        {
            await InitializeAsync();

            var cartId = "cart123";
            var cartItems = new List<CartsDTO>();

            _shoppingCartStoreMock.Setup(x => x.GetCartItemsAllData(cartId))
                .ReturnsAsync(cartItems);

            // Act
            var result = await _service.GetCartItems(cartId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Data.cartsVMs);
            Assert.Equal(0m, result.Data.CartTotal);
            Assert.Equal(0, result.Data.CartSum);
        }

        [Fact]
        public async Task CreateOrder_ValidCart_CreatesOrderAndEmptiesCart()
        {
            await InitializeAsync();

            // Arrange
            var cartId = "cart123";
            var customerOrderId = 1;
            var sizeName = "Large";
            var colorName = "Red";

            var cartItems = new ShoppingCartVM
            {
                cartsVMs = new List<CartsVM>
                {
                    new CartsVM { ProductID = 1, Quantity = 2, Price = 10.00m }
                }
            };
            var orderItems = new List<CustomerOrderItem>
            {
                new CustomerOrderItem { ProductID = 1, CustomerOrderID = customerOrderId, Quantity = 2, Price = 10.00m }
            };

            var cartList = new List<CartsDTO> { new CartsDTO { ProductID = 1, Quantity = 2, Price = 10.00m } };

            CartsVM cartVM = new CartsVM { ProductID = 1, Quantity = 3, Price = 10.00m};
            List<CartsVM> cartListVM = new List<CartsVM>();
            cartListVM.Add(cartVM);




            _colorServiceMock.Setup(x => x.GetSizeName(It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(sizeName));
            _colorServiceMock.Setup(x => x.GetColorName(It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(colorName));

            _shoppingCartStoreMock.Setup(x => x.GetCartItemsAllData(cartId))
                .ReturnsAsync(cartList);

            _mapperMock.Setup(x => x.Map<List<CartsVM>>(It.IsAny<List<Carts>>()))
                .Returns(cartItems.cartsVMs);
            _customerOrderItemsStoreMock.Setup(x => x.CustomerOrderItemsAdd(It.IsAny<List<CustomerOrderItem>>()))
                .ReturnsAsync(1);
            _shoppingCartStoreMock.Setup(x => x.CalculateOrderTotal(customerOrderId))
                .ReturnsAsync(20.00m);
            _shoppingCartStoreMock.Setup(x => x.GetCartDataOnlyItems(cartId))
                .ReturnsAsync(new List<Carts> { new Carts() });
            _shoppingCartStoreMock.Setup(x => x.RemoveFromCart(It.IsAny<Carts>()))
                .ReturnsAsync(1);

            _mapperMock.Setup(x => x.Map<List<CartsVM>>(It.IsAny<List<CartsDTO>>()))
           .Returns(cartListVM);

            // Act
            var result = await _service.CreateOrder(cartId, customerOrderId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(20.00m, result.Data);
            _customerOrderItemsStoreMock.Verify(x => x.CustomerOrderItemsAdd(It.IsAny<List<CustomerOrderItem>>()), Times.Once());
            _shoppingCartStoreMock.Verify(x => x.RemoveFromCart(It.IsAny<Carts>()), Times.Once());
        }

        [Fact]
        public async Task CreateOrder_CustomerOrderItemsFail_SendFailure()
        {
            await InitializeAsync();

            // Arrange
            var cartId = "cart123";
            var customerOrderId = 1;
            var sizeName = "Large";
            var colorName = "Red";

            var cartItems = new ShoppingCartVM
            {
                cartsVMs = new List<CartsVM>
                {
                    new CartsVM { ProductID = 1, Quantity = 2, Price = 10.00m }
                }
            };
            var orderItems = new List<CustomerOrderItem>
            {
                new CustomerOrderItem { ProductID = 1, CustomerOrderID = customerOrderId, Quantity = 2, Price = 10.00m }
            };

            var cartList = new List<CartsDTO> { new CartsDTO { ProductID = 1, Quantity = 2, Price = 10.00m } };

            CartsVM cartVM = new CartsVM { ProductID = 1, Quantity = 3, Price = 10.00m };
            List<CartsVM> cartListVM = new List<CartsVM>();
            cartListVM.Add(cartVM);

            _colorServiceMock.Setup(x => x.GetSizeName(It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(sizeName));
            _colorServiceMock.Setup(x => x.GetColorName(It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(colorName));

            _shoppingCartStoreMock.Setup(x => x.GetCartItemsAllData(cartId))
                .ReturnsAsync(cartList);

            _mapperMock.Setup(x => x.Map<List<CartsVM>>(It.IsAny<List<Carts>>()))
                .Returns(cartItems.cartsVMs);
            _customerOrderItemsStoreMock.Setup(x => x.CustomerOrderItemsAdd(It.IsAny<List<CustomerOrderItem>>()))
                .ReturnsAsync(0);

            _mapperMock.Setup(x => x.Map<List<CartsVM>>(It.IsAny<List<CartsDTO>>()))
            .Returns(cartListVM);

            // Act
            var result = await _service.CreateOrder(cartId, customerOrderId);

            // Assert
            Assert.False(result.IsSuccess);
            _customerOrderItemsStoreMock.Verify(x => x.CustomerOrderItemsAdd(It.IsAny<List<CustomerOrderItem>>()), Times.Once());
            _loggerMock.Verify(
              x => x.Log(
                  LogLevel.Error,
                  It.IsAny<EventId>(),
                  It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Cannot add order item. Rolling back transaction.")),
                  It.IsAny<Exception>(),
                  It.IsAny<Func<It.IsAnyType, Exception, string>>()),
              Times.Once);
        }

        [Fact]
        public async Task CreateOrder_CustomerOrderTotalFail_SendFailure()
        {
            await InitializeAsync();
                      
            var cartId = "cart123";
            var customerOrderId = 1;
            var sizeName = "Large";
            var colorName = "Red";

            var cartItems = new ShoppingCartVM
            {
                cartsVMs = new List<CartsVM>
                {
                    new CartsVM { ProductID = 1, Quantity = 2, Price = 10.00m }
                }
            };
            var orderItems = new List<CustomerOrderItem>
            {
                new CustomerOrderItem { ProductID = 1, CustomerOrderID = customerOrderId, Quantity = 2, Price = 10.00m }
            };

            var cartList = new List<CartsDTO> { new CartsDTO { ProductID = 1, Quantity = 2, Price = 10.00m } };

            CartsVM cartVM = new CartsVM { ProductID = 1, Quantity = 3, Price = 10.00m };
            List<CartsVM> cartListVM = new List<CartsVM>();
            cartListVM.Add(cartVM);

            _colorServiceMock.Setup(x => x.GetSizeName(It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(sizeName));
            _colorServiceMock.Setup(x => x.GetColorName(It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(colorName));

            _shoppingCartStoreMock.Setup(x => x.GetCartItemsAllData(cartId))
                .ReturnsAsync(cartList);

            _mapperMock.Setup(x => x.Map<List<CartsVM>>(It.IsAny<List<Carts>>()))
                .Returns(cartItems.cartsVMs);
            _customerOrderItemsStoreMock.Setup(x => x.CustomerOrderItemsAdd(It.IsAny<List<CustomerOrderItem>>()))
                .ReturnsAsync(1);
            _shoppingCartStoreMock.Setup(x => x.CalculateOrderTotal(customerOrderId))
                .ReturnsAsync(0m);

            _mapperMock.Setup(x => x.Map<List<CartsVM>>(It.IsAny<List<CartsDTO>>()))
           .Returns(cartListVM);

            // Act
            var result = await _service.CreateOrder(cartId, customerOrderId);

            // Assert
            Assert.False(result.IsSuccess);
            _customerOrderItemsStoreMock.Verify(x => x.CustomerOrderItemsAdd(It.IsAny<List<CustomerOrderItem>>()), Times.Once());
            _loggerMock.Verify(
              x => x.Log(
                  LogLevel.Error,
                  It.IsAny<EventId>(),
                  It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Cannot calculate order total. Rolling back transaction.")),
                  It.IsAny<Exception>(),
                  It.IsAny<Func<It.IsAnyType, Exception, string>>()),
              Times.Once);
        }
        [Fact]
        public async Task CreateOrder_Exception_SendFailure()
        {
            await InitializeAsync();

            var exception = new Exception("Error" + new Exception());
            var cartId = "cart123";
            var customerOrderId = 1;
            var sizeName = "Large";
            var colorName = "Red";

            var cartItems = new ShoppingCartVM
            {
                cartsVMs = new List<CartsVM>
                {
                    new CartsVM { ProductID = 1, Quantity = 2, Price = 10.00m }
                }
            };
            var orderItems = new List<CustomerOrderItem>
            {
                new CustomerOrderItem { ProductID = 1, CustomerOrderID = customerOrderId, Quantity = 2, Price = 10.00m }
            };

            var cartList = new List<CartsDTO> { new CartsDTO { ProductID = 1, Quantity = 2, Price = 10.00m } };

            CartsVM cartVM = new CartsVM { ProductID = 1, Quantity = 3, Price = 10.00m };
            List<CartsVM> cartListVM = new List<CartsVM>();
            cartListVM.Add(cartVM);

            _shoppingCartStoreMock.Setup(x => x.GetCartItemsAllData(cartId))
                .ThrowsAsync(exception);

            _colorServiceMock.Setup(x => x.GetSizeName(It.IsAny<int>()))
               .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(sizeName));
            _colorServiceMock.Setup(x => x.GetColorName(It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(colorName));

            _shoppingCartStoreMock.Setup(x => x.GetCartItemsAllData(cartId))
                .ReturnsAsync(cartList);

            _mapperMock.Setup(x => x.Map<List<CartsVM>>(It.IsAny<List<Carts>>()))
                .Returns(cartItems.cartsVMs);
            _customerOrderItemsStoreMock.Setup(x => x.CustomerOrderItemsAdd(It.IsAny<List<CustomerOrderItem>>()))
                .ThrowsAsync(exception);
            //_shoppingCartStoreMock.Setup(x => x.CalculateOrderTotal(customerOrderId))
            //    .ReturnsAsync(20.00m);
            //_shoppingCartStoreMock.Setup(x => x.GetCartDataOnlyItems(cartId))
            //    .ReturnsAsync(new List<Carts> { new Carts() });
            // _shoppingCartStoreMock.Setup(x => x.RemoveFromCart(It.IsAny<Carts>()))
            //     .ReturnsAsync(1);

            _mapperMock.Setup(x => x.Map<List<CartsVM>>(It.IsAny<List<CartsDTO>>()))
           .Returns(cartListVM);


            // Act
            var result = await _service.CreateOrder(cartId, customerOrderId);

            // Assert
            Assert.False(result.IsSuccess);
            _loggerMock.Verify(
              x => x.Log(
                  LogLevel.Error,
                  It.IsAny<EventId>(),
                  It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to create order.")),
                  It.IsAny<Exception>(),
                  It.IsAny<Func<It.IsAnyType, Exception, string>>()),
              Times.Once);
        }

        [Fact]
        public async Task CreateOrder_GetCartItemsDBException_SendFailure()
        {
            await InitializeAsync();

            var exception = new DbUpdateException("Database Error" + new Exception());
            var cartId = "cart123";
            var customerOrderId = 1;
            var sizeName = "Large";
            var colorName = "Red";

            var cartItems = new ShoppingCartVM
            {
                cartsVMs = new List<CartsVM>
                {
                    new CartsVM { ProductID = 1, Quantity = 2, Price = 10.00m }
                }
            };
            var orderItems = new List<CustomerOrderItem>
            {
                new CustomerOrderItem { ProductID = 1, CustomerOrderID = customerOrderId, Quantity = 2, Price = 10.00m }
            };

            var cartList = new List<CartsDTO> { new CartsDTO { ProductID = 1, Quantity = 2, Price = 10.00m } };

            CartsVM cartVM = new CartsVM { ProductID = 1, Quantity = 3, Price = 10.00m };
            List<CartsVM> cartListVM = new List<CartsVM>();
            cartListVM.Add(cartVM);

            _colorServiceMock.Setup(x => x.GetSizeName(It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(sizeName));
            _colorServiceMock.Setup(x => x.GetColorName(It.IsAny<int>()))
                .ReturnsAsync(LuckysDepartmentStore.Utilities.ExecutionResult<string>.Success(colorName));

            _shoppingCartStoreMock.Setup(x => x.GetCartItemsAllData(cartId))
                .ReturnsAsync(cartList);

            _mapperMock.Setup(x => x.Map<List<CartsVM>>(It.IsAny<List<Carts>>()))
                .Returns(cartItems.cartsVMs);
            _customerOrderItemsStoreMock.Setup(x => x.CustomerOrderItemsAdd(It.IsAny<List<CustomerOrderItem>>()))
                .ThrowsAsync(exception);
            _shoppingCartStoreMock.Setup(x => x.CalculateOrderTotal(customerOrderId))
                .ReturnsAsync(20.00m);
            _shoppingCartStoreMock.Setup(x => x.GetCartDataOnlyItems(cartId))
                .ReturnsAsync(new List<Carts> { new Carts() });
            _shoppingCartStoreMock.Setup(x => x.RemoveFromCart(It.IsAny<Carts>()))
                .ReturnsAsync(1);

            _mapperMock.Setup(x => x.Map<List<CartsVM>>(It.IsAny<List<CartsDTO>>()))
           .Returns(cartListVM);


            // Act
            var result = await _service.CreateOrder(cartId, customerOrderId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to create order in database.", result.ErrorMessage);
            _loggerMock.Verify(
              x => x.Log(
                  LogLevel.Error,
                  It.IsAny<EventId>(),
                  It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to create order in database.")),
                  It.IsAny<Exception>(),
                  It.IsAny<Func<It.IsAnyType, Exception, string>>()),
              Times.Once);
        }

        [Fact]
        public async Task MigrateCart_ValidUser_MigratesCart()
        {
            await InitializeAsync();

            // Arrange
            var userName = "testuser";
            var cartId = "cart123";
            var cartItems = new List<Carts> { new Carts { ProductID = 1, CartID = cartId } };
            _shoppingCartStoreMock.Setup(x => x.GetCartDataOnlyItems(cartId))
                .ReturnsAsync(cartItems);
            _shoppingCartStoreMock.Setup(x => x.MigrateCart(userName, cartItems))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.MigrateCart(userName, cartId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(cartId, result.Data);
            _shoppingCartStoreMock.Verify(x => x.MigrateCart(userName, cartItems), Times.Once());
        }

        [Fact]
        public async Task MigrateCart_DBException_SendsFailure()
        {
            await InitializeAsync();

            var exception = new DbUpdateException("Database Error" + new Exception());            
            var userName = "testuser";
            var cartId = "cart123";
            var cartItems = new List<Carts> { new Carts { ProductID = 1, CartID = cartId } };

            _shoppingCartStoreMock.Setup(x => x.GetCartDataOnlyItems(cartId))
                .ThrowsAsync(exception);
            _shoppingCartStoreMock.Setup(x => x.MigrateCart(userName, cartItems))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.MigrateCart(userName, cartId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to migrate cart in database.", result.ErrorMessage);
            _loggerMock.Verify(
              x => x.Log(
                  LogLevel.Error,
                  It.IsAny<EventId>(),
                  It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to migrate cart in database.")),
                  It.IsAny<Exception>(),
                  It.IsAny<Func<It.IsAnyType, Exception, string>>()),
              Times.Once);
        }

        [Fact]
        public async Task MigrateCart_Exception_SendsFailure()
        {
            await InitializeAsync();
            var exception = new Exception("Error" + new Exception());

            // Arrange
            var userName = "testuser";
            var cartId = "cart123";
            var cartItems = new List<Carts> { new Carts { ProductID = 1, CartID = cartId } };
            _shoppingCartStoreMock.Setup(x => x.GetCartDataOnlyItems(cartId))
                .ThrowsAsync(exception);
            _shoppingCartStoreMock.Setup(x => x.MigrateCart(userName, cartItems))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.MigrateCart(userName, cartId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to migrate cart.", result.ErrorMessage);
            _loggerMock.Verify(
              x => x.Log(
                  LogLevel.Error,
                  It.IsAny<EventId>(),
                  It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to migrate cart.")),
                  It.IsAny<Exception>(),
                  It.IsAny<Func<It.IsAnyType, Exception, string>>()),
              Times.Once);
        }

        [Fact]
        public async Task GetCartId_NoSession_SetsNewGuid()
        {
            await InitializeAsync();
            // Arrange
            var sessionMock = new Mock<ISession>();
            var userMock = new Mock<ClaimsPrincipal>();
            var identityMock = new Mock<IIdentity>();

            // Setup HttpContext
            _httpContextMock.Setup(x => x.HttpContext.Session).Returns(sessionMock.Object);
            _httpContextMock.Setup(x => x.HttpContext.User).Returns(userMock.Object);

            // Setup User and Identity to return null Name (anonymous user)
            userMock.Setup(x => x.Identity).Returns(identityMock.Object);
            identityMock.Setup(x => x.Name).Returns((string)null);

            // Simulate session key not found initially (mimicking GetString returning null)
            byte[] storedValue = null;
            sessionMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out storedValue))
                .Returns(false);

            // Simulate setting a string in session and update TryGetValue to return the set value
            string capturedKey = null;
            sessionMock.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Callback<string, byte[]>((key, value) =>
                {
                    capturedKey = key;
                    storedValue = value; // Store the value for subsequent TryGetValue calls
                                         // Update TryGetValue to return the stored value
                    sessionMock.Setup(s => s.TryGetValue(key, out storedValue))
                        .Returns(true);
                });

            // Act
            var result = _service.GetCartId();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data); // Ensure Data is not null
            Guid parsedGuid = Guid.Parse(result.Data); // Ensures it's a valid GUID
            Assert.NotNull(capturedKey); // Ensure a key was set
            Assert.Equal(result.Data, Encoding.UTF8.GetString(storedValue)); // Ensure the set value matches
        }

        [Fact]
        public async Task GetCartId_Session_SetsKeyToUserId()
        {
            await InitializeAsync();
            // Arrange
            var sessionMock = new Mock<ISession>();
            var userMock = new Mock<ClaimsPrincipal>();
            var identityMock = new Mock<IIdentity>();
            string userName = "test";

            // Setup HttpContext
            _httpContextMock.Setup(x => x.HttpContext.Session).Returns(sessionMock.Object);
            _httpContextMock.Setup(x => x.HttpContext.User).Returns(userMock.Object);

            // Setup User and Identity to return null Name (anonymous user)
            userMock.Setup(x => x.Identity).Returns(identityMock.Object);
            identityMock.Setup(x => x.Name).Returns(userName);

            // Simulate session key not found initially (mimicking GetString returning null)
            byte[] storedValue = null;
            sessionMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out storedValue))
                .Returns(false);

            // Simulate setting a string in session and update TryGetValue to return the set value
            string capturedKey = null;
            sessionMock.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Callback<string, byte[]>((key, value) =>
                {
                    capturedKey = key;
                    storedValue = value; // Store the value for subsequent TryGetValue calls
                                         // Update TryGetValue to return the stored value
                    sessionMock.Setup(s => s.TryGetValue(key, out storedValue))
                        .Returns(true);
                });

            // Act
            var result = _service.GetCartId();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data); // Ensure Data is not null            
            Assert.NotNull(capturedKey); // Ensure a key was set
            Assert.Equal(result.Data, Encoding.UTF8.GetString(storedValue)); // Ensure the set value matches
        }

        [Fact]
        public async Task GetCartId_NoSessionError_Fails()
        {
            await InitializeAsync();
            // Arrange
            var sessionMock = new Mock<ISession>();
            var userMock = new Mock<ClaimsPrincipal>();
            var identityMock = new Mock<IIdentity>();
            var exception = new Exception("Error" + new Exception());

            // Setup HttpContext
            _httpContextMock.Setup(x => x.HttpContext.Session).Returns(sessionMock.Object);
            _httpContextMock.Setup(x => x.HttpContext.User).Returns(userMock.Object);

            // Setup User and Identity to return null Name (anonymous user)
            userMock.Setup(x => x.Identity).Returns(identityMock.Object);
            identityMock.Setup(x => x.Name).Returns((string)null);

            // Simulate session key not found initially (mimicking GetString returning null)
            byte[] storedValue = null;
            sessionMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out storedValue))
                .Returns(false);

            // Simulate setting a string in session and update TryGetValue to return the set value
            string capturedKey = null;
            sessionMock.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Throws(exception);

            // Act
            var result = _service.GetCartId();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to create cart ID.", result.ErrorMessage);
            _loggerMock.Verify(
              x => x.Log(
                  LogLevel.Error,
                  It.IsAny<EventId>(),
                  It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to create cart ID.")),
                  It.IsAny<Exception>(),
                  It.IsAny<Func<It.IsAnyType, Exception, string>>()),
              Times.Once);
        }

        [Fact]
        public async Task GetCartId_SessionError_Fails()
        {
            await InitializeAsync();
            // Arrange
            var sessionMock = new Mock<ISession>();
            var userMock = new Mock<ClaimsPrincipal>();
            var identityMock = new Mock<IIdentity>();
            var exception = new Exception("Error" + new Exception());
            string userName = "test";

            // Setup HttpContext
            _httpContextMock.Setup(x => x.HttpContext.Session).Returns(sessionMock.Object);
            _httpContextMock.Setup(x => x.HttpContext.User).Returns(userMock.Object);

            // Setup User and Identity to return null Name (anonymous user)
            userMock.Setup(x => x.Identity).Returns(identityMock.Object);
            identityMock.Setup(x => x.Name).Returns(userName);

            // Simulate session key not found initially (mimicking GetString returning null)
            byte[] storedValue = null;
            sessionMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out storedValue))
                .Returns(false);

            // Simulate setting a string in session and update TryGetValue to return the set value
            string capturedKey = null;
            sessionMock.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Throws(exception);

            // Act
            var result = _service.GetCartId();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to create cart ID from userID.", result.ErrorMessage);
            _loggerMock.Verify(
              x => x.Log(
                  LogLevel.Error,
                  It.IsAny<EventId>(),
                  It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unable to create cart ID from userID.")),
                  It.IsAny<Exception>(),
                  It.IsAny<Func<It.IsAnyType, Exception, string>>()),
              Times.Once);
        }

        [Fact]
        public async Task GetCartIdOnLogInAsync_NoExistingCart_SetsUsername()
        {
            await InitializeAsync();
            var sessionMock = new Mock<ISession>();
            var userMock = new Mock<ClaimsPrincipal>();
            var identityMock = new Mock<IIdentity>();
            // Arrange
            var userName = "testUser";
            var cartId = "cart123";
            var cartItems = new List<Carts> { new Carts { ProductID = 1, CartID = cartId } };

            // Setup HttpContext
            _httpContextMock.Setup(x => x.HttpContext.Session).Returns(sessionMock.Object);
            _httpContextMock.Setup(x => x.HttpContext.User).Returns(userMock.Object);

            // Setup User and Identity
            userMock.Setup(x => x.Identity).Returns(identityMock.Object);
            identityMock.Setup(x => x.Name).Returns("testUser");

            // Simulate no existing cart (GetString returns null)
            byte[] storedValue = null;
            sessionMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out storedValue))
                .Returns(false);

            // Simulate setting a string in session
            string capturedKey = null;
            sessionMock.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Callback<string, byte[]>((key, value) =>
                {
                    capturedKey = key;
                    storedValue = value;
                    // Update TryGetValue to return the stored value
                    sessionMock.Setup(s => s.TryGetValue(key, out storedValue))
                        .Returns(true);
                });

            // GetCartId Setup
            // Setup HttpContext
            _httpContextMock.Setup(x => x.HttpContext.Session).Returns(sessionMock.Object);
            _httpContextMock.Setup(x => x.HttpContext.User).Returns(userMock.Object);

            // Setup User and Identity to return null Name (anonymous user)
            userMock.Setup(x => x.Identity).Returns(identityMock.Object);
            identityMock.Setup(x => x.Name).Returns(userName);

            // Act
            var result = await _service.GetCartIdOnLogInAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("testUser", result.Data);
            Assert.NotNull(capturedKey);
            Assert.Equal("testUser", Encoding.UTF8.GetString(storedValue));            
        }

        [Fact]
        public async Task GetCartIdOnLogInAsync_ExistingCart_MigratesCart()
        {
            await InitializeAsync();
            var sessionMock = new Mock<ISession>();
            var userMock = new Mock<ClaimsPrincipal>();
            var identityMock = new Mock<IIdentity>();
            // Arrange
            var userName = "testUser";
            var cartId = "cart123";
            var cartItems = new List<Carts> { new Carts { ProductID = 1, CartID = cartId } };

            // Setup HttpContext
            _httpContextMock.Setup(x => x.HttpContext.Session).Returns(sessionMock.Object);
            _httpContextMock.Setup(x => x.HttpContext.User).Returns(userMock.Object);

            // Setup User and Identity
            userMock.Setup(x => x.Identity).Returns(identityMock.Object);
            identityMock.Setup(x => x.Name).Returns("testUser");

            // Simulate no existing cart (GetString returns null)
            byte[] storedValue = Encoding.UTF8.GetBytes("3f2504e0-4f89-41d3-9a0c-0305e82c3301");
            sessionMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out storedValue))
                .Returns(false);

            // Simulate setting a string in session
            string capturedKey = null;
            sessionMock.Setup(x => x.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Callback<string, byte[]>((key, value) =>
                {
                    capturedKey = key;
                    storedValue = value;
                    // Update TryGetValue to return the stored value
                    sessionMock.Setup(s => s.TryGetValue(key, out storedValue))
                        .Returns(true);
                });

            // GetCartId Setup
            // Setup HttpContext
            _httpContextMock.Setup(x => x.HttpContext.Session).Returns(sessionMock.Object);
            _httpContextMock.Setup(x => x.HttpContext.User).Returns(userMock.Object);

            // Setup User and Identity to return null Name (anonymous user)
            userMock.Setup(x => x.Identity).Returns(identityMock.Object);
            identityMock.Setup(x => x.Name).Returns(userName);

            // Act
            var result = await _service.GetCartIdOnLogInAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("testUser", result.Data);
            Assert.NotNull(capturedKey);
            Assert.Equal("testUser", Encoding.UTF8.GetString(storedValue));
        }
       
    }
}
