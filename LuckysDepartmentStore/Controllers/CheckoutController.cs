﻿using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Checkout;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuckysDepartmentStore.Controllers
{
    [Authorize]
    public class CheckoutController(ICheckoutService _checkoutService, IShoppingCartService _shoppingCartService, IConsumerService _consumerService, UserManager<ApplicationUser> _userManager) : Controller
    {
        // GET: CheckoutController
        public async Task<ActionResult> AddressAndPayment()
        {
            OrderModelVM ordervm = new OrderModelVM()
            {
                Shipping = new List<ShippingAddressVM>(),
                Payment = new List<PaymentOptionsVM>()
            };

            var shippingAddresses = await _consumerService.GetShippingAddress(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var paymentOptions = await _consumerService.GetPaymentOptions(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (shippingAddresses != null)
            {
                ordervm.Shipping = shippingAddresses.Data;             
            }

            if (paymentOptions != null)
            {
                ordervm.Payment = paymentOptions.Data;
            }

                return View(ordervm);
        }
        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(int SelectedShippingAddressID, int SelectedPaymentOptionsID, List<ShippingAddressVM> ShippingAddresses, List<PaymentOptionsVM> PaymentOptions)//OrderModelVM values)
        {
            var order = new Order();

            
            try
            {
                var selectedShipping = ShippingAddresses.FirstOrDefault(s => s.ShippingAddressID == SelectedShippingAddressID);
                var selectedPayment = PaymentOptions.FirstOrDefault(p => p.PaymentOptionsID == SelectedPaymentOptionsID);

                var user = await _userManager.GetUserAsync(User);
                order.UserId = user.Id;
                order.UserName = selectedShipping.FirstName;                
                order.OrderDate = DateTime.Now;
                order.FirstNameShipping = selectedShipping.FirstName;
                order.LastNameShipping = selectedShipping.LastName;
                order.Address1 = selectedShipping.Address1;                
                order.Address2 = selectedShipping?.Address2 ?? order.Address2;                
                order.City = selectedShipping.City;
                order.state = selectedShipping.State;
                order.Zip = selectedShipping.ZipCode;

                order.FirstNameBilling = selectedPayment.FirstName;
                order.LastNameBilling = selectedPayment.LastName;
                order.AccountNumber = selectedPayment.AccountNumber;
                order.CvcCode = selectedPayment.CvcCode;
                order.BillingAddress1 = selectedPayment.BillingAddress1;
                order.BillingAddress2 = selectedPayment?.BillingAddress2 ?? order.BillingAddress2;
                order.BillingCity = selectedPayment.City;
                order.BillingState = selectedPayment.State;
                order.BillingZipCode = selectedPayment.ZipCode;

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
