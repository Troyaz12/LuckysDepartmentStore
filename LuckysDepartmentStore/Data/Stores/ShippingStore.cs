using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Data.Stores
{
    public class ShippingStore : IShippingStore
    {
        public LuckysContext _context;

        public ShippingStore(LuckysContext context)
        {
            _context = context;
        }

        public async Task AddShippingAddress(Shipping address)
        {
            _context.Shipping.Add(address);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ShippingAddress>> GetShippingAddress(string UserID)
        {
            var shippingAddress = await _context.ShippingAddress
                       .Where(address => address.UserId == UserID)
                       .ToListAsync();

            return shippingAddress;
        }
    }
}
