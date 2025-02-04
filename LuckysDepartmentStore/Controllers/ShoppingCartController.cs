using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace LuckysDepartmentStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<CartHub> _logger;

        public ShoppingCartController(IShoppingCartService shoppingCartService, ILogger<CartHub> logger)
        {
            _shoppingCartService = shoppingCartService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cartID = _shoppingCartService.GetCart();
            var allItems = await _shoppingCartService.GetCartItems(cartID);

            if (!allItems.IsSuccess)
            {
                TempData["FailureMessage"] = allItems.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            return View(allItems.Data);
        }
        // Post: /Store/AddToCart/5
        [HttpPost]
        public async Task<IActionResult> AddToCartAsync(CartItemsDTO item)
        {
            if (ModelState.IsValid)
            {               
                var cart = _shoppingCartService.GetCart();
                var cartResp = await _shoppingCartService.AddToCartAsync(item, cart);

                if (!cartResp.IsSuccess)
                {
                    TempData["FailureMessage"] = cartResp.ErrorMessage;

                    return RedirectToAction("Index", "Error");
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Item", "Home", new { productId = item.ProductID });
        }
        [HttpPost]
        public async Task<ActionResult> RemoveFromCartAsync(CartItemsDTO item)
        {
            if (ModelState.IsValid)
            {
                var cart = _shoppingCartService.GetCart();
                var cartResp = await _shoppingCartService.AddToCartAsync(item, cart);

                if (!cartResp.IsSuccess)
                {
                    TempData["FailureMessage"] = cartResp.ErrorMessage;

                    return RedirectToAction("Index", "Error");
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Item", "Home", new { productId = item.ProductID });
        }

        //[ChildActionOnly]
        [HttpGet]
        public async Task<ActionResult> CartSummaryAsync()
        {
            var cart = _shoppingCartService.GetCart();

            var count = await _shoppingCartService.GetCount(cart);

            if (!count.IsSuccess)
            {
                TempData["FailureMessage"] = count.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            ViewData["CartCount"] = count.Data;


            return PartialView("CartSummary");
        }
        
        [HttpGet]
        public async Task<IActionResult> GetShoppingCartCountAsync()
        {
            var cart = _shoppingCartService.GetCart();
            var count = await _shoppingCartService.GetCount(cart);

            if (!count.IsSuccess)
            {
                _logger.LogError(count.ErrorMessage);
            }

            return Json(new { badge = count });
        }     
    }
}
