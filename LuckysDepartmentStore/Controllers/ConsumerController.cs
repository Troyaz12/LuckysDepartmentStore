using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace LuckysDepartmentStore.Controllers
{
    public class ConsumerController(IConsumerService _consumerService, UserManager<ApplicationUser> _userManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateShippingAddress([FromBody] ShippingAddressVM shippingAddress)
        {
           var user = await _userManager.GetUserAsync(User);
           shippingAddress.UserId = user.Id;           

            var newAddresses = await _consumerService.CreateShippingAddress(shippingAddress);

            if (!newAddresses.IsSuccess)
            {
                TempData["FailureMessage"] = newAddresses.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            var allAddresses = await _consumerService.GetShippingAddress(shippingAddress.UserId);

            if (!allAddresses.IsSuccess)
            {
                TempData["FailureMessage"] = allAddresses.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            return PartialView("_ShippingPartial", allAddresses.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAddresses(string userId)
        {
            var addresses = await _consumerService.GetShippingAddress(userId);
         
            if (!addresses.IsSuccess)
            {
                TempData["FailureMessage"] = addresses.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            return View(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentOption([FromBody] PaymentOptionsVM paymentOption)
        {
            var user = await _userManager.GetUserAsync(User);
            paymentOption.UserId = user.Id;

            var newPaymentOption = await _consumerService.CreatePaymentOption(paymentOption);
            
            if (!newPaymentOption.IsSuccess)
            {
                TempData["FailureMessage"] = newPaymentOption.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            var allPaymentOptions = await _consumerService.GetPaymentOptions(paymentOption.UserId);


            return PartialView("_PaymentPartial", allPaymentOptions.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentOptions(string userId)
        {
            var paymentOptions = await _consumerService.GetPaymentOptions(userId);

            if (!paymentOptions.IsSuccess)
            {
                TempData["FailureMessage"] = paymentOptions.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            return View(paymentOptions);
        }
        [HttpPost]
        public async Task<IActionResult> EditShippingAddress([FromBody] ShippingAddressVM shippingAddress)
        {
            var user = await _userManager.GetUserAsync(User);
            shippingAddress.UserId = user.Id;

            if (!ModelState.IsValid)
            {
                // Return errors along with the partial view
                var errorMessages = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(k => k.Key, v => v.Value.Errors.Select(e => e.ErrorMessage).ToList());

                return Json(new { success = false, errors = errorMessages });
            }

            var editedAddresses = await _consumerService.EditShippingRecord(shippingAddress);

            if (!editedAddresses.IsSuccess)
            {
                TempData["FailureMessage"] = editedAddresses.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            var allAddresses = await _consumerService.GetShippingAddress(shippingAddress.UserId);

            if (!allAddresses.IsSuccess)
            {
                TempData["FailureMessage"] = allAddresses.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            return PartialView("_ShippingPartial", allAddresses.Data);           

        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var deleteAddress = await _consumerService.DeleteShippingRecord(id);

            if (!deleteAddress.IsSuccess)
            {
                TempData["FailureMessage"] = deleteAddress.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> EditPayment([FromBody] PaymentOptionsVM paymentOptions)
        {
            var user = await _userManager.GetUserAsync(User);
            paymentOptions.UserId = user.Id;

            var editedAddresses = await _consumerService.EditPaymentRecord(paymentOptions);


            if (!editedAddresses.IsSuccess)
            {
                TempData["FailureMessage"] = editedAddresses.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            var allPaymentOptions = await _consumerService.GetPaymentOptions(paymentOptions.UserId);

            if (!allPaymentOptions.IsSuccess)
            {
                TempData["FailureMessage"] = allPaymentOptions.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            return PartialView("_PaymentPartial", allPaymentOptions.Data);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePayment([FromBody] int id)
        {
            var deleteAddress = await _consumerService.DeletePaymentRecord(id);

            if (!deleteAddress.IsSuccess)
            {
                TempData["FailureMessage"] = deleteAddress.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }
            return Json(new { success = true });
        }
    }
}
