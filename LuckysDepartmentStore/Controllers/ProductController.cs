using AutoMapper;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Emit;

namespace LuckysDepartmentStore.Controllers
{
    public class ProductController(IProductService _productService) : Controller
    {
        public static ProductCreateVM productVM = new ProductCreateVM();
        public static ProductEditVM productEditVM = new ProductEditVM();
        // GET: Product
        public ActionResult Index(string category, string searchString)
        {
            ProductListVM productList = new ProductListVM();

            var products = _productService.GetProductsSearchBar(category, searchString);
            productList.Products = products;

            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {

            var details = _productService.GetDetails(id);

            if(details.IsSuccess == false)
            {
                TempData["FailureMessage"] = details.ErrorMessage;

                return RedirectToAction(nameof(Index));
            }


            return View(details.Data);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ProductCreateVM product = new ProductCreateVM();
            product.Color = _productService.GetColors();
            product.Category = _productService.GetCategory();
            product.SubCategory = _productService.GetSubCategory();
            product.Brand = _productService.GetBrand();

            return View(product);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Price,Description,Quantity,ProductName,CategorySelection,BrandSelection," +
            "ColorSelection,SubCategoryID,ProductPictureFile,CreatedDate,Category,Color,ColorID,CategoryID," +
            "SubCategorySelection, ColorProduct,BrandID")] ProductCreateVM product)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var productSent = await _productService.CreateAsync(product);                    

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            productEditVM = _productService.GetAProduct(id);

            productEditVM.Color = _productService.GetColors();
            productEditVM.Category = _productService.GetCategory();
            productEditVM.SubCategory = _productService.GetSubCategory();
            productEditVM.Brand = _productService.GetBrand();

            productVM.ColorProduct = productEditVM.ColorProduct;

            return View(productEditVM);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(ProductEditVM product)
        {
            try
            {
                var editResult = await _productService.EditProduct(product);                

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }        

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var result = _productService.Delete(id);

                if (!result.IsSuccess)
                {
                    TempData["FailureMessage"] = result.ErrorMessage;

                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult UpdateList([FromBody] ColorProductVM product)
        {
            productVM.ColorProduct.Add(product);
                                
            return PartialView("_DynamicPartialList", productVM);
        }
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult UpdateListForEdit([FromBody] ColorProductVM product)
        {
            productVM.ColorProduct.Add(product);

            return PartialView("_DynamicPartialListEdit", productEditVM);
        }
        [HttpPost]
        public ActionResult DeleteItem(int index)
        {
            if (index >= 0 && index <= productVM.ColorProduct.Count)
            {
                productVM.ColorProduct.RemoveAt(index);
            }
            return PartialView("_DynamicPartialList", productVM);
        }
        [HttpPost]
        public ActionResult DeleteEditItem(int index)
        {
            if (index >= 0 && index <= productEditVM.ColorProduct.Count)
            {
                productEditVM.ColorProduct.RemoveAt(index);
            }
            return PartialView("_DynamicPartialListEdit", productEditVM);
        }




    }
}
