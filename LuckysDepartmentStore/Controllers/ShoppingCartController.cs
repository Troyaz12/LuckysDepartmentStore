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

            return View(allItems.Data);
        }
        // Post: /Store/AddToCart/5
        [HttpPost]
        public async Task<IActionResult> AddToCartAsync(CartItemsDTO item)
        {
            if (ModelState.IsValid)
            {               
                var cart = _shoppingCartService.GetCart();
                await _shoppingCartService.AddToCartAsync(item, cart);

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
                await _shoppingCartService.AddToCartAsync(item, cart);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Item", "Home", new { productId = item.ProductID });
        }

        //[ChildActionOnly]
        [HttpGet]
        public ActionResult CartSummary()
        {
            var cart = _shoppingCartService.GetCart();

            ViewData["CartCount"] = _shoppingCartService.GetCount(cart);

            return PartialView("CartSummary");
        }
        
        [HttpGet]
        public IActionResult GetShoppingCartCount()
        {
            var cart = _shoppingCartService.GetCart();
            var count = _shoppingCartService.GetCount(cart);
            return Json(new { badge = count });
        }     
    }
}
