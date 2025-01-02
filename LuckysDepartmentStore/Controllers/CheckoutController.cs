using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Checkout;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Controllers
{
    //[Authorize]
    public class CheckoutController(ICheckoutService _checkoutService, IShoppingCartService _shoppingCartService) : Controller
    {
        // GET: CheckoutController
        public ActionResult AddressAndPayment()
        {
            OrderModelVM ordervm = new OrderModelVM()
            {
                Shipping = new Shipping(),
                Payment = new Payment()
            };

            return View(ordervm);
        }
        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPaymentAsync(OrderModelVM values)
        {
            var order = new Order();

            bool isUpdated = await TryUpdateModelAsync(order);

            try
            {
               
                order.UserName = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                //Save Order
                var orderId = _checkoutService.Order(order);

                //Process the order
                var cart = ShoppingCartService.GetCart(this.HttpContext);
                Product product = new Product();
                var orderResult = _shoppingCartService.CreateOrder(product, cart.ShoppingCartId);

                return RedirectToAction("Complete",
                new { id = orderResult });  // revisit to make sure this is correct
               
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
