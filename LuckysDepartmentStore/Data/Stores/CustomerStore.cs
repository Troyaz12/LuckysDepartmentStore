using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Consumer;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using Microsoft.EntityFrameworkCore;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace LuckysDepartmentStore.Data.Stores
{
    public class CustomerStore : ICustomerStore
    {
        public LuckysContext _context;

        public CustomerStore(LuckysContext context)
        {
            _context = context;
        }
        public async Task SaveOrder(CustomerOrder order)
        {
            _context.CustomerOrders.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsOrderValid(int orderNumber, string user)
        {
            bool isValid = await _context.CustomerOrders
             .Include(co => co.CustomerShippingData)
             .ThenInclude(c => c.User)
             .AnyAsync(o => o.CustomerOrderID == orderNumber && o.CustomerShippingData.User.UserName == user);

            return isValid;
        }

        public async Task CreateCustomerShippingAddress(ShippingAddress newAddress)
        {
            var addressId = await _context.ShippingAddress.AddAsync(newAddress);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShippingAddressDTO>> GetShippingAddress(string userID)
        {
            var allAddresses = await _context.ShippingAddress
               .Where(address => address.UserId == userID)
               .Select(address => new ShippingAddressDTO
               {
                   FirstName = address.FirstName,
                   LastName = address.LastName,
                   Address1 = address.Address1,
                   Address2 = address.Address2,
                   City = address.City,
                   State = address.State,
                   ZipCode = address.ZipCode,
                   ShippingAddressID = address.ShippingAddressID
               })
               .ToListAsync();

            return allAddresses;
        }
        public async Task<ShippingAddress> GetShippingAddressByID(int id)
        {
            var shippingAddress = await _context.ShippingAddress.FindAsync(id);

            return shippingAddress;
        }
        public async Task DeleteShippingAddress(ShippingAddress shippingAddress)
        {
            _context.ShippingAddress.Remove(shippingAddress);
            await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateShippingAddress(ShippingAddressVM shippingAddress)
        {
            var shippingAddressOld = await _context.ShippingAddress.FindAsync(shippingAddress.ShippingAddressID);

            if (shippingAddressOld == null)
                return 0; // No record found to update

            shippingAddressOld.FirstName = shippingAddress.FirstName;
            shippingAddressOld.LastName = shippingAddress.LastName;
            shippingAddressOld.Address1 = shippingAddress.Address1;
            shippingAddressOld.Address2 = shippingAddress.Address2;
            shippingAddressOld.City = shippingAddress.City;
            shippingAddressOld.State = shippingAddress.State;
            shippingAddressOld.ZipCode = shippingAddress.ZipCode;

            var rowsEffected = _context.SaveChanges();

            return rowsEffected;
        }
    }
}
