using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service
{
    public interface IShoppingCartService
    {
        public Task<ExecutionResult<Carts>> AddToCartAsync(Product product, string ShoppingCartId);


    }
}
