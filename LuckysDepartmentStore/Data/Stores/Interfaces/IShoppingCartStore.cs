using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface IShoppingCartStore
    {
        public Task<Carts> CheckForItemInCart(CartItemsDTO product, string ShoppingCartId);

        public Task<int> AddItemToNewCart(Carts product);

        public Task<int> UpdateCartItemQuantity(Carts product, int quantityUpdate);

        //public Task<Carts> GetCart(Product product, string ShoppingCartId);

        public Task<int> RemoveFromCart(Carts cartItem);

        public Task<List<Carts>> GetCartDataOnlyItems(string ShoppingCartId);

        public Task<List<CartsDTO>> GetCartItemsAllData(string ShoppingCartId);

        public Task<int?> GetCartItemCount(string ShoppingCartId);

        public Task<decimal?> GetCartTotal(string ShoppingCartId);

        public Task<decimal> CalculateOrderTotal(int customerOrderId);

        public Task MigrateCart(string userName, List<Carts> shoppingCart);

        public Task<CartsDTO> GetCartItemsAllDataByID(int itemId);

        public Task<bool> RemoveCartItem(int Id);

        public Task<int> EditCartItem(CartItemEdit item);

        public Task<List<Carts>> MigrateAnonymousCartItems(string shoppingCartId, string userId);

    }
}
