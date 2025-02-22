using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace LuckysDepartmentStore.Controllers
{
    public class ProductController(IProductService _productService) : Controller
    {
        // GET: Product
        [HttpGet]
        public async Task<ActionResult> Index(string category, string searchString)
        {
            ProductListVM productList = new ProductListVM();

            var products = await _productService.GetProductsSearchBar(category, searchString);

            if (!products.IsSuccess)
            {
                TempData["FailureMessage"] = products.ErrorMessage;
                return RedirectToAction("Index", "Error");
            }

            productList.Products = products.Data;

            return View(productList);
        }

        // GET: Product/Details/5
        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            if (id == 0)
            {
                TempData["FailureMessage"] = "Error getting details.";
                return RedirectToAction("Index", "Error");
            }

            var details = await _productService.GetDetails(id);
            var sizes = await _productService.GetSize();

            if (!details.IsSuccess || !sizes.IsSuccess)
            {
                TempData["FailureMessage"] = "Error getting details.";
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
        [HttpGet]
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
                    TempData["FailureMessage"] = "Error creating product.";
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
                TempData["FailureMessage"] = "Error creating product.";

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
                product.ColorProduct
                    .Where(cp => cp.ColorID == 0)
                    .ToList()
                    .ForEach(cp => cp.ColorID = null);

                var productSent = await _productService.CreateAsync(product);

                if (!productSent.IsSuccess)
                {
                    TempData["FailureMessage"] = productSent.ErrorMessage;

                    return RedirectToAction("Index", "Error");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        [HttpGet]
        public async Task<ActionResult> EditAsync(int id)
        {
            if (id == 0)
            {
                TempData["FailureMessage"] = "Error Editing Product. No id found.";
                return RedirectToAction("Index", "Error");
            }

            try
            {
                var productEditVM = await _productService.GetAProduct(id);

                var colors = await _productService.GetColors();
                var category = await _productService.GetCategory();
                var subCategory = await _productService.GetSubCategory();
                var brand = await _productService.GetBrand();
                var sizes = await _productService.GetSize();

                if (!productEditVM.IsSuccess || !colors.IsSuccess || !category.IsSuccess ||
                    !subCategory.IsSuccess || !brand.IsSuccess || !sizes.IsSuccess)
                {
                    TempData["FailureMessage"] = "Error editing product data.";

                    return RedirectToAction("Index", "Error");
                }

                productEditVM.Data.Color = colors.Data;
                productEditVM.Data.Category = category.Data;
                productEditVM.Data.SubCategory = subCategory.Data;
                productEditVM.Data.Brand = brand.Data;
                productEditVM.Data.Sizes = sizes.Data;

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
                TempData["FailureMessage"] = "Error editing product data.";

                return RedirectToAction("Index", "Error");
            }            
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(ProductEditVM product)
        {
            if (ModelState.IsValid)
            {
                var editResult = await _productService.EditProduct(product);

                if (!editResult.IsSuccess)
                {
                    TempData["FailureMessage"] = editResult.ErrorMessage;

                    return RedirectToAction("Index", "Error");
                }

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }        

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            if (id == 0)
            {
                TempData["FailureMessage"] = "Error deleting products.";
                return RedirectToAction("Index", "Error");
            }

            var result = await _productService.Delete(id);

            if (!result.IsSuccess)
            {
                TempData["FailureMessage"] = result.ErrorMessage;

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));           
        }

        [HttpPost]
        public ActionResult UpdateList([FromBody] UpdateColorListVM colorModel)
        {                       
            return PartialView("_DynamicPartialList", colorModel.ColorProductList);
        }
        [HttpPost]
        public ActionResult UpdateListForEdit([FromBody] UpdateColorListVM product)
        {
            return PartialView("_DynamicPartialListEdit", product.ColorProductList);
        }
        [HttpPost]
        public ActionResult DeleteItem([FromBody] RemoveColorVM removeColor)
        {
            try
            {
                var index = removeColor.Index;
                var colorProducts = removeColor.CurrentList;

                if (index >= 0 && index <= colorProducts.Count)
                {
                    removeColor.CurrentList.RemoveAt(index);
                }
                return PartialView("_DynamicPartialList", colorProducts);
            }
            catch (Exception ex)
            {
                TempData["FailureMessage"] = "Error deleting item.";

                return RedirectToAction(nameof(Index));
            }
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
