using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace LuckysDepartmentStore.Controllers
{
    public class HomeController(IDiscountService _discountService, IProductService _productService, 
        IShoppingCartService _shoppingCartService, 
           IUtility _utility) : Controller
    {      
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            // Call GetCartId() early to ensure session is set up
            var cartID = _shoppingCartService.GetCart();

            if (!cartID.IsSuccess)
            {
                TempData["FailureMessage"] = "Error getting cart data.";
                return RedirectToAction("Index", "Error");
            }

            var frontPageData = await _discountService.GetActiveDiscounts();

            if (!frontPageData.IsSuccess)
            {
                TempData["ErrorMessage"] = frontPageData.ErrorMessage;

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
            string? brandSelection, int? productID, string? discountTags, string? discountDescription)
        {
            if (!string.IsNullOrEmpty(discountTags))
            {
                discountTags = discountTags.Trim();
            }

            if (!_utility.IsSearchStringValid(discountTags, out string errorMessage))
            {
                TempData["ErrorMessage"] = errorMessage;
                return RedirectToAction("Index", "Error");
            }

            var productList = await _productService.GetProductsByDiscount(categorySelection, subCategorySelection, brandSelection, productID, discountTags);

            if (!productList.IsSuccess)
            {
                TempData["ErrorMessage"] = productList.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            string description = "Showing results for \"" + discountDescription + "\"" + "(" + productList.Data.Count + " items" + ")";
            TempData["description"] = description;

            return View(productList.Data);
        }
        [HttpGet]
        public async Task<IActionResult> SearchProduct(string? category, string? subCategory, 
            string? brand, int? productID, string? searchString)
        {

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.Trim();             
            }
            
            if (!_utility.IsSearchStringValid(searchString, out string errorMessage))
            {
                TempData["ErrorMessage"] = errorMessage;
                return RedirectToAction("Index", "Error");
            }

            var productList = await _productService.GetProductsSearch(category, subCategory, brand, productID, searchString);

            if (!productList.IsSuccess)
            {
                TempData["ErrorMessage"] = productList.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            StringBuilder searchItem = new StringBuilder();
            searchItem.Append(category + " " ?? "");
            searchItem.Append(subCategory + " " ?? "");
            searchItem.Append(brand + " " ?? "");
            searchItem.Append(searchString ?? "");

            string description = "Showing results for \"" + searchItem.ToString().TrimEnd() + "\"" + " (" + productList.Data.Count + " items" + ")";

            TempData["description"] = description;

            return View(productList.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Item(int productId)
        {
            var product = await _productService.GetItem(productId);           

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
    }
}
