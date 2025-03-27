using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Consumer;
using LuckysDepartmentStore.Models.ViewModels.Consumer;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface ICustomerStore
    {
        public Task SaveOrder(CustomerOrder order);
        public Task<bool> IsOrderValid(int orderNumber, string user);
        public Task CreateCustomerShippingAddress(ShippingAddress newAddress);
        public Task<List<ShippingAddressDTO>> GetShippingAddress(string userID);
        public Task<ShippingAddress> GetShippingAddressByID(int id);
        public Task DeleteShippingAddress(ShippingAddress shippingAddress);
        public Task<int> UpdateShippingAddress(ShippingAddressVM shippingAddress);
    }
}
