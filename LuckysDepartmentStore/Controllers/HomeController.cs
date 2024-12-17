using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LuckysDepartmentStore.Controllers
{
    public class HomeController(IDiscountService _discountService, ILogger<HomeController> _logger, IProductService _productService) : Controller
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
		public IActionResult Login(Customer customer)
		{


			return View();
		}
        [HttpGet]
        public async Task<IActionResult> Item(string? productId)
        {
            

            return View();
        }
    }
}
