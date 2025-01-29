using AutoMapper;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Controllers
{
    public class DiscountController(IProductService _productService, IDiscountService _discountService, IMapper _mapper) : Controller
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
        public async Task<ActionResult> Create(DiscountVM discount)
        {
            DiscountCreateVM product = new DiscountCreateVM();
            var category = await _productService.GetCategory();
            var subCategory = await _productService.GetSubCategory();
            var brand = await _productService.GetBrand();

            if (!category.IsSuccess || !subCategory.IsSuccess || !brand.IsSuccess)
            {
                TempData["FailureMessage"] = "Error getting product data.";

                return RedirectToAction("Index", "Error");
            }

            product.Category = category.Data;
            product.SubCategory = subCategory.Data;
            product.Brand = brand.Data;

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
                        var productExists = await _productService.GetDetails(discount.ProductID ?? 0);

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
        public async Task<ActionResult> EditAsync(int id)
        {
            var discount = _discountService.GetDiscount(id);
            var category = await _productService.GetCategory();
            var subCategory = await _productService.GetSubCategory();
            var brand = await _productService.GetBrand();

            if (!category.IsSuccess || !subCategory.IsSuccess || !brand.IsSuccess || !discount.IsSuccess)
            {
                TempData["FailureMessage"] = "Error getting discount data.";

                return RedirectToAction("Index", "Error");
            }

            var discountProducts = _mapper.Map<DiscountEditVM>(discount.Data);

            discountProducts.Category = category.Data;
            discountProducts.SubCategory = subCategory.Data;
            discountProducts.Brand = brand.Data;

            return View(discountProducts);
        }

        // POST: DiscountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(DiscountEditVM discount)
        {
            try
            {
                var discountEdited = await _discountService.UpdateDiscount(discount);


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
