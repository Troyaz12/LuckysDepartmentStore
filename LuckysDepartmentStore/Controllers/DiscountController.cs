using AutoMapper;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace LuckysDepartmentStore.Controllers
{
    public class DiscountController(IProductService _productService, IDiscountService _discountService, IMapper _mapper) : Controller
    {
        // GET: DiscountController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var discounts = await _discountService.GetActiveDiscounts();

            if (!discounts.IsSuccess)
            {
                TempData["FailureMessage"] = discounts.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }

            return View(discounts.Data);
        }

        // GET: DiscountController/Details/5
        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int id)
        {

            var discount = await _discountService.GetDiscount(id);

            if (!discount.IsSuccess)
            {
                TempData["FailureMessage"] = discount.ErrorMessage;

                return RedirectToAction("Index", "Error");
            }



            return View(discount.Data);
        }

        // GET: DiscountController/Create
        [HttpGet]
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
                            TempData["FailureMessage"] = productExists.ErrorMessage;

                            return RedirectToAction("Index", "Error");
                        }
                        
                    }

                    var discountSent = await _discountService.CreateAsync(discount);

                    return RedirectToAction(nameof(Index));                    
                }
                catch(Exception ex)
                {
                    TempData["FailureMessage"] = "Error creating discount.";

                    return RedirectToAction("Index", "Error");
                }
            }
            return View(discount);

        }

        // GET: DiscountController/Edit/5
        [HttpGet]
        public async Task<ActionResult> EditAsync(int id)
        {
            var discount = await _discountService.GetDiscount(id);
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

                if (!discountEdited.IsSuccess)
                {
                    TempData["FailureMessage"] = discountEdited.ErrorMessage;
                    return RedirectToAction("Index", "Error");
                }

                return RedirectToAction("Index", "Discount");
            }
            catch
            {
                return View();
            }
        }        

        // POST: DiscountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            var result = await _discountService.DeleteDiscount(id);
          
            if (!result.IsSuccess || !result.IsSuccess)
            {
                TempData["FailureMessage"] = result.ErrorMessage;
                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
