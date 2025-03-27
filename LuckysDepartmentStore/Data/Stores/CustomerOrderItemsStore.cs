using AutoMapper;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Data.Stores
{
    public class CustomerOrderItemsStore : ICustomerOrderItemsStore
    {
        public LuckysContext _context;

        public CustomerOrderItemsStore(LuckysContext context)
        {
            _context = context;
        }
        public async Task<int> CustomerOrderItemsAdd(List<CustomerOrderItem> items)
        {
            _context.CustomerOrderItems.AddRange(items);
            var rowEffected = await _context.SaveChangesAsync();

            return rowEffected;
        }
    }
}
