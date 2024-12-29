using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Service;
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
            return RedirectToAction("Home", "Item", new { productId = item.ProductID });
        }

    }
}
