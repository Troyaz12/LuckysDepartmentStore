using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Service
{
    public interface IShoppingCartService
    {
        public Task<ExecutionResult<Carts>> AddToCartAsync(ItemVM product, string ShoppingCartId);

        public Task<ExecutionResult<int>> RemoveFromCart(Product product, string ShoppingCartId);

        public Task<ExecutionResult<int>> EmptyCart(string ShoppingCartId);

        public Task<ExecutionResult<List<Carts>>> GetCartItems(string ShoppingCartId);

        public Task<ExecutionResult<int>> GetCount(string ShoppingCartId);

        public Task<Utilities.ExecutionResult<decimal>> GetTotal(string ShoppingCartId);

        public Task<Utilities.ExecutionResult<decimal>>  CreateOrder(Product order, string ShoppingCartId, int customerId);

        public Task<Utilities.ExecutionResult<string>> MigrateCart(string userName, string ShoppingCartId);

        public string GetCart();
        public string GetCart(Controller controller);

        public Task<ExecutionResult<int>> GetCartCount(string ShoppingCartId);
       
    }
}
