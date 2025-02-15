using AutoMapper;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;


namespace LuckysDepartmentStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<CartHub> _logger;
        private readonly IProductService _productService;
        private IMapper _mapper;

        public ShoppingCartController(IShoppingCartService shoppingCartService, ILogger<CartHub> logger, IProductService productService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
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
        [ValidateAntiForgeryToken]
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

            TempData["FailureMessage"] = "Unable to add to cart.";

            return RedirectToAction("Index", "Error");
        }
       
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var removeResult = await _shoppingCartService.RemoveItemFromCart(id);

            if (!removeResult.IsSuccess)
            {
                TempData["FailureMessage"] = removeResult.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index");
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
        // GET: Product/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id, int productId)
        {
            if (id == 0 || productId == 0)
            {
                TempData["ErrorMessage"] = "Unable to get cart item.";

                return RedirectToAction("Index", "Error");
            }


            var product = await _productService.GetItem(productId);

            if (!product.IsSuccess)
            {
                TempData["ErrorMessage"] = product.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            var cartItem = _mapper.Map<CartItemVM>(product.Data);
            cartItem.cartItemID = id;

            cartItem.Color = cartItem.ColorProduct.Select(product => product.Name)
               .Distinct()
               .ToList();

            return View(cartItem);
        }        
        // GET: Product/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(CartItemEdit item)
        {
            if (ModelState.IsValid)
            {

                var itemResponse = await _shoppingCartService.EditItemInCart(item);

                if (!itemResponse.IsSuccess)
                {
                    TempData["ErrorMessage"] = itemResponse.ErrorMessage;

                    return RedirectToAction("Index", "Error");
                }

                return RedirectToAction("Index");

            }

            return View(item);
        }
    }
}