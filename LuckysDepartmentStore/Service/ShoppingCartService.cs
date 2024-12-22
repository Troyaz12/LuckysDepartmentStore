using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckysDepartmentStore.Service
{
    public class ShoppingCartService :IShoppingCartService
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
        public void AddToCart(Product product)
        {
            // Get the matching cart and album instances
            var cartItem = _context.Cart.SingleOrDefault(
                c => c. == ShoppingCartId
                && c.AlbumId == album.AlbumId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new CustomerOrderItem
                {
                    AlbumId = album.AlbumId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                storeDB.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            storeDB.SaveChanges();
        }

    }
}
