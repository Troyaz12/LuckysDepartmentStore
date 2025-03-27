using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Data.Stores
{
    public class ShoppingCartStore : IShoppingCartStore
    {
        public LuckysContext _context;

        public ShoppingCartStore(LuckysContext context)
        {
            _context = context;
        }

        public async Task<int> AddItemToNewCart(Carts product)
        {
            await _context.Carts.AddAsync(product);
            var rowsUpdated = await _context.SaveChangesAsync();

            return rowsUpdated;
        }

        public async Task<Carts> CheckForItemInCart(CartItemsDTO product, string ShoppingCartId)
        {
            var cartItem = await _context.Carts.SingleOrDefaultAsync(
                  c => c.CartID == ShoppingCartId
                  && c.ProductID == product.ProductID
                  && c.Color == product.ColorSelection);

            return cartItem;
        }

        //public async Task<Carts> GetCart(Product product, string ShoppingCartId)
        //{
        //    // Get the cart
        //    var cartItem = await _context.Carts.SingleAsync(
        //        cart => cart.CartID == ShoppingCartId
        //        && cart.ID == product.ProductID);

        //    return cartItem;
        //}

        public Task<int> RemoveFromCart(Carts cartItem)
        {
            _context.Carts.Remove(cartItem);

            var rowsAffected = _context.SaveChangesAsync();


            return rowsAffected;
        }

        public async Task<int> UpdateCartItemQuantity(Carts product, int quantityUpdate)
        {
            product.Quantity = quantityUpdate;

            // Save changes
            var rowsUpdated = await _context.SaveChangesAsync();

            return rowsUpdated;
        }

        public async Task<List<Carts>> GetCartDataOnlyItems(string ShoppingCartId)
        {
            // Get the cart
            var cartItems = await _context.Carts
                    .Where(cart => cart.CartID == ShoppingCartId)
                    .ToListAsync();

            return cartItems;
        }

        public async Task<List<CartsDTO>> GetCartItemsAllData(string ShoppingCartId)
        {
            var cartItems = await(
                    from cart in _context.Carts
                    join product in _context.Products on cart.ProductID equals product.ProductID
                    join category in _context.Categories on product.CategoryID equals category.CategoryID into categories
                    from category in categories.DefaultIfEmpty()
                    join subCategory in _context.SubCategories on product.SubCategoryID equals subCategory.SubCategoryID into subCategories
                    from subCategory in subCategories.DefaultIfEmpty()
                    join brand in _context.Brand on product.BrandID equals brand.BrandId into brands
                    from brand in brands.DefaultIfEmpty()
                    join productDiscount in _context.Discounts on product.ProductID equals productDiscount.ProductID into productDiscounts
                    from productDiscount in productDiscounts.Where(d => d.DiscountActive).DefaultIfEmpty()
                    join brandDiscount in _context.Discounts on product.BrandID equals brandDiscount.BrandID into brandDiscounts
                    from brandDiscount in brandDiscounts.Where(d => d.DiscountActive).DefaultIfEmpty()
                    join categoryDiscount in _context.Discounts on product.CategoryID equals categoryDiscount.CategoryID into categoryDiscounts
                    from categoryDiscount in categoryDiscounts.Where(d => d.DiscountActive).DefaultIfEmpty()
                    join subCategoryDiscount in _context.Discounts on product.SubCategoryID equals subCategoryDiscount.SubCategoryID into subCategoryDiscounts
                    from subCategoryDiscount in subCategoryDiscounts.Where(d => d.DiscountActive).DefaultIfEmpty()
                    join tagDiscount in _context.Discounts on product.DiscountTag equals tagDiscount.DiscountTag into tagDiscounts
                    from tagDiscount in tagDiscounts.Where(d => d.DiscountActive).DefaultIfEmpty()
                    where cart.CartID == ShoppingCartId
                    group new { product, category, subCategory, brand, productDiscount, brandDiscount, categoryDiscount, subCategoryDiscount, tagDiscount }
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
                            cart.Color,
                            cart.ID
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
                        DiscountAmount = grouped.Sum(x => x.productDiscount != null ? x.productDiscount.DiscountAmount : 0m) +
                                 grouped.Sum(x => x.brandDiscount != null ? x.brandDiscount.DiscountAmount : 0m) +
                                 grouped.Sum(x => x.categoryDiscount != null ? x.categoryDiscount.DiscountAmount : 0m) +
                                 grouped.Sum(x => x.subCategoryDiscount != null ? x.subCategoryDiscount.DiscountAmount : 0m) +
                                 grouped.Sum(x => x.tagDiscount != null ? x.tagDiscount.DiscountAmount : 0m),
                        DiscountPercent = grouped.Sum(x => x.productDiscount != null ? x.productDiscount.DiscountPercent : 0m) +
                                  grouped.Sum(x => x.brandDiscount != null ? x.brandDiscount.DiscountPercent : 0m) +
                                  grouped.Sum(x => x.categoryDiscount != null ? x.categoryDiscount.DiscountPercent : 0m) +
                                  grouped.Sum(x => x.subCategoryDiscount != null ? x.subCategoryDiscount.DiscountPercent : 0m) +
                                  grouped.Sum(x => x.tagDiscount != null ? x.tagDiscount.DiscountPercent : 0m),
                        DiscountTag = grouped.Key.DiscountTag,
                        Size = grouped.Key.Size,
                        Color = grouped.Key.Color,
                        ID = grouped.Key.ID
                    }).ToListAsync();

            return cartItems;
        }
        public async Task<int?> GetCartItemCount(string ShoppingCartId)
        {
            // Get the count of each item in the cart and sum them up
            var count = (from cartItems in _context.Carts
                     where cartItems.CartID == ShoppingCartId
                     select (int?)cartItems.Quantity).Sum();

            return count;
        }

        public async Task<decimal?> GetCartTotal(string ShoppingCartId)
        {
            var total = await _context.Carts
                                .Where(cartItems => cartItems.CartID == ShoppingCartId)
                                .Select(cartItems => (int?)cartItems.Quantity * cartItems.Price)
                                .SumAsync();

            return total;
        }

        public async Task<decimal> CalculateOrderTotal(int customerOrderId)
        {
            var orderTotal = await _context.CustomerOrderItems
                   .Where(oi => oi.CustomerOrderID == customerOrderId)
                   .SumAsync(oi => oi.Price * oi.Quantity);

            return orderTotal;
        }

        public async Task MigrateCart(string userName, List<Carts> shoppingCart)
        {
            foreach (Carts item in shoppingCart)
            {
                item.CartID = userName;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<CartsDTO> GetCartItemsAllDataByID(int itemId)
        {
            var cartItems = await(
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
                   where cart.ID == itemId
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
                       DiscountAmount = grouped.Sum(x => x.discount.DiscountAmount != null ? x.discount.DiscountAmount : 0),
                       DiscountPercent = grouped.Sum(x => x.discount.DiscountPercent != null ? x.discount.DiscountPercent : 0),
                       DiscountTag = grouped.Key.DiscountTag,
                       Size = grouped.Key.Size,
                       Color = grouped.Key.Color
                   }).FirstOrDefaultAsync();

            return cartItems;
        }

        public async Task<bool> RemoveCartItem(int Id) // Adjust Id type as needed
        {
            var cartItem = await _context.Carts.FindAsync(Id);
            if (cartItem == null)
            {
                return false;
            }

            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> EditCartItem(CartItemEdit item)
        {
            var cartItemResult = await _context.Carts.FindAsync(item.ID);

            if (cartItemResult == null)
            {
                return 0;
            }

            cartItemResult.Quantity = item.Quantity;
            cartItemResult.Color = item.ColorSelection;
            cartItemResult.Size = item.SizeSelection;
            cartItemResult.Price = item.Price;

            var cartItemSave = await _context.SaveChangesAsync();


            return cartItemSave;
        }
    }
}
