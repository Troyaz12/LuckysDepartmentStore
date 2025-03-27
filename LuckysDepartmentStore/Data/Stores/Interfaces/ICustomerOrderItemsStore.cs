using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface ICustomerOrderItemsStore
    {
        public Task<int> CustomerOrderItemsAdd(List<CustomerOrderItem> items);
    }
}
