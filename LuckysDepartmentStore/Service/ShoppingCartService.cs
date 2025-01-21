using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Home;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        public const string CartSessionKey = "CartId";    
        private readonly IHttpContextAccessor _httpContext;
        public Utility _utility;

    public ShoppingCartService(LuckysContext context, IMapper mapper, IHttpContextAccessor httpContext, Utility utility)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
            _utility = utility;
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
        public async Task<Utilities.ExecutionResult<CartsVM>> AddToCartAsync(ItemVM product, string ShoppingCartId)
        {            
            try
            {
                // Get the matching cart instances
                var cartItem = await _context.Carts.SingleOrDefaultAsync(
                    c => c.CartID == ShoppingCartId
                    && c.ProductID == product.ProductID);

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
                    _context.Carts.Add(cartItem);
                }
                else
                {
                    // If the item does exist in the cart, 
                    // then add one to the quantity
                    cartItem.Quantity += cartItem.Quantity;
                }
                // Save changes
                await _context.SaveChangesAsync();

                var cartVM = _mapper.Map<CartsVM>(cartItem);

                return Utilities.ExecutionResult<CartsVM>.Success(cartVM);
            }
            catch(Exception ex)
            {
                return Utilities.ExecutionResult<CartsVM>.Failure("Unable to add to cart.");
            }

            
        }

        public async Task<Utilities.ExecutionResult<int>> RemoveFromCart(Product product, string ShoppingCartId)
        {
            int itemCount = 0;

            try
            {
                // Get the cart
                var cartItem = _context.Carts.Single(
                    cart => cart.CartID == ShoppingCartId
                    && cart.ID == product.ProductID);                

                if (cartItem != null)
                {
                    if (cartItem.Quantity > 1)
                    {
                        cartItem.Quantity--;
                        itemCount = cartItem.Quantity;
                    }
                    else
                    {
                        _context.Carts.Remove(cartItem);
                    }
                    // Save changes
                    _context.SaveChangesAsync();
                }
                else
                {
                    return Utilities.ExecutionResult<int>.Failure("Unable to get cart items.");
                }

            }
            catch (Exception ex)
            {
                return Utilities.ExecutionResult<int>.Failure("Unable to remove cart item.");
            }

            return Utilities.ExecutionResult<int>.Success(itemCount);
        }
        public async Task<Utilities.ExecutionResult<int>> EmptyCart(string ShoppingCartId)
        {
            List<Carts> cartItems = null;

            try
            {
                cartItems = await _context.Carts
                    .Where(cart => cart.CartID == ShoppingCartId)
                    .ToListAsync();

                if (cartItems == null || !cartItems.Any())
                {
                    return Utilities.ExecutionResult<int>.Failure("Unable to get cart items.");
                }

                foreach (var cartItem in cartItems)
                {
                    _context.Carts.Remove(cartItem);
                }
                // Save changes
                _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return Utilities.ExecutionResult<int>.Failure("Unable to remove cart item.");
            }

                return Utilities.ExecutionResult<int>.Success(cartItems.Count());
        }
        public async Task<Utilities.ExecutionResult<List<CartsVM>>> GetCartItems(string ShoppingCartId)
        {
            try
            {
                var cartItems =
                    from cart in _context.Carts
                    join product in _context.Products on cart.ProductID equals product.ProductID
                    join category in _context.Categories on product.CategoryID equals category.CategoryID into categories
                    from category in categories.DefaultIfEmpty()
                    join subCategory in _context.SubCategories on product.SubCategoryID equals subCategory.SubCategoryID into subCategories
                    from subCategory in subCategories.DefaultIfEmpty()
                    join brand in _context.Brand on product.BrandID equals brand.BrandId into brands
                    from brand in brands.DefaultIfEmpty()
                    join discount in _context.Discounts on
                        product.ProductID equals discount.ProductID into discounts
                    from discount in discounts.Where(d => d.DiscountActive &&
                        (d.ProductID == product.ProductID ||
                         d.BrandID == product.BrandID ||
                         d.CategoryID == product.CategoryID ||
                         d.SubCategoryID == product.SubCategoryID)).DefaultIfEmpty()
                    where cart.CartID == ShoppingCartId
                    group new { product, category, subCategory, brand, discount }
                        by new
                        {
                            product.ProductID,
                            product.ProductName,
                            product.Price,
                            product.Description,
                            cart.Quantity,
                            product.ProductPicture,
                            category.CategoryName,
                            subCategory.SubCategoryName,
                            brand.BrandName,
                            product.DiscountTag,
                            cart.Size,
                            cart.Color
                        }
                    into grouped
                    select new CartsDTO
                    {
                        ProductID = grouped.Key.ProductID,
                        ProductName = grouped.Key.ProductName,
                        Price = grouped.Key.Price,
                        Description = grouped.Key.Description,
                        Quantity = grouped.Key.Quantity,
                        ProductPicture = grouped.Key.ProductPicture,
                        Category = grouped.Key.CategoryName,
                        SubCategory = grouped.Key.SubCategoryName,
                        Brand = grouped.Key.BrandName,
                        DiscountAmount = grouped.Sum(x => x.discount.DiscountAmount),
                        DiscountPercent = grouped.Sum(x => x.discount.DiscountPercent),
                        DiscountTag = grouped.Key.DiscountTag,
                        Size = grouped.Key.Size,
                        Color = grouped.Key.Color
                    };




                if (cartItems == null || !cartItems.Any())
                {
                    return Utilities.ExecutionResult<List<CartsVM>>.Failure("Unable to get cart items.");
                }

                var cartVM = _mapper.Map<List<CartsVM>>(cartItems);

                for (int x=0; x < cartVM.Count; x++)
                {
                    cartVM[x].ProductImage = _utility.BytesToImage(cartVM[x].ProductPicture);
                    cartVM[x].SalePrice = _utility.CalculateSalePrice(cartVM[x].DiscountAmount, cartVM[x].DiscountPercent, cartVM[x].Price);
                }

                return Utilities.ExecutionResult<List<CartsVM>>.Success(cartVM);
            }
            catch (Exception ex)
            {
                return Utilities.ExecutionResult<List<CartsVM>>.Failure("Unable to get cart items.");
            }           
        }
        public async Task<Utilities.ExecutionResult<int>> GetCount(string ShoppingCartId)
        {
            int? count = null;
            try
            {
                // Get the count of each item in the cart and sum them up
                count = (from cartItems in _context.Carts
                              where cartItems.CartID == ShoppingCartId
                              select (int?)cartItems.Quantity).Sum();
                
              
            }
            catch (Exception ex)
            {
                return Utilities.ExecutionResult<int>.Failure("Unable to get cart items.");
            }

            return Utilities.ExecutionResult<int>.Success(count ?? 0);
        }
        public async Task<Utilities.ExecutionResult<decimal>> GetTotal(string ShoppingCartId)
        {
            decimal? total = null;
            try
            {
                // Multiply album price by count of that album to get 
                // the current price for each of those albums in the cart
                // sum all album price totals to get the cart total
                total = await _context.Carts
                                  .Where(cartItems => cartItems.CartID == ShoppingCartId)
                                  .Select(cartItems => (int?)cartItems.Quantity * cartItems.Price)
                                  .SumAsync();                
            }
            catch (Exception ex)
            {
                return Utilities.ExecutionResult<decimal>.Failure("Unable to get cart total.");
            }

            return Utilities.ExecutionResult<decimal>.Success(total ?? decimal.Zero);
        }
        public async Task<Utilities.ExecutionResult<decimal>> CreateOrder(Product order, string ShoppingCartId, int customerOrderId)
        { // may not need Product

            decimal orderTotal = 0;

            try
            {
                var cartItems = GetCartItems(ShoppingCartId);
                // Iterate over the items in the cart, 
                // adding the order details for each
                foreach (var item in cartItems.Result.Data)
                {
                    var customerOrderItem = new CustomerOrderItem
                    {
                        ProductID = item.ProductID,
                        CustomerOrderID = customerOrderId,
                        Price = item.Price,
                        Quantity = item.Quantity

                    };
                    // Set the order total of the shopping cart
                //    orderTotal += (item.Quantity * item.Price);

                    _context.CustomerOrderItems.Add(customerOrderItem);

                }                

                // Save the order
                await _context.SaveChangesAsync();

                orderTotal = await _context.CustomerOrderItems
                    .Where(oi => oi.CustomerOrderID == customerOrderId)
                    .SumAsync(oi => oi.Price * oi.Quantity);



                
                // Empty the shopping cart
                EmptyCart(ShoppingCartId);

            }
            catch (Exception ex) 
            {
                return Utilities.ExecutionResult<decimal>.Failure("Unable to get cart total.");
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
                var shoppingCart = _context.Carts.Where(
                    c => c.CartID == ShoppingCartId);

                foreach (Carts item in shoppingCart)
                {
                    item.CartID = userName;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Utilities.ExecutionResult<string>.Failure("Unable migrate cart.");
            }

            return Utilities.ExecutionResult<string>.Success(ShoppingCartId);
        }
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId()
        {
            
            if (_httpContext.HttpContext.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(_httpContext.HttpContext.User.Identity.Name))
                {
                    _httpContext.HttpContext.Session.SetString(CartSessionKey, _httpContext.HttpContext.User.Identity.Name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    _httpContext.HttpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return _httpContext.HttpContext.Session.GetString(CartSessionKey);
        }

        public async Task<ExecutionResult<int>> GetCartCount(string ShoppingCartId)
        {
            var cartItems = GetCartItems(ShoppingCartId);

            int quantity = cartItems.Result.Data.Sum(item => item.Quantity);

            return ExecutionResult<int>.Success(quantity);
        }       
    }
}