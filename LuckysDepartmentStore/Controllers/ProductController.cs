using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Mvc;

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
            ProductCreateVM productVM = new ProductCreateVM();

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

            var sizes = _productService.GetSize();
            var allDetails = details.Data;

            for (int x = 0; x < allDetails.ColorProduct.Count; x++)
            {
                var selectedSize = sizes.FirstOrDefault(s => s.SizesID == allDetails.ColorProduct[x].SizeID);

                if (selectedSize != null)
                {
                    allDetails.ColorProduct[x].SizeName = selectedSize.Size;
                }
                else
                {
                    allDetails.ColorProduct[x].SizeName = "Size not found";
                }

            }

            return View(allDetails);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ProductCreateVM product = new ProductCreateVM();
            product.Color = _productService.GetColors();
            product.Category = _productService.GetCategory();
            product.SubCategory = _productService.GetSubCategory();
            product.Brand = _productService.GetBrand();
            product.Sizes = _productService.GetSize();

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

            var sizes = _productService.GetSize();            
            productEditVM.Sizes = sizes;

            for (int x = 0; x < productEditVM.ColorProduct.Count; x++)
            {
                var selectedSize = sizes.FirstOrDefault(s => s.SizesID == productEditVM.ColorProduct[x].SizeID);

                if (selectedSize != null)
                {
                    productEditVM.ColorProduct[x].SizeName = selectedSize.Size;
                }
                else
                {
                    productEditVM.ColorProduct[x].SizeName = "Size not found";
                }

            }

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
        public ActionResult UpdateList([FromBody] UpdateColorListVM colorModel)
        {                       
            return PartialView("_DynamicPartialList", colorModel.ColorProductList);
        }
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult UpdateListForEdit([FromBody] ColorProductVM product)
        {
            // productVM.ColorProduct.Add(product);
            productEditVM.ColorProduct.Add(product);

            return PartialView("_DynamicPartialListEdit", productEditVM);
        }
        [HttpPost]
        public ActionResult DeleteItem([FromBody] RemoveColorVM removeColor)
        {
            var index = removeColor.Index;
            var colorProducts = removeColor.CurrentList;

            if (index >= 0 && index <= colorProducts.Count)
            {
                removeColor.CurrentList.RemoveAt(index);
            }
            return PartialView("_DynamicPartialList", colorProducts);
        }
        [HttpPost]
        public ActionResult DeleteEditItem([FromBody] RemoveColorVM removeColor)
        {
            var index = removeColor.Index;
            var colorProducts = removeColor.CurrentList;

            if (index >= 0 && index <= colorProducts.Count)
            {
                productEditVM.ColorProduct.RemoveAt(index);
            }
            return PartialView("_DynamicPartialListEdit", colorProducts);
        }

    }
}
