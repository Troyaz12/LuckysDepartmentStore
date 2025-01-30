using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class CheckoutService : ICheckoutService
    {
        public LuckysContext _context;
        private IShoppingCartService _shoppingCartService;

        public CheckoutService(LuckysContext context, IShoppingCartService shoppingCartService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<Utilities.ExecutionResult<OrderIds>> Order(Order order)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    int customerId;
                    OrderIds orderids = new OrderIds();

                    var shippingAddress = await _context.ShippingAddress
                        .Where(address => address.UserId == order.UserId)
                        .ToListAsync();

                    // insert payment
                    var payment = new Payment();

                    // add name
                    payment.AccountNumber = order.AccountNumber;
                    payment.CvcCode = order.CvcCode;
                    payment.BillingAddress1 = order.BillingAddress1;
                    payment.BillingAddress2 = order.BillingAddress2;
                    payment.City = order.City;
                    payment.State = order.state;
                    payment.ZipCode = order.Zip;
                    payment.IsCheckingAccount = false;
                    payment.IsCreditCard = true;
                    payment.IsProcessed = false;
                    payment.ProcessMessage = "Pending";

                    _context.Payments.Add(payment);

                    await _context.SaveChangesAsync();

                    // insert into shipping id
                    var shipping = new Shipping();
                    shipping.FirstName = order.FirstNameShipping;
                    shipping.LastName = order.LastNameShipping;
                    shipping.Address1 = order.Address1;
                    shipping.Address2 = order.Address2;
                    shipping.City = order.City;
                    shipping.state = order.state;
                    shipping.Zip = order.Zip;

                    _context.Shipping.Add(shipping);

                    await _context.SaveChangesAsync();

                    // insert into customer orders
                    var customerOrder = new CustomerOrder();
                    customerOrder.PaymentID = payment.PaymentID;
                    customerOrder.ShippingAddressID = shipping.ShippingID;
                    customerOrder.UserId = order.UserId;

                    _context.CustomerOrders.Add(customerOrder);
                    await _context.SaveChangesAsync();

                    var cart = _shoppingCartService.GetCart();
                    Product product = new Product();
                    var orderTotal = await _shoppingCartService.CreateOrder(product, cart, customerOrder.CustomerOrderID);

                    // Directly update `payment` instead of querying the DB again
                    payment.Total = orderTotal.Data;
                    int paymentSaved = await _context.SaveChangesAsync();

                    if (paymentSaved == 0)
                    {
                        return Utilities.ExecutionResult<OrderIds>.Failure("Failed to update payment total.");
                    }

                    await transaction.CommitAsync();

                    orderids.PaymentID = payment.PaymentID;
                    orderids.CustomerOrderID = customerOrder.CustomerOrderID;
                    orderids.ShippingID = shipping.ShippingID;

                    return Utilities.ExecutionResult<OrderIds>.Success(orderids);
                }
                catch (Exception ex)
                {
                    // Rollback the transaction on error
                    await transaction.RollbackAsync();
                    return Utilities.ExecutionResult<OrderIds>.Failure($"Order failed: {ex.Message}");
                }
            }
        }
        public async Task<Utilities.ExecutionResult<bool>> IsValid(int orderNumber, string user)
        {
            try
            {
                bool isValid = await _context.CustomerOrders
               .Include(co => co.Customer)
               .ThenInclude(c => c.User)
               .AnyAsync(o => o.CustomerOrderID == orderNumber && o.Customer.User.UserName == user);

                return Utilities.ExecutionResult<bool>.Success(isValid);
            }
            catch (Exception ex)
            {
                return Utilities.ExecutionResult<bool>.Failure("Failed to check validation.");
            }           
        }
    }
}
