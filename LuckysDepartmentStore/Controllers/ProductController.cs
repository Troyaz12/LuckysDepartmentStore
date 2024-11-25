using AutoMapper;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LuckysDepartmentStore.Controllers
{
    public class ProductController(IProductService _productService) : Controller
    {
        public static ProductCreateVM productVM = new ProductCreateVM();
        // GET: Product
        public ActionResult Index()
        {
            ProductListVM productList = new ProductListVM();

            var products = _productService.GetProducts();
            productList.Products = products;

            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
            "ColorSelection,SubCategoryID,ProductPicture,CreatedDate,Category,Color,ColorID,CategoryID," +
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
            return View();
        }

        // POST: Product/Edit/5
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

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult UpdateList([FromBody] ColorProductVM product)
        {
            productVM.ColorProduct.Add(product);
                                
            return PartialView("_DynamicPartialList", productVM);
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
    }
}
