﻿using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuckysDepartmentStore.Controllers
{
    public class ConsumerController(IConsumerService _consumerService, UserManager<IdentityUser> _userManager) : Controller
    {
        //private readonly UserManager<IdentityUser> _userManager;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateShippingAddress([FromBody] ShippingAddressVM shippingAddress)
        {
           var user = _userManager.GetUserAsync(User);
           shippingAddress.UserId = user.Result.Id;           

            var newAddresses = await _consumerService.CreateShippingAddress(shippingAddress);

            if (newAddresses.IsSuccess == false)
            {
                return View();
            }

            var allAddresses = await _consumerService.GetShippingAddress(shippingAddress.UserId);


            return PartialView("_ShippingPartial", allAddresses);            
        }

        [HttpGet]
        public async Task<IActionResult> GetAddresses(string userId)
        {
            var addresses = await _consumerService.GetShippingAddress(userId);

            if (addresses.IsSuccess == false)
            {
                return View();
            }

            return View(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentOption(PaymentOptionsVM paymentOption)
        {
            var newPaymentOption = await _consumerService.CreatePaymentOption(paymentOption);

            if (newPaymentOption.IsSuccess == false)
            {
                return View();
            }

            var allPaymentOptions = await _consumerService.GetShippingAddress(User.FindFirstValue(ClaimTypes.NameIdentifier));


            return PartialView("_PaymentPartial", allPaymentOptions);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentOptions(string userId)
        {
            var paymentOptions = await _consumerService.GetPaymentOptions(userId);

            if (paymentOptions.IsSuccess == false)
            {
                return View();
            }

            return View(paymentOptions);
        }
    }
}
