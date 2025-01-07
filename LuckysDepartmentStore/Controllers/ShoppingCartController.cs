using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Controllers
{
    public class ShoppingCartController(IShoppingCartService _shoppingCartService) : Controller
    {
        public async Task<IActionResult>Index()
        {
            var cartID = _shoppingCartService.GetCart();
            var allItems = await _shoppingCartService.GetCartItems(cartID);




            return View(allItems.Data);
        }
        // GET: /Store/AddToCart/5
        public async Task<ActionResult> AddToCartAsync(ItemVM item)
        {
            if (ModelState.IsValid)
            {
                var cart = _shoppingCartService.GetCart();
                await _shoppingCartService.AddToCartAsync(item, cart);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Item", "Home", new { productId = item.ProductID });
        }
        public async Task<ActionResult> RemoveFromCartAsync(ItemVM item)
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
        public ActionResult CartSummary()
        {
            var cart = _shoppingCartService.GetCart();

            ViewData["CartCount"] = _shoppingCartService.GetCount(cart);

            return PartialView("CartSummary");
        }
    }
}
