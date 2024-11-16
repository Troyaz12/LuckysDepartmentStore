using AutoMapper;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LuckysDepartmentStore.Controllers
{
    public class ProductController(IProductService _productService) : Controller
    {
        // GET: Product
        public ActionResult Index()
        {

            ProductListVM productList = new ProductListVM();
            List<ProductVM> products = new List<ProductVM>();
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
            ProductVM product = new ProductVM();
            product.Color = _productService.GetColors();
            product.Category = _productService.GetCategory();
            product.SubCategory = _productService.GetSubCategory();

            return View(product);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ProductID,Price,Description,Quantity,ProductName,CategorySelection,BrandSelection," +
            "ColorSelection,SubCategoryId,ProductPicture,CreatedDate,Category,Color,ColorId,SubCategory,CategoryId," +
            "SubCategorySelection")] ProductVM product)
        {
            try
            {
                var productSent = _productService.Create(product);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
    }
}
