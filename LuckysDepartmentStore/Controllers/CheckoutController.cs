using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Checkout;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuckysDepartmentStore.Controllers
{
    [Authorize]
    public class CheckoutController(ICheckoutService _checkoutService, IShoppingCartService _shoppingCartService, IConsumerService _consumerService) : Controller
    {
        // GET: CheckoutController
        public ActionResult AddressAndPayment()
        {
            OrderModelVM ordervm = new OrderModelVM()
            {
                Shipping = new List<ShippingAddressVM>(),
                Payment = new List<Payment>()
            };

            var shippingAddresses = _consumerService.GetShippingAddress(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(shippingAddresses != null)
            {
                ordervm.Shipping = shippingAddresses.Result.Data;
            }



            return View(ordervm);
        }
        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(OrderModelVM values)
        {
            var order = new Order();            

            try
            {
               
                //order.UserName = User.Identity.Name;
                //order.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                //order.OrderDate = DateTime.Now;
                ////order.FirstName = User.Identities.
                ////order.LastName = User.Identities.
                //order.Address1 = values.Shipping.Address1;
                //order.Address2 = values.Shipping.Address2;
                //order.City = values.Shipping.City;
                //order.state = values.Shipping.state;
                //order.Zip = values.Shipping.Zip;
                //order.RoutingNumber = values.Payment.RoutingNumber;
                //order.AccountNumber = values.Payment.AccountNumber;
                //order.CvcCode = values.Payment.CvcCode;
                //order.BillingAddress1 = values.Payment.BillingAddress1;
                //order.BillingAddress2 = values.Payment.BillingAddress2;
                //order.BillingCity = values.Payment.City;
                //order.BillingState = values.Payment.State;
                //order.BillingZipCode = values.Payment.ZipCode;
                //order.IsCheckingAccount = values.Payment.IsCheckingAccount;
                //order.IsCreditCard = values.Payment.IsCreditCard;



                //Save Order
                var orderId = await _checkoutService.Order(order);

                //Process the order
                var cart = _shoppingCartService.GetCart();
                Product product = new Product();
                var orderResult = _shoppingCartService.CreateOrder(product, cart, orderId.Data.CustomerOrderID);

                return RedirectToAction("Complete",
                new { id = orderId.Data.CustomerOrderID });  // revisit to make sure this is correct
               
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        // GET: CheckoutController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CheckoutController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckoutController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckoutController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CheckoutController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckoutController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CheckoutController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            var isValid = _checkoutService.IsValid(id, User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
