using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LuckysDepartmentStore.Controllers
{
    public class HomeController(IDiscountService _discountService, ILogger<HomeController> _logger, IProductService _productService, SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager, IShoppingCartService _shoppingCartService) : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var frontPageData = await _discountService.GetActiveDiscounts();

            if (!frontPageData.IsSuccess)
            {
                TempData["FailureMessage"] = frontPageData.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            return View(frontPageData.Data);
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public async Task<IActionResult> SearchDiscount(string? categorySelection, string? subCategorySelection, 
            string? brandSelection, int? productID, string? discountTags)
        {
            var productList = await _productService.GetProductsByDiscount(categorySelection, subCategorySelection, brandSelection, productID, discountTags);

            if (!productList.IsSuccess)
            {
                TempData["ErrorMessage"] = productList.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            return View(productList.Data);
        }
        [HttpGet]
        public async Task<IActionResult> SearchProduct(string? category, string? subCategory, 
            string? brand, int? productID, string? searchString)
        {
            var productList = await _productService.GetProductsSearch(category, subCategory, brand, productID, searchString);

            if (!productList.IsSuccess)
            {
                TempData["ErrorMessage"] = productList.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            return View(productList.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Item(int? productId)
        {
            var product = await _productService.GetItem((int) productId);           

            if (!product.IsSuccess)
            {
                TempData["ErrorMessage"] = product.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            product.Data.Color = product.Data.ColorProduct.Select(product => product.Name)
               .Distinct()
               .ToList();

            return View(product.Data);
        }

        [HttpPost] // get all sizes for each color
        public IActionResult GetSizeButtons([FromBody] List<ColorSizesVM> color)
        {
            try
            {
                var allSizes = color
                .Where(product => product.Name == product.SelectedColor)
                .Select(product => new Sizes
                {
                    Size = product.SizeName,
                    SizesID = product.SizeID
                })
                .Distinct()
                .ToList();

                return PartialView("_ButtonPartial", allSizes);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return RedirectToAction("Index", "Error");
            }
            
        }
        //[HttpPost]
        //private void MigrateShoppingCart(string UserName)
        //{
        //    // Associate shopping cart items with logged-in user
        //    var cart = _shoppingCartService.GetCart();

        //    var migrateResult = _shoppingCartService.MigrateCart(UserName, cart);

        //    if (!product.IsSuccess)
        //    {
        //        TempData["ErrorMessage"] = product.ErrorMessage;

        //        return RedirectToAction("Index", "Error");
        //    }

        //    HttpContext.Session.SetString(ShoppingCart.CartSessionKey, UserName);
        //}
       
        //[HttpPost]
        //public async Task<ActionResult> Register(RegisterationModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
        //        var result = await _userManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded)
        //        {
        //            MigrateShoppingCart(model.UserName);
                   
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}



    }
}
