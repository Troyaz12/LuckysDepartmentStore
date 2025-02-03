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

        public Task<ExecutionResult<int>> RemoveFromCart(Product product, string ShoppingCartId);

        public Task<ExecutionResult<int>> EmptyCart(string ShoppingCartId);

        public Task<ExecutionResult<List<CartsVM>>> GetCartItems(string ShoppingCartId);

        public Task<ExecutionResult<int>> GetCount(string ShoppingCartId);

        public Task<ExecutionResult<decimal>> GetTotal(string ShoppingCartId);

        public Task<ExecutionResult<decimal>> CreateOrder(Product order, string ShoppingCartId, int customerId);

        public Task<ExecutionResult<string>> MigrateCart(string userName, string ShoppingCartId);

        public string GetCart();
        public string GetCart(Controller controller);

        public Task<ExecutionResult<int>> GetCartCount(string ShoppingCartId);

    }
}
