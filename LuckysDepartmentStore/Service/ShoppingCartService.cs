using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly LuckysContext _context;
        private readonly IMapper _mapper;
        public const string CartSessionKey = "CartId";
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUtility _utility;
        private readonly IColorService _colorService;
        private readonly IShoppingCartStore _shoppingCartStore;
        private readonly ICustomerOrderItemsStore _customerOrderItemsStore;
        private readonly ILogger _logger;

        public ShoppingCartService(LuckysContext context, IMapper mapper, IHttpContextAccessor httpContext,
            IUtility utility, IColorService color, IShoppingCartStore shoppingCartStore, ICustomerOrderItemsStore customerOrderItemsStore, ILogger<ShoppingCartService> logger)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
            _utility = utility;
            _colorService = color;
            _shoppingCartStore = shoppingCartStore;
            _customerOrderItemsStore = customerOrderItemsStore;
            _logger = logger;
        }

        public string GetCart()
        {
            //var cart = new ShoppingCart();
            var shoppingCartId = GetCartId();
            return shoppingCartId;
        }
        // Helper method to simplify shopping cart calls
        public string GetCart(Controller controller)
        {
            return GetCart();

        }
        public async Task<ExecutionResult<CartsVM>> AddToCartAsync(CartItemsDTO product, string ShoppingCartId)
        {
            try
            {
                // Get the matching cart instances
                var cartItem = await _shoppingCartStore.CheckForItemInCart(product, ShoppingCartId);
                int rowsUpdated = 0;

                if (cartItem == null)
                {
                    product.ProductPicture = _utility.StringToBytes(product.ProductImage);

                    // Create a new cart item if no cart item exists
                    cartItem = new Carts
                    {
                        ProductID = product.ProductID,
                        CartID = ShoppingCartId,
                        Quantity = product.Quantity,
                        Price = product.Price,
                        CreatedDate = DateTime.Now,
                        ProductPicture = product.ProductPicture,
                        ProductName = product.ProductName,
                        Color = product.ColorSelection,
                        Size = product.SizeSelection
                    };

                    await _shoppingCartStore.AddItemToNewCart(cartItem);
                }
                else
                {
                    // If the item does exist in the cart, 
                    // then add one to the quantity
                    var newQuantity = cartItem.Quantity + product.Quantity;

                    rowsUpdated = await _shoppingCartStore.UpdateCartItemQuantity(cartItem, newQuantity);

                    if (rowsUpdated == 0)
                    {
                        return ExecutionResult<CartsVM>.Failure("Unable to update cart.");
                    }
                }               

                var cartVM = _mapper.Map<CartsVM>(cartItem);

                return ExecutionResult<CartsVM>.Success(cartVM);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Unable to add to cart in database.");
                return ExecutionResult<CartsVM>.Failure("Unable to add to cart.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to add to cart.");
                return ExecutionResult<CartsVM>.Failure("Unable to add to cart.");
            }
        }
        public async Task<ExecutionResult<int>> EmptyCart(string ShoppingCartId)
        {
            List<Carts> cartItems = null;

            try
            {
                cartItems = await _shoppingCartStore.GetCartDataOnlyItems(ShoppingCartId);

                if (cartItems == null || !cartItems.Any())
                {
                    return ExecutionResult<int>.Failure("Unable to get cart items.");
                }

                foreach (var cartItem in cartItems)
                {
                    await _shoppingCartStore.RemoveFromCart(cartItem);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to add to cart.");
                return ExecutionResult<int>.Failure("Unable to remove cart item.");
            }

            return ExecutionResult<int>.Success(cartItems.Count());
        }
        public async Task<ExecutionResult<ShoppingCartVM>> GetCartItems(string ShoppingCartId)
        {
            try
            {
                var shoppingCart = new ShoppingCartVM();

                var cartItems = await _shoppingCartStore.GetCartItemsAllData(ShoppingCartId);

                if (cartItems == null)
                {
                    return ExecutionResult<ShoppingCartVM>.Failure("Unable to get cart items.");
                }
                else if (!cartItems.Any())
                {
                    ExecutionResult<ShoppingCartVM>.Success(shoppingCart);
                }

                var cartVM = _mapper.Map<List<CartsVM>>(cartItems);

                for (int x = 0; x < cartVM.Count; x++)
                {
                    cartVM[x].ProductImage = _utility.BytesToImage(cartVM[x].ProductPicture);
                    cartVM[x].SalePrice = _utility.CalculateSalePrice(cartVM[x].DiscountAmount, cartVM[x].DiscountPercent, cartVM[x].Price);
                    cartVM[x].Subtotal = _utility.CalculateItemSubtotal(cartVM[x].Quantity, cartVM[x].SalePrice);
                    var sizeName = await _colorService.GetSizeName(cartVM[x].Size);
                    cartVM[x].SizeString = sizeName.Data;

                    var colorName = await _colorService.GetColorName(cartVM[x].Color);
                    cartVM[x].ColorString = colorName.Data;

                    shoppingCart.CartTotal += cartVM[x].Subtotal;
                    shoppingCart.CartSum += cartVM[x].Quantity;
                    shoppingCart.cartsVMs.Add(cartVM[x]);
                }

                return ExecutionResult<ShoppingCartVM>.Success(shoppingCart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get cart items.");
                return ExecutionResult<ShoppingCartVM>.Failure("Unable to get cart items.");
            }
        }
        public async Task<ExecutionResult<int>> GetCount(string ShoppingCartId)
        {
            int? count = null;
            try
            {
                // Get the count of each item in the cart and sum them up
                count = await _shoppingCartStore.GetCartItemCount(ShoppingCartId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get cart items.");
                return ExecutionResult<int>.Failure("Unable to get cart items.");
            }

            return ExecutionResult<int>.Success(count ?? 0);
        }
        public async Task<ExecutionResult<decimal>> GetTotal(string ShoppingCartId)
        {
            decimal? total = null;
            try
            {                
                total = await _shoppingCartStore.GetCartTotal(ShoppingCartId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to get cart total.");
                return ExecutionResult<decimal>.Failure("Unable to get cart total.");
            }

            return ExecutionResult<decimal>.Success(total ?? decimal.Zero);
        }
        public async Task<ExecutionResult<decimal>> CreateOrder(string ShoppingCartId, int customerOrderId)
        { // may not need Product

            decimal orderTotal = 0;

            // Start a transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cartItems = GetCartItems(ShoppingCartId);
                List<CustomerOrderItem> items = new List<CustomerOrderItem>();

                // Iterate over the items in the cart, 
                // adding the order details for each
                foreach (var item in cartItems.Result.Data.cartsVMs)
                {
                    var customerOrderItem = new CustomerOrderItem
                    {
                        ProductID = item.ProductID,
                        CustomerOrderID = customerOrderId,
                        Price = item.Price,
                        Quantity = item.Quantity

                    };

                    items.Add(customerOrderItem);
                }

                // Save the order
                var rowsEffected = await _customerOrderItemsStore.CustomerOrderItemsAdd(items);

                if (rowsEffected == 0)
                {
                    await transaction.RollbackAsync();
                    return Utilities.ExecutionResult<decimal>.Failure("Unable to process order.");
                }

                orderTotal = await _shoppingCartStore.CalculateOrderTotal(customerOrderId);

                if (orderTotal == 0)
                {
                    await transaction.RollbackAsync();
                    return Utilities.ExecutionResult<decimal>.Failure("Unable to process order.");
                }

                // Empty the shopping cart
                EmptyCart(ShoppingCartId);

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Unable to create order in database.");
                await transaction.RollbackAsync();
                return Utilities.ExecutionResult<decimal>.Failure("Unable to create order in database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to create order.");
                await transaction.RollbackAsync();
                return Utilities.ExecutionResult<decimal>.Failure("Unable to create order.");
            }

            // Return the CustomerOrderID as the confirmation number
            return Utilities.ExecutionResult<decimal>.Success(orderTotal);
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public async Task<Utilities.ExecutionResult<string>> MigrateCart(string userName, string ShoppingCartId)
        {
            try
            {
                var shoppingCart = await _shoppingCartStore.GetCartDataOnlyItems(ShoppingCartId);

                await _shoppingCartStore.MigrateCart(userName, shoppingCart);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Unable to migrate cart in database.");
                return ExecutionResult<string>.Failure("Unable to migrate cart in database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to migrate cart.");
                return Utilities.ExecutionResult<string>.Failure("Unable to migrate cart.");
            }            

            return Utilities.ExecutionResult<string>.Success(ShoppingCartId);
        }
        
        public string GetCartId()
        {

            if (_httpContext.HttpContext.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(_httpContext.HttpContext.User.Identity.Name))
                {
                    try
                    {
                        _httpContext.HttpContext.Session.SetString(CartSessionKey, _httpContext.HttpContext.User.Identity.Name);
                    }
                    catch (Exception ex)
                    {

                    }

                }
                else
                {
                    try
                    {
                        // Generate a new random GUID using System.Guid class
                        Guid tempCartId = Guid.NewGuid();
                        // Send tempCartId back to client as a cookie
                        _httpContext.HttpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            return _httpContext.HttpContext.Session.GetString(CartSessionKey);
        }
        
        public async Task<string> GetCartIdOnLogInAsync()
        {
            var username = _httpContext.HttpContext.User.Identity.Name;
            // does the existing id have any items associated with it? pull items
            if (_httpContext.HttpContext.Session.GetString(CartSessionKey) != null && !string.IsNullOrWhiteSpace(username))
            {
                var cart = GetCart();
                // pull items from logged in id
                var migrateResult = await MigrateAnonymousCartItems(cart);
            }

            _httpContext.HttpContext.Session.SetString(CartSessionKey, _httpContext.HttpContext.User.Identity.Name);

            return _httpContext.HttpContext.Session.GetString(CartSessionKey);
        }

        public async Task<ExecutionResult<int>> GetCartCount(string ShoppingCartId)
        {
            var cartItems = GetCartItems(ShoppingCartId);

            int quantity = cartItems.Result.Data.CartSum;

            return ExecutionResult<int>.Success(quantity);
        }

        public async Task<ExecutionResult<CartsVM>> GetCartItem(int itemId)
        {
            try
            {
                var cartItems = await _shoppingCartStore.GetCartItemsAllDataByID(itemId);

                if (cartItems == null)
                {
                    return ExecutionResult<CartsVM>.Failure("Unable to get cart item.");
                }

                var cartVM = _mapper.Map<CartsVM>(cartItems);

                cartVM.ProductImage = _utility.BytesToImage(cartVM.ProductPicture);
                cartVM.SalePrice = _utility.CalculateSalePrice(cartVM.DiscountAmount, cartVM.DiscountPercent, cartVM.Price);

                var sizeName = await _colorService.GetSizeName(cartVM.Size);
                cartVM.SizeString = sizeName.Data;

                var colorName = await _colorService.GetColorName(cartVM.Color);
                cartVM.ColorString = colorName.Data;


                return ExecutionResult<CartsVM>.Success(cartVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get cart item {@itemId}", itemId);
                return ExecutionResult<CartsVM>.Failure("Unable to get cart items.");
            }
        }
        public async Task<ExecutionResult<bool>> RemoveItemFromCart(int Id)
        {
            try
            {
                var itemRemoved = await _shoppingCartStore.RemoveCartItem(Id);

                if (itemRemoved == null || itemRemoved == false)
                {
                    return ExecutionResult<bool>.Failure("Unable to remove item.");
                }

                return ExecutionResult<bool>.Success(itemRemoved);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to remove cart item {@Id} in database.", Id);
                return ExecutionResult<bool>.Failure("Unable to remove item in database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to remove cart item {@Id}", Id);
                return ExecutionResult<bool>.Failure("Unable to remove item.");
            }
        }

        public async Task<ExecutionResult<int>> EditItemInCart(CartItemEdit cartItem)
        {
            try
            {
                var cartItemResult = await _shoppingCartStore.EditCartItem(cartItem);

                if (cartItemResult == 0)
                {
                    return ExecutionResult<int>.Failure("Unable to edit item in cart.");
                }

                return ExecutionResult<int>.Success(cartItemResult);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Unable to edit item in cart. Cart Item: {@cartItem}", cartItem);
                return ExecutionResult<int>.Failure("Unable to edit item in database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to edit item in cart. Cart Item: {@cartItem}", cartItem);
                return ExecutionResult<int>.Failure("Unable to edit item in database.");
            }
        }

        public async Task<ExecutionResult<List<Carts>>> MigrateAnonymousCartItems(string anonymousCartId)
        {
            try
            {
                var userId = _httpContext.HttpContext.User.Identity.Name;

                if (string.IsNullOrEmpty(userId))
                {
                    return ExecutionResult<List<Carts>>.Failure("User not authenticated.");
                }

                if (string.IsNullOrEmpty(anonymousCartId))
                {
                    return ExecutionResult<List<Carts>>.Failure("Invalid anonymous cart ID.");
                }

                // Retrieve existing cart items from the database
                var cartItems = await _shoppingCartStore.MigrateAnonymousCartItems(anonymousCartId, userId);

                return ExecutionResult<List<Carts>>.Success(cartItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable migrate cart. Id: {@anonymousCartId}", anonymousCartId);
                return ExecutionResult<List<Carts>>.Failure("Unable migrate cart.");
            }
        }
        public ExecutionResult<Guid> SetCartSessionKey()
        {
            try
            {
                Guid tempCartId = Guid.NewGuid();
                // Send tempCartId back to client as a cookie
                _httpContext.HttpContext.Session.SetString(CartSessionKey, tempCartId.ToString());

                return ExecutionResult<Guid>.Success(tempCartId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable set cart session key.");
                return ExecutionResult<Guid>.Failure("Unable set cart session key.");
            }
            
        }
    }
}