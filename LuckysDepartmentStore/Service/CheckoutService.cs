using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class CheckoutService : ICheckoutService
    {
        private readonly LuckysContext _context;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShippingStore _shippingStore;
        private readonly IPaymentStore _paymentStore;
        private readonly ICustomerStore _customerStore;
        private readonly ILogger _logger;

        public CheckoutService(LuckysContext context, IShoppingCartService shoppingCartService, 
            IShippingStore shippingStore, IPaymentStore paymentStore, ICustomerStore customerStore, ILogger<CheckoutService> logger)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
            _shippingStore = shippingStore;
            _paymentStore = paymentStore;
            _customerStore = customerStore;
            _logger = logger;
        }

        public async Task<Utilities.ExecutionResult<OrderIds>> Order(Order order)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    int customerId;
                    OrderIds orderids = new OrderIds();

                   // var shippingAddress = await _shippingStore.GetShippingAddress(order.UserId);

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

                    await _paymentStore.SavePayment(payment);

                    

                    // insert into shipping id
                    var shipping = new Shipping();
                    shipping.FirstName = order.FirstNameShipping;
                    shipping.LastName = order.LastNameShipping;
                    shipping.Address1 = order.Address1;
                    shipping.Address2 = order.Address2;
                    shipping.City = order.City;
                    shipping.state = order.state;
                    shipping.Zip = order.Zip;

                    await _shippingStore.AddShippingAddress(shipping);

                    // insert into customer orders
                    var customerOrder = new CustomerOrder();
                    customerOrder.PaymentID = payment.PaymentID;
                    customerOrder.ShippingAddressID = shipping.ShippingID;
                    customerOrder.UserId = order.UserId;

                    await _customerStore.SaveOrder(customerOrder);

                    var cart = _shoppingCartService.GetCart();

                    if (!cart.IsSuccess)
                    {
                        _logger.LogError("Failed to process order {@order}. Unable to get CartID.", order);
                        await transaction.RollbackAsync(); // Explicit rollback for logical failure
                        return Utilities.ExecutionResult<OrderIds>.Failure($"Failed to process order.");
                    }

                    Product product = new Product();
                    var orderTotal = await _shoppingCartService.CreateOrder(cart.Data, customerOrder.CustomerOrderID);

                    if (!orderTotal.IsSuccess)
                    {
                        _logger.LogError("Failed to process order {@order}", order);
                        await transaction.RollbackAsync(); // Explicit rollback for logical failure
                        return Utilities.ExecutionResult<OrderIds>.Failure($"Failed to calculate order total: {orderTotal.ErrorMessage}");
                    }

                    // Directly update `payment` instead of querying the DB again
                    payment.Total = orderTotal.Data;
                    int paymentSaved = await _paymentStore.UpdatePayment(payment);

                    if (paymentSaved == 0)
                    {
                        _logger.LogError("Failed update payment for order {@order}", order);
                        await transaction.RollbackAsync(); // Explicit rollback
                        return Utilities.ExecutionResult<OrderIds>.Failure("Failed to update payment total.");
                    }

                    await transaction.CommitAsync();

                    orderids.PaymentID = payment.PaymentID;
                    orderids.CustomerOrderID = customerOrder.CustomerOrderID;
                    orderids.ShippingID = shipping.ShippingID;

                    return Utilities.ExecutionResult<OrderIds>.Success(orderids);
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Failed to save order to the database {@order}", order);
                    return Utilities.ExecutionResult<OrderIds>.Failure("Failed to create order.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to process order {@order}", order);
                    // Rollback the transaction on error
                    await transaction.RollbackAsync();
                    return Utilities.ExecutionResult<OrderIds>.Failure("Order failed.");
                }
            }
        }
        public async Task<Utilities.ExecutionResult<bool>> IsValid(int orderNumber, string user)
        {
            try
            {
                bool isValid = await _customerStore.IsOrderValid(orderNumber, user);

                return Utilities.ExecutionResult<bool>.Success(isValid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to validate order {@orderNumber}, {user}", orderNumber, user);
                return Utilities.ExecutionResult<bool>.Failure("Failed to check validation.");
            }           
        }
    }
}
