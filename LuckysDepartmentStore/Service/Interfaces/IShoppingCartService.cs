using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface IShoppingCartService
    {
        public Task<ExecutionResult<CartsVM>> AddToCartAsync(CartItemsDTO product, string ShoppingCartId);

        public Task<ExecutionResult<int>> EmptyCart(string ShoppingCartId);

        public Task<ExecutionResult<ShoppingCartVM>> GetCartItems(string ShoppingCartId);

        public Task<ExecutionResult<int>> GetCount(string ShoppingCartId);

        public Task<ExecutionResult<decimal>> GetTotal(string ShoppingCartId);

        public Task<ExecutionResult<decimal>> CreateOrder(string ShoppingCartId, int customerId);

        public Task<ExecutionResult<string>> MigrateCart(string userName, string ShoppingCartId);

        public string GetCart();

        public string GetCart(Controller controller);

        public Task<ExecutionResult<int>> GetCartCount(string ShoppingCartId);

        public Task<ExecutionResult<CartsVM>> GetCartItem(int itemId);

        public Task<ExecutionResult<bool>> RemoveItemFromCart(int Id);

        public Task<ExecutionResult<int>> EditItemInCart(CartItemEdit cartItem);
        
        public Task<string> GetCartIdOnLogInAsync();

        public ExecutionResult<Guid> SetCartSessionKey();

        public Task<ExecutionResult<List<Carts>>> MigrateAnonymousCartItems(string shoppingCartId);
    }
}
