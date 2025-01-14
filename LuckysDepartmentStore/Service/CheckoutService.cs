using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Service.Interfaces;
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

            _context.SaveChanges();

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

            _context.SaveChanges();

            // insert into customer orders
            var customerOrder = new CustomerOrder();
         //   customerOrder.CustomerID = customerId;
            customerOrder.PaymentID = payment.PaymentID;
            customerOrder.ShippingAddressID = shipping.ShippingID;
            customerOrder.UserId = order.UserId;

            _context.CustomerOrders.Add(customerOrder);
            _context.SaveChanges();

            var cart = _shoppingCartService.GetCart();
            Product product = new Product();
            var orderTotal = _shoppingCartService.CreateOrder(product, cart, customerOrder.CustomerOrderID);

            var paymentUpdate = _context.Payments.Find(payment.PaymentID);

            if (paymentUpdate != null)
            {
                var blog = _context.Payments.Single(b => b.PaymentID == payment.PaymentID);
                blog.Total = orderTotal.Result.Data;
                _context.SaveChanges();

                ////orderUpdate.
                //paymentUpdate.Total = orderTotal.Result.Data;

                //_context.Entry(paymentUpdate).State = EntityState.Modified;

                //var result = _context.SaveChanges();
            }
            else
            {
                return Utilities.ExecutionResult<OrderIds>.Failure("Find payment.");
            }

            orderids.PaymentID = payment.PaymentID;
        //    orderids.CustomerID = customerId;
            orderids.CustomerOrderID = customerOrder.CustomerOrderID;
            orderids.ShippingID = shipping.ShippingID;            

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
