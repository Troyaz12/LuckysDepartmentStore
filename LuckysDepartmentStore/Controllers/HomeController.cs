using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
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
        public async Task<IActionResult> IndexAsync()
        {
            var frontPageData = await _discountService.GetActiveDiscounts();

            if (!frontPageData.IsSuccess)
            {
                TempData["FailureMessage"] = "Error getting product data.";

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
        public IActionResult Login(string inputEmail, string inputPassword)
        {


            return View();
        }
        [HttpGet]
        public IActionResult CreateNewAccount()
        {


            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Login(LogOnModel model, string returnUrl)
		{
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    MigrateShoppingCart(model.UserName);                    

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            
            return View(model);

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
        [HttpPost]
        private void MigrateShoppingCart(string UserName)
        {
            // Associate shopping cart items with logged-in user
            var cart = _shoppingCartService.GetCart();

            _shoppingCartService.MigrateCart(UserName, cart);
            HttpContext.Session.SetString(ShoppingCart.CartSessionKey, UserName);
        }
       
        [HttpPost]
        public async Task<ActionResult> Register(RegisterationModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    MigrateShoppingCart(model.UserName);
                   
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }



    }
}
