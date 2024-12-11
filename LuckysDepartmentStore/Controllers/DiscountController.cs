using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace LuckysDepartmentStore.Controllers
{
    public class DiscountController(IProductService _productService, IDiscountService _discountService) : Controller
    {
        // GET: DiscountController
        public ActionResult Index()
        {
            var discounts = _discountService.GetActiveDiscounts();

            if (!discounts.IsSuccess)
            {
                ViewBag.FailureMessage = discounts.ErrorMessage;

                return View();
            }

            return View(discounts.Data);
        }

        // GET: DiscountController/Details/5
        public ActionResult Details(int id)
        {

            var discount = _discountService.GetDiscount(id);

            if (!discount.IsSuccess)
            {
                TempData["FailureMessage"] = discount.ErrorMessage;

                return RedirectToAction(nameof(Index));
            }



            return View(discount.Data);
        }

        // GET: DiscountController/Create
        public ActionResult Create(DiscountVM discount)
        {
            DiscountCreateVM product = new DiscountCreateVM();
            product.Category = _productService.GetCategory();
            product.SubCategory = _productService.GetSubCategory();
            product.Brand = _productService.GetBrand();

            return View(product);
        }

        // POST: DiscountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DiscountCreateVM discount)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    if (discount.ProductID.HasValue)
                    {
                        var productExists = _productService.GetDetails(discount.ProductID ?? 0);

                        if (!productExists.IsSuccess)
                        {
                            ViewBag.FailureMessage = productExists.ErrorMessage;

                            return View(discount);
                        }
                        
                    }

                    var discountSent = await _discountService.CreateAsync(discount);

                    return RedirectToAction(nameof(Index));                    
                }
                catch
                {
                    return View(discount);
                }
            }
            return View(discount);

        }

        // GET: DiscountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DiscountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }        

        // POST: DiscountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
           
                var result = _discountService.DeleteDiscount(id);

                if (!result.IsSuccess)
                {
                    TempData["FailureMessage"] = result.ErrorMessage;

                    return RedirectToAction(nameof(Index));
                }


            return RedirectToAction(nameof(Index));
           
           
           
        }
    }
}
