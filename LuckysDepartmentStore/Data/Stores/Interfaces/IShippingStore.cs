using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface IShippingStore
    {
        public Task<List<ShippingAddress>> GetShippingAddress(string UserID);
        public Task AddShippingAddress(Shipping address);

    }
}
