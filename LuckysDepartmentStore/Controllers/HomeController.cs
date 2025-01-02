using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LuckysDepartmentStore.Controllers
{
    public class HomeController(IDiscountService _discountService, ILogger<HomeController> _logger, IProductService _productService, SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> _userManager, IShoppingCartService _shoppingCartService) : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            var frontPageData = _discountService.GetActiveDiscounts();

            if (!frontPageData.IsSuccess)
            {
                ViewBag.FailureMessage = frontPageData.ErrorMessage;

                return View();
            }

            return View(frontPageData.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public async Task<IActionResult> SearchAsync(string? categorySelection, string? subCategorySelection, string? brandSelection, int? productID, string? keywords)
        {
            var productList = await _productService.GetProductsByDiscount(categorySelection, subCategorySelection, brandSelection, productID, keywords);

            return View(productList.Data);
        }
        public IActionResult Login(string inputEmail, string inputPassword)
        {


            return View();
        }
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
            // If we got this far, something failed, redisplay form
            return View(model);
        
               // return View();
		}
        [HttpGet]
        public IActionResult Item(int? productId)
        {
            var product = _productService.GetItem((int) productId);
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
        private void MigrateShoppingCart(string UserName)
        {
            // Associate shopping cart items with logged-in user
            var cart = ShoppingCartService.GetCart(this.HttpContext);

            _shoppingCartService.MigrateCart(UserName, cart.ShoppingCartId);
            HttpContext.Session.SetString(ShoppingCart.CartSessionKey, UserName);
        }
       
        [HttpPost]
        public async Task<ActionResult> Register(RegisterationModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
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
