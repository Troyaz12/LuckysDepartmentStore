using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Controllers
{
    public class ShoppingCartController(IShoppingCartService _shoppingCartService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // GET: /Store/AddToCart/5
        public async Task<ActionResult> AddToCartAsync(ItemVM item)
        {
            if (ModelState.IsValid)
            {
                var cart = ShoppingCartService.GetCart(this.HttpContext);
                await _shoppingCartService.AddToCartAsync(item, cart.ShoppingCartId);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Item", "Home", new { productId = item.ProductID });
        }
        public async Task<ActionResult> RemoveFromCartAsync(ItemVM item)
        {
            if (ModelState.IsValid)
            {
                var cart = ShoppingCartService.GetCart(this.HttpContext);
                await _shoppingCartService.AddToCartAsync(item, cart.ShoppingCartId);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Item", "Home", new { productId = item.ProductID });
        }
        
        //[ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCartService.GetCart(this.HttpContext);

            ViewData["CartCount"] = _shoppingCartService.GetCount(cart.ShoppingCartId);

            return PartialView("CartSummary");
        }
    }
}
