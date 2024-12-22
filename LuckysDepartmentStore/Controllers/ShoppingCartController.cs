using LuckysDepartmentStore.Models;
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
        public ActionResult AddToCart(Product product)
        {       
            var cart = ShoppingCartService.GetCart(this.HttpContext);
            _shoppingCartService.AddToCartAsync(product, cart.ShoppingCartId);

            return RedirectToAction("Index");
        }









    }
}
