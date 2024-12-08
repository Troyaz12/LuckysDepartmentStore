using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Controllers
{
    public class DiscountController(IProductService _productService, IDiscountService _discountService) : Controller
    {
        // GET: DiscountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DiscountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                    }
                    else {
                        var discountSent = await _discountService.CreateAsync(discount);

                        return RedirectToAction(nameof(Index));
                    }
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

        // GET: DiscountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DiscountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
