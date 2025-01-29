using LuckysDepartmentStore.Migrations;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace LuckysDepartmentStore.Controllers
{
    public class ProductController(IProductService _productService) : Controller
    {
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
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var details = await _productService.GetDetails(id);
            var sizes = await _productService.GetSize();

            if (!details.IsSuccess || !sizes.IsSuccess)
            {
                TempData["FailureMessage"] = "Error getting discount data.";
                return RedirectToAction("Index", "Error");
            }

            
            var allDetails = details.Data;

            for (int x = 0; x < allDetails.ColorProduct.Count; x++)
            {
                var selectedSize = sizes.Data.FirstOrDefault(s => s.SizesID == allDetails.ColorProduct[x].SizeID);

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
        public async Task<ActionResult> CreateAsync()
        {
            ProductCreateVM product = new ProductCreateVM();

            try
            {
                var color = await _productService.GetColors();
                var category = await _productService.GetCategory();
                var subCategory = await _productService.GetSubCategory();
                var brand = await _productService.GetBrand();
                var sizes = await _productService.GetSize();

                if (!color.IsSuccess || !category.IsSuccess || !subCategory.IsSuccess || !brand.IsSuccess || !sizes.IsSuccess)
                {
                    TempData["FailureMessage"] = "Error getting discount data.";
                    return RedirectToAction("Index", "Error");
                }

                product.Color = color.Data;
                product.Category = category.Data;
                product.SubCategory = subCategory.Data;
                product.Brand = brand.Data;
                product.Sizes = sizes.Data;
            }
            catch (Exception ex) 
            {
                TempData["FailureMessage"] = "Error getting product data.";

                return RedirectToAction("Index", "Error");

            }            

            return View(product);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Price,Description,Quantity,ProductName,CategorySelection,BrandSelection," +
            "ColorSelection,SubCategoryID,ProductPictureFile,CreatedDate,Category,Color,ColorID,CategoryID," +
            "SubCategorySelection, ColorProduct,BrandID, DiscountTags, SearchWords")] ProductCreateVM product)
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
        public async Task<ActionResult> EditAsync(int id)
        {
            try
            {
                var productEditVM = await _productService.GetAProduct(id);

                var colors = await _productService.GetColors();
                var category = await _productService.GetCategory();
                var subCategory = await _productService.GetSubCategory();
                var brand = await _productService.GetBrand();
                var sizes = await _productService.GetSize();

                if (colors.IsSuccess)
                {
                    productEditVM.Data.Color = colors.Data;
                }

                if (category.IsSuccess)
                {
                    productEditVM.Data.Category = category.Data;
                }

                if (subCategory.IsSuccess)
                {
                    productEditVM.Data.SubCategory = subCategory.Data;
                }

                if (brand.IsSuccess)
                {
                    productEditVM.Data.Brand = brand.Data;
                }

                if (sizes.IsSuccess)
                {
                    productEditVM.Data.Sizes = sizes.Data;
                }                 

                for (int x = 0; x < productEditVM.Data.ColorProduct.Count; x++)
                {
                    var selectedSize = sizes.Data.FirstOrDefault(s => s.SizesID == productEditVM.Data.ColorProduct[x].SizeID);

                    if (selectedSize != null)
                    {
                        productEditVM.Data.ColorProduct[x].SizeName = selectedSize.Size;
                    }
                    else
                    {
                        productEditVM.Data.ColorProduct[x].SizeName = "Size not found";
                    }

                }

                return View(productEditVM.Data);
            }
            catch (Exception ex)
            {
                TempData["FailureMessage"] = "Error getting product data.";

                return RedirectToAction("Index", "Error");
            }            
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
        public ActionResult UpdateListForEdit([FromBody] UpdateColorListVM product)
        {
            return PartialView("_DynamicPartialListEdit", product.ColorProductList);
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
          
            return PartialView("_DynamicPartialListEdit", colorProducts);
        }

    }
}
