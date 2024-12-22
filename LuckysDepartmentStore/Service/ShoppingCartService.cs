using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LuckysDepartmentStore.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        public LuckysContext _context;
        public IMapper _mapper;      
        public const string CartSessionKey = "CartId";        

        public ShoppingCartService(LuckysContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);

        }
        public async Task<Utilities.ExecutionResult<Carts>> AddToCartAsync(Product product, string ShoppingCartId)
        {
            Carts cartItem = null;
            try
            {
                // Get the matching cart and album instances
                cartItem = await _context.Carts.SingleOrDefaultAsync(
                    c => c.CartID == ShoppingCartId
                    && c.ProductID == product.ProductID);

                if (cartItem == null)
                {
                    // Create a new cart item if no cart item exists
                    cartItem = new Carts
                    {
                        ProductID = product.ProductID,
                        CartID = ShoppingCartId,
                        Quantity = product.Quantity,
                        CreatedDate = DateTime.Now
                    };
                    _context.Carts.Add(cartItem);
                }
                else
                {
                    // If the item does exist in the cart, 
                    // then add one to the quantity
                    cartItem.Quantity++;
                }
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return Utilities.ExecutionResult<Carts>.Failure("Unable to add to cart.");
            }

            return Utilities.ExecutionResult<Carts>.Success(cartItem);
        }

    }
}
