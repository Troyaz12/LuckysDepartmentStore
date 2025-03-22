using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Discount;
using LuckysDepartmentStore.Models.DTO.Home;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Utility = LuckysDepartmentStore.Utilities.Utility;

namespace LuckysDepartmentStore.Service
{
    public class ProductService : IProductService
    {
        private readonly LuckysContext _context;
        private readonly IMapper _mapper;
        private readonly IColorService _colorService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IBrandService _brandService;       
        private readonly IUtility _utility;
        private readonly IProductStore _productStore;
        private readonly IColorStore _colorStore;
        private readonly IRatingsStore _ratingsStore;
        private readonly IDiscountStore _discountStore;
            
        public ProductService(LuckysContext context, IMapper mapper, IColorService colorService, 
            ICategoryService categoryService, ISubCategoryService subCategoryService, IBrandService brandService,
            IUtility utility, IProductStore productStore, IColorStore colorStore, IRatingsStore ratingsStore, IDiscountStore discountStore)
        {
            _context = context;
            _mapper = mapper;
            _colorService = colorService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _brandService = brandService;
            _utility = utility;
            _productStore = productStore;
            _colorStore = colorStore;
            _ratingsStore = ratingsStore;
            _discountStore = discountStore;
        }
        public async Task<ExecutionResult<Product>> CreateAsync(ProductCreateVM product)
        {

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {

                try
                {
                    if ((product != null && product.ColorID == null))
                    {
                        for (int x = 0; x < product.ColorProduct.Count; x++)
                        {
                            if (product.ColorProduct[x].ColorID == 0 || product.ColorProduct[x].ColorID == null)
                            {
                                var colorIdEx = await _colorService.Create(product.ColorProduct[x].Name);

                                if (!colorIdEx.IsSuccess)
                                {                                    
                                    return ExecutionResult<Product>.Failure("Cannot create color.");
                                }

                                var colorId = colorIdEx.Data;
                                product.ColorProduct[x].ColorID = colorId;
                            }
                        }
                    }
                    if ((product != null && product.SizeID == null))
                    {
                        for (int x = 0; x < product.ColorProduct.Count; x++)
                        {
                            if (product.ColorProduct[x].SizeID == 0 || product.ColorProduct[x].SizeID == null)
                            {
                                var sizeIdRes = await _colorService.CreateSize(product.ColorProduct[x].SizeName);

                                if (!sizeIdRes.IsSuccess)
                                {
                                    // Rollback the transaction on error
                                    await transaction.RollbackAsync();
                                    return ExecutionResult<Product>.Failure("Cannot create size.");
                                }

                                product.ColorProduct[x].SizeID = sizeIdRes.Data;
                            }
                        }
                    }
                    if ((product != null && product.CategoryID == null) || product.CategoryID == 0)
                    {
                        var categoryRes = await _categoryService.Create(product);

                        if (!categoryRes.IsSuccess)
                        {
                            // Rollback the transaction on error
                            await transaction.RollbackAsync();
                            return ExecutionResult<Product>.Failure("Cannot create category.");
                        }

                        product.CategoryID = categoryRes.Data;
                    }

                    if ((product != null && product.SubCategoryID == null) || product.SubCategoryID == 0)
                    {
                        var subcategoryRes = await _subCategoryService.Create(product);

                        if (!subcategoryRes.IsSuccess)
                        {
                            // Rollback the transaction on error
                            await transaction.RollbackAsync();
                            return ExecutionResult<Product>.Failure("Cannot create subcategory.");
                        }

                        product.SubCategoryID = subcategoryRes.Data;
                    }


                    if ((product != null && product.BrandID == null) || product.BrandID == 0)
                    {
                        var brandRes = await _brandService.Create(product);

                        if (!brandRes.IsSuccess)
                        {
                            // Rollback the transaction on error
                            await transaction.RollbackAsync();
                            return ExecutionResult<Product>.Failure("Cannot create brand.");
                        }

                        product.BrandID = brandRes.Data;
                    }

                    var newProduct = _mapper.Map<Product>(product);

                    newProduct.ProductPicture = _utility.ImageBytes(product.ProductPictureFile);

                    _context.Add(newProduct);
                    await _context.SaveChangesAsync();

                    int productId = newProduct.ProductID;

                    var newColorProducts = product.ColorProduct
                       .Select(cp =>
                       {
                           var newColorProduct = _mapper.Map<ColorProduct>(cp);
                           newColorProduct.ProductID = productId;
                           return newColorProduct;
                       })
                       .ToList();

                    _context.AddRange(newColorProducts);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return ExecutionResult<Product>.Success(newProduct);
                }
                catch (DbUpdateException ex)
                {
                    return ExecutionResult<Product>.Failure($"Order failed: {ex.Message}");                   
                }
                catch (Exception ex)
                {
                    return ExecutionResult<Product>.Failure($"Order failed: {ex.Message}");
                }
            }
            });
        }
        public async Task<ExecutionResult<List<Color>>> GetColors()
        {
            try
            {
                var color = await _colorStore.AllColors();

                return ExecutionResult<List<Color>>.Success(color);

            }
            catch (Exception ex)
            {
                return ExecutionResult<List<Color>>.Failure("Unable to search products.");
            }
            
        }
        public async Task<ExecutionResult<List<Category>>> GetCategory()
        {
            try
            {
                var categories = await _productStore.Categories();

                return ExecutionResult<List<Category>>.Success(categories);
            }
            catch(Exception ex)
            {
                return ExecutionResult<List<Category>>.Failure("Unable to search categories.");
            }
        }
        public async Task<ExecutionResult<List<SubCategory>>> GetSubCategory()
        {
            try
            {
                var subCategory = await _productStore.SubCategories();

                return ExecutionResult<List<SubCategory>>.Success(subCategory);
            }
            catch(Exception ex)
            {
                return ExecutionResult<List<SubCategory>>.Failure("Unable to search Subcategories.");
            }        
        }
        public async Task<ExecutionResult<List<Brand>>> GetBrand()
        {
            try
            {
                var brands = await _productStore.Brand();

                return ExecutionResult<List<Brand>>.Success(brands);
            }
            catch (Exception ex)
            {
                return ExecutionResult<List<Brand>>.Failure("Unable to search brands.");
            }
        }

        public async Task<ExecutionResult<List<ProductVM>>> GetProductsSearchBar(string categorySearch, string searchString)
        {
            try
            {
               var products = await _productStore.AllProductAllInfoNoDiscount();

                if (!string.IsNullOrEmpty(categorySearch))
                {
                    products = products.Where(s => s.Category!.ToUpper().Contains(categorySearch.ToUpper())).ToList();
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    products = products.Where(s => s.ProductName!.ToUpper().Contains(searchString.ToUpper())).ToList();
                }

                var list = _mapper.Map<List<ProductVM>>(products);

                return ExecutionResult<List<ProductVM>>.Success(list);
            }
            catch (Exception ex)
            {
                return ExecutionResult<List<ProductVM>>.Failure("Unable to get products.");
            }
        }
        public async Task<ExecutionResult<List<ProductVM>>> GetProductsByDiscount(string? categorySelection, 
            string? subCategorySelection, string? brandSelection, int? productID, string? discountTags)
        {
            try
            {
                var products = await GetProductsWithDiscount(categorySelection,
                    subCategorySelection, brandSelection, discountTags);

                if (!products.IsSuccess)
                {
                    return ExecutionResult<List<ProductVM>>.Failure("Unable to search discounts.");
                }

                var productListVM = _mapper.Map<List<ProductVM>>(products.Data);

                foreach (ProductVM singleProduct in productListVM)
                {
                    singleProduct.ProductImage = _utility.BytesToImage(singleProduct.ProductPicture);

                    if (singleProduct.DiscountAmount != null && singleProduct.DiscountAmount > 0)
                    {
                        singleProduct.SalePrice = (decimal)(singleProduct.Price - singleProduct.DiscountAmount);

                    }
                    else if (singleProduct.DiscountPercent != null && singleProduct.DiscountPercent > 0)
                    {
                        singleProduct.SalePrice = (decimal)(singleProduct.Price - (singleProduct.Price * singleProduct.DiscountPercent));
                    }
                }

                return ExecutionResult<List<ProductVM>>.Success(productListVM);                
            }
            catch (Exception ex)
            {
                return ExecutionResult<List<ProductVM>>.Failure("Unable to search discounts.");
            }
        }
        public async Task<ExecutionResult<ProductEditVM>> GetAProduct(int productId)
        {
            try
            {
                var productDTO = await _productStore.GetProductByIDNoDiscount(productId);
                var colorProductDTO = await _colorStore.ColorsByProductID(productId);
                var productModel = _utility.MapEditProduct(productDTO);

                var colorProducts = _mapper.Map<List<ColorProductVM>>(colorProductDTO);

                productModel.ColorProduct = colorProducts;

                return ExecutionResult<ProductEditVM>.Success(productModel);
            }
            catch(Exception ex)
            {
                return ExecutionResult<ProductEditVM>.Failure("Unable to edit products.");
            }
        }
        public async Task<ExecutionResult<ProductEditVM>> EditProduct(ProductEditVM productEdit)
        {

            try
            {
                if (productEdit == null)
                {
                    return ExecutionResult<ProductEditVM>.Failure("Unable to edit product.");
                }

                var productOld = await _productStore.ProductByID(productEdit.ProductID);

                if (productOld == null)
                {
                    return ExecutionResult<ProductEditVM>.Failure("Product not found.");
                }

                productOld.Price = productEdit.Price;
                productOld.Quantity = productEdit.Quantity;
                productOld.Description = productEdit.Description;
                productOld.ProductName = productEdit.ProductName;
                productOld.CategoryID = (int) productEdit.CategoryID;
                productOld.BrandID = (int) productEdit.BrandID;
                productOld.SubCategoryID = (int) productEdit.SubCategoryID;
                productOld.DiscountTag = productEdit.DiscountTag;
                productOld.SearchWords = productEdit.SearchWords;

                if (productEdit.ProductPictureFile != null)
                {
                    productOld.ProductPicture = _utility.ImageBytes(productEdit.ProductPictureFile);
                }

                var colorProductsEdit = _mapper.Map<List<ColorProduct>>(productEdit.ColorProduct);                

                var colorProductsDB = _context.ColorProducts.Where(e => e.ProductID == productEdit.ProductID).ToList();

                var colorProductsToRemove = colorProductsDB
               .Where(cp => !colorProductsEdit.Any(p => p.ColorProductID == cp.ColorProductID) && cp.ColorProductID != 0)
               .ToList();

                var colorProductsToAdd = colorProductsEdit
                    .Where(cp => cp.ColorProductID == 0)
                    .Select(cp => { cp.ProductID = productEdit.ProductID; return cp; })
                    .ToList();

                // Delegate persistence to ProductStore
                await _productStore.UpdateProduct(productOld, colorProductsToAdd, colorProductsToRemove);

                return ExecutionResult<ProductEditVM>.Success(productEdit);
            }
            catch (Exception ex)
            {
                return ExecutionResult<ProductEditVM>.Failure("Unable to edit product.");
            }
        }

        public async Task<ExecutionResult<ProductDetailVM>> GetDetails(int productId)
        {
            var productDTO = await _productStore.GetProductByIDNoDiscount(productId);
            var colorProductDTO = await _colorStore.ColorsByProductID(productId);


            if (productDTO == null)
            {
                return ExecutionResult<ProductDetailVM>.Failure("Cannot find product in database. Product ID does not exist."); 
            }

            var productModel = _utility.MapDetailProduct(productDTO);

            var colorProducts = _mapper.Map<List<ColorProductVM>>(colorProductDTO);

            productModel.ProductImage = _utility.BytesToImage(productModel.ProductPicture);

            productModel.ColorProduct = colorProducts;

            return ExecutionResult<ProductDetailVM>.Success(productModel);
        }
        public async Task<ExecutionResult<int>> Delete(int productId)
        {
            try
            {
                bool deleted = await _productStore.DeleteProduct(productId);

                if (!deleted)
                {
                    return ExecutionResult<int>.Failure("An error occured. Cannot find delete product.");
                }
                

                return ExecutionResult<int>.Success(productId);
            }
            catch (Exception ex)
            {
                return ExecutionResult<int>.Failure("An error occured. Cannot find delete product.");
            }

        }
        public async Task<ExecutionResult<List<Sizes>>> GetSize()
        {
            try
            {
                var size = await _colorStore.AllSizes();

                return ExecutionResult<List<Sizes>>.Success(size);
            }
            catch (Exception ex)
            {
                return ExecutionResult<List<Sizes>>.Failure("An error occured. Cannot get sizes.");
            }
        }
        public async Task<ExecutionResult<ItemVM>> GetItem(int productId)
        {
            try {

                var productDTO = await _productStore.GetItem(productId);

                var colorProductDTO = await _colorStore.ColorsByProductID(productId);

                var ratingProductDTO = await _ratingsStore.RatingsByProductID(productId);

                if (productDTO == null || colorProductDTO == null || ratingProductDTO == null)
                {
                    return ExecutionResult<ItemVM>.Failure("Cannot find product in database. Product ID does not exist.");
                }

                var item = _mapper.Map<ItemVM>(productDTO);

                // check discount tag
                if (item.DiscountAmount > 0 || item.DiscountPercent > 0)
                {
                    var itemResult = await CalculateDiscount(item);

                    if (!itemResult.IsSuccess)
                    {
                        return ExecutionResult<ItemVM>.Failure("Unable to calculate discount.");
                    }
                    item =itemResult.Data;
                }

                var colorProducts = _mapper.Map<List<ColorProductVM>>(colorProductDTO);

                var sortedList = colorProducts
                    .OrderBy(cp => cp.ColorID ?? int.MaxValue)
                    .ThenBy(cp => cp.SizeID ?? int.MaxValue)
                    .ToList();

                if (ratingProductDTO != null)
                {
                    var ratings = _mapper.Map<List<RatingVM>>(ratingProductDTO);
                    item.RatingsCount = ratings.Count;
                    item.Stars = _utility.ItemRating(ratings);
                }

                if (item.ProductPicture != null)
                {
                    item.ProductImage = _utility.BytesToImage(item.ProductPicture);
                }

                item.ColorProduct = colorProducts;

                foreach (var colors in colorProducts)
                {
                    if (colors.SizeName != null && !item.Sizes.Any(p => p.Size == colors.SizeName))
                    {
                        Sizes newSize = new Sizes();
                        newSize.Size = colors.SizeName;

                        if (colors.SizeID.HasValue) {
                            newSize.SizesID = (int)colors.SizeID;
                        }
                        else
                        {
                            newSize.SizesID = 0;
                        }

                        item.Sizes.Add(newSize);
                    }
                }

                return ExecutionResult<ItemVM>.Success(item);

            }
            catch(Exception e)
            {
                return ExecutionResult<ItemVM>.Failure("Unable to retrieve item.");
            }
        }
        public async Task<ExecutionResult<List<ProductVM>>> GetProductsSearch(string? categorySelection,
           string? subCategorySelection, string? brandSelection, int? productID, string? searchString)
        {
            try
            {
                if (categorySelection != null || subCategorySelection != null || brandSelection != null)
                {
                    var productSearch = await _productStore.GetProductsSearch(categorySelection, subCategorySelection, brandSelection, searchString);

                    return ExecutionResult<List<ProductVM>>.Success(productSearch);
                }
                else if (productID != null)
                {
                    var productSearch = await _productStore.GetProductDiscountByIDSearch(productID);

                    return ExecutionResult<List<ProductVM>>.Success(productSearch);

                }
                else if (searchString != null)
                {
                    var productSearch = await _productStore.GetProductSearchString(searchString);

                    return ExecutionResult<List<ProductVM>>.Success(productSearch);
                }
                return ExecutionResult<List<ProductVM>>.Failure("No search information provided.");

            }
            catch (Exception ex)
            {
                return ExecutionResult<List<ProductVM>>.Failure("Unable to search products.");
            }
        }
        public async Task<ExecutionResult<ItemVM>> CalculateDiscount(ItemVM item)
        {
            try
            {
                // check for other discounts
                if (!string.IsNullOrEmpty(item.DiscountTag)) {
                    var discountTagsArray = item.DiscountTag.Split(',').Select(k => k.ToUpper().Trim()).ToList();
                    var discountDTO = await (
                        from Discount in _context.Discounts
                        where discountTagsArray.Any(k => EF.Functions.Like(Discount.DiscountTag.ToUpper(), "%" + k + "%"))
                        select Discount).ToListAsync();

                    List<Discount> discountList = new List<Discount>();

                    foreach (var itemDiscount in discountDTO)
                    {
                        if (itemDiscount.DiscountTag.Equals(item.DiscountTag))
                        {
                            discountList.Add(itemDiscount);
                        }
                    }

                    var totalDiscountAmount = discountList.Sum(discount => discount.DiscountAmount);
                    var totalDiscountPercent = discountList.Sum(discount => discount.DiscountPercent);

                    item.DiscountAmount = totalDiscountAmount;
                    item.DiscountPercent = totalDiscountPercent;
                }

                item.SalePrice = _utility.CalculateSalePrice(item.DiscountAmount, item.DiscountPercent, item.Price);

                return ExecutionResult<ItemVM>.Success(item);
            }
            catch (Exception ex)
            {
                return ExecutionResult<ItemVM>.Failure("Unable to search products.");
            }
            
        }
        public async Task<ExecutionResult<List<ProductVmDTO>>> GetProductsWithDiscount(string? categorySelection,
            string? subCategorySelection, string? brandSelection, string? discountTags)
        {
            try
            {
                List<ProductVmDTO> products = new List<ProductVmDTO>();

                if (subCategorySelection != null || brandSelection != null || categorySelection != null)
                {
                    products = await _productStore.ProductsWithDiscountsSelection();

                }

                List<ProductVmDTO> productsWithDTags = new List<ProductVmDTO>();
                var filteredProducts = new List<ProductVmDTO>();
                if (discountTags != null)
                {
                    var discountTagsArray = discountTags.Split(',')
                                                         .Select(k => k.ToUpper().Trim())
                                                         .ToList();

                    // Get all discounts and split DiscountTag outside of the query
                    var allDiscounts = await _discountStore.DiscountWithTag();

                    var discounts =
                        allDiscounts
                        .Where(d => d.DiscountTag.Split(',')
                                                  .Select(tag => tag.ToUpper().Trim())
                                                  .Intersect(discountTagsArray)
                                                  .Any())
                        .ToList();

                    var discountTagLookup = discounts
                        .ToDictionary(d => d.DiscountTag.ToUpper().Trim(), d => d);


                    productsWithDTags = await _productStore.ProductsWithDiscountsByTag(discountTagsArray);

                    for (int x = 0; x < productsWithDTags.Count(); x++)
                    {
                        var productDTagsArray = productsWithDTags[x].DiscountTag.Split(',').Select(k => k.ToUpper().Trim()).ToList();
                        bool hasMatchingDiscount = false;

                        foreach (var tag in productDTagsArray)
                        {
                            if (discountTagLookup.TryGetValue(tag, out var discount))
                            {
                                productsWithDTags[x].DiscountAmount = (productsWithDTags[x].DiscountAmount ?? 0) + discount.DiscountAmount;
                                productsWithDTags[x].DiscountPercent = (productsWithDTags[x].DiscountPercent ?? 0) + discount.DiscountPercent;
                                hasMatchingDiscount = true;
                            }
                        }
                        if (hasMatchingDiscount)
                        {
                            filteredProducts.Add(productsWithDTags[x]);
                        }

                    }
                }

                var combinedProducts = products
                           .Concat(filteredProducts)
                           .GroupBy(p => p.ProductID)
                           .Select(g => new ProductVmDTO
                           {
                               ProductID = g.Key,
                               ProductName = g.First().ProductName,
                               Price = g.First().Price,
                               Description = g.First().Description,
                               Quantity = g.First().Quantity,
                               Category = g.First().Category,
                               SubCategory = g.First().SubCategory,
                               Brand = g.First().Brand,
                               CreatedDate = g.First().CreatedDate,
                               ProductPicture = g.First().ProductPicture,
                               DiscountAmount = g.Sum(x => x.DiscountAmount ?? 0),
                               DiscountPercent = g.Sum(x => x.DiscountPercent ?? 0),
                               DiscountTag = g.First().DiscountTag
                           })
                           .ToList();

                return ExecutionResult<List<ProductVmDTO>>.Success(combinedProducts);

            }
            catch (Exception ex)
            {
                return ExecutionResult<List<ProductVmDTO>>.Failure("Unable to get discounts.");
            }
        }
    }
}