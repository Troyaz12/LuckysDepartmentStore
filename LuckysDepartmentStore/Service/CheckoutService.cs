using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class CheckoutService : ICheckoutService
    {
        public LuckysContext _context;
        public CheckoutService(LuckysContext context)
        {
            _context = context;
        }

        public int Order(Order order)
        {

            return 0;
        }
        public bool IsValid(int orderNumber, string user)
        {
            bool isValid = _context.CustomerOrders
                .Include(co => co.Customer)
                .ThenInclude(c => c.User)
                .Any(o => o.CustomerOrderID == orderNumber && o.Customer.User.UserName == user);

            return isValid;
        }

    }
}
