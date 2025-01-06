using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Collections.Generic;

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

            int customerId;
            OrderIds orderids = new OrderIds();
         
            var shippingAddress = _context.ShippingAddress
                .Where(address => address.UserId == order.UserId)
                .ToList();

            // insert payment
            var payment = new Payment();
        //    payment.CustomerID = customerId;

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

            var paymentId = _context.SaveChanges();

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

            var shippingId = _context.SaveChanges();

            // insert into customer orders
            var customerOrder = new CustomerOrder();
         //   customerOrder.CustomerID = customerId;
            customerOrder.PaymentID = paymentId;

            _context.CustomerOrders.Add(customerOrder);
            var customerOrderId = _context.SaveChanges();

            var cart = _shoppingCartService.GetCart();
            Product product = new Product();
            var orderTotal = _shoppingCartService.CreateOrder(product, cart, customerOrderId);

            var paymentUpdate = _context.Payments.Find(paymentId);

            if (paymentUpdate != null)
            {
                //orderUpdate.
                paymentUpdate.Total = orderTotal.Result.Data;

                _context.Payments.Add(payment);

                var result = _context.SaveChanges();
            }
            else
            {
                return Utilities.ExecutionResult<OrderIds>.Failure("Find payment.");
            }

            orderids.PaymentID = paymentId;
        //    orderids.CustomerID = customerId;
            orderids.CustomerOrderID = customerOrderId;
            orderids.ShippingID = shippingId;            

            return Utilities.ExecutionResult<OrderIds>.Success(orderids);
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
