﻿using AutoMapper;
using LuckysDepartmentStore.Data;
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
        public LuckysContext _context;
        public IMapper _mapper;
        public IColorService _colorService;
        public ICategoryService _categoryService;
        public ISubCategoryService _subCategoryService;
        public IBrandService _brandService;       
        private readonly Utility _utility;

        public ProductService(LuckysContext context, IMapper mapper, IColorService colorService, 
            ICategoryService categoryService, ISubCategoryService subCategoryService, IBrandService brandService
            , Utility utility)
        {
            _context = context;
            _mapper = mapper;
            _colorService = colorService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _brandService = brandService;
            _utility = utility;
        }
        public async Task<ExecutionResult<Product>> CreateAsync(ProductCreateVM product)
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

                    //foreach (ColorProductVM colors in product.ColorProduct)
                    //{
                    //    var newColorProduct = _mapper.Map<ColorProduct>(colors);
                    //    newColorProduct.ProductID = productId;

                    //    _context.Add(newColorProduct);

                    //}

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
        }
        public async Task<ExecutionResult<List<Color>>> GetColors()
        {
            try
            {
                var color = await _context.Colors
               .ToListAsync();

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
                var categories = await _context.Categories
              .ToListAsync();

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
                var subCategory = await _context.SubCategories
                .ToListAsync();

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
                var brands = await _context.Brand
                .ToListAsync();

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
                var products = await (
                    from Product in _context.Products
                    join Category in _context.Categories on Product.CategoryID equals Category.CategoryID
                    join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID
                    join Brand in _context.Brand on Product.BrandID equals Brand.BrandId
                    select new ProductVmDTO
                    {
                        ProductID = Product.ProductID,
                        ProductName = Product.ProductName,
                        Price = Product.Price,
                        Description = Product.Description,
                        Quantity = Product.Quantity,
                        Category = Category.CategoryName,
                        SubCategory = SubCategory.SubCategoryName,
                        Brand = Brand.BrandName,
                        CreatedDate = Product.CreatedDate,
                    }).ToListAsync();

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
                var productDTO = await(
                from Product in _context.Products
                join Category in _context.Categories on Product.CategoryID equals Category.CategoryID
                join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID
                join Brand in _context.Brand on Product.BrandID equals Brand.BrandId
                where Product.ProductID == productId
                select new ProductEditDTO
                {
                    ProductID = Product.ProductID,
                    ProductName = Product.ProductName,
                    Price = Product.Price,
                    Description = Product.Description,
                    Quantity = Product.Quantity,
                    Category = Category.CategoryName,
                    SubCategory = SubCategory.SubCategoryName,
                    Brand = Brand.BrandName,
                    CreatedDate = Product.CreatedDate,
                    BrandId = Product.BrandID,
                    CategoryId = Category.CategoryID,
                    SubCategoryId = SubCategory.SubCategoryID,
                    DiscountTags = Product.DiscountTag,
                    SearchWords = Product.SearchWords
                }).FirstOrDefaultAsync();

                var colorProductDTO = await(
                    from ColorProducts in _context.ColorProducts
                    join Colors in _context.Colors on ColorProducts.ColorID equals Colors.ColorID
                    where ColorProducts.ProductID == productId
                    select new ColorProductEditDTO
                    {
                        ProductID = ColorProducts.ProductID,
                        ColorID = ColorProducts.ColorID,
                        Quantity = ColorProducts.Quantity,
                        Name = Colors.Name,
                        ColorProductID = ColorProducts.ColorProductID,
                        SizeID = ColorProducts.SizeID
                    }).ToListAsync();

             //   var product = productDTO.FirstOrDefault();

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

                var productOld = await _context.Products.FindAsync(productEdit.ProductID);

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

                var productID = productEdit.ProductID;

                var colorProductsDB = _context.ColorProducts.Where(e => e.ProductID == productID).ToList();

                // check for product colors that have been removed
                foreach (ColorProduct colorProduct in colorProductsDB)
                {
                    if (!colorProductsEdit.Any(p => p.ColorProductID == colorProduct.ColorProductID) && colorProduct.ColorProductID != 0)
                    {
                        _context.ColorProducts.Remove(colorProduct);
                    }
                }

                // check for product colors that have been added
                foreach (ColorProduct colorProduct in colorProductsEdit)
                {
                    if (colorProduct.ColorProductID == 0)
                    {
                        colorProduct.ProductID = productID;
                        _context.ColorProducts.Add(colorProduct);
                    }
                }

                var productSave = _context.SaveChanges();

                return ExecutionResult<ProductEditVM>.Success(productEdit);
            }
            catch (Exception ex)
            {
                return ExecutionResult<ProductEditVM>.Failure("Unable to edit product.");
            }
        }

        public async Task<ExecutionResult<ProductDetailVM>> GetDetails(int productId)
        {          

            var productDTO = await(
             from Product in _context.Products
             join Category in _context.Categories on Product.CategoryID equals Category.CategoryID
             join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID
             join Brand in _context.Brand on Product.BrandID equals Brand.BrandId
             where Product.ProductID == productId
             select new ProductDetailDTO
             {
                 ProductID = Product.ProductID,
                 ProductName = Product.ProductName,
                 Price = Product.Price,
                 Description = Product.Description,
                 Quantity = Product.Quantity,
                 Category = Category.CategoryName,
                 SubCategory = SubCategory.SubCategoryName,
                 Brand = Brand.BrandName,
                 CreatedDate = Product.CreatedDate,
                 BrandId = Product.BrandID,
                 CategoryId = Category.CategoryID,
                 SubCategoryId = SubCategory.SubCategoryID,
                 ProductPicture = Product.ProductPicture,
                 DiscountTags = Product.DiscountTag,
                 SearchWords = Product.SearchWords

             }).FirstOrDefaultAsync();

            var colorProductDTO = await(
                from ColorProducts in _context.ColorProducts
                join Colors in _context.Colors on ColorProducts.ColorID equals Colors.ColorID
                where ColorProducts.ProductID == productId
                select new ColorProductDetailDTO
                {
                    ProductID = ColorProducts.ProductID,
                    ColorID = ColorProducts.ColorID,
                    Quantity = ColorProducts.Quantity,
                    Name = Colors.Name,
                    ColorProductID = ColorProducts.ColorProductID,
                    SizeID  = ColorProducts.SizeID
                }).ToListAsync();            

            if(productDTO == null)
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
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == productId);
                if (product == null)
                {
                    return ExecutionResult<int>.Failure("An error occured. Cannot find delete product.");
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

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
                var size = await _context.Sizes
                .ToListAsync();

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

                var productDTO = await (
                    from Product in _context.Products
                    join Category in _context.Categories on Product.CategoryID equals Category.CategoryID into categories
                    from Category in categories.DefaultIfEmpty()
                    join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID into subCategories
                    from SubCategory in subCategories.DefaultIfEmpty()
                    join Brand in _context.Brand on Product.BrandID equals Brand.BrandId into Brands
                    from Brand in Brands.DefaultIfEmpty()
                    join DiscountsByBrand in _context.Discounts on Product.BrandID equals DiscountsByBrand.BrandID into DiscountBrand
                    from DiscountsByBrand in DiscountBrand.DefaultIfEmpty()
                    join DiscountsByCategory in _context.Discounts on Product.CategoryID equals DiscountsByCategory.CategoryID into DiscountCategory
                    from DiscountsByCategory in DiscountCategory.DefaultIfEmpty()
                    join DiscountsByProduct in _context.Discounts on Product.ProductID equals DiscountsByProduct.ProductID into DiscountProduct
                    from DiscountsByProduct in DiscountProduct.DefaultIfEmpty()
                    join DiscountsBySubcategory in _context.Discounts on Product.SubCategoryID equals DiscountsBySubcategory.SubCategoryID into DiscountSubCategory
                    from DiscountsBySubcategory in DiscountSubCategory.DefaultIfEmpty()
                    where productId == Product.ProductID

                    select new ItemDTO
                    {
                        ProductID = Product.ProductID,
                        ProductName = Product.ProductName,
                        Price = Product.Price,
                        Description = Product.Description,
                        Quantity = Product.Quantity,
                        Category = Category.CategoryName,
                        SubCategory = SubCategory.SubCategoryName,
                        Brand = Brand.BrandName,
                        CreatedDate = Product.CreatedDate,
                        ProductPicture = Product.ProductPicture,
                        DiscountAmount = (decimal?)DiscountsByBrand.DiscountAmount ?? (decimal?)DiscountsByCategory.DiscountAmount ?? (decimal?)DiscountsByProduct.DiscountAmount ?? (decimal?)DiscountsBySubcategory.DiscountAmount,
                        DiscountPercent = (decimal?)DiscountsByBrand.DiscountPercent ?? (decimal?)DiscountsByCategory.DiscountPercent ?? (decimal?)DiscountsByProduct.DiscountPercent ?? (decimal?)DiscountsBySubcategory.DiscountPercent,
                        DiscountTag = Product.DiscountTag
                    }).FirstOrDefaultAsync();

                var colorProductDTO = await (
                    from ColorProducts in _context.ColorProducts
                    join Colors in _context.Colors on ColorProducts.ColorID equals Colors.ColorID
                    join Sizes in _context.Sizes on ColorProducts.SizeID equals Sizes.SizesID
                    where ColorProducts.ProductID == productId
                    select new ColorProductItemDTO
                    {
                        ProductID = ColorProducts.ProductID,
                        ColorID = ColorProducts.ColorID,
                        Quantity = ColorProducts.Quantity,
                        Name = Colors.Name,
                        ColorProductID = ColorProducts.ColorProductID,
                        SizeID = ColorProducts.SizeID,
                        SizeName = Sizes.Size
                    }).ToListAsync();

                var ratingProductDTO = await (
                   from Ratings in _context.Ratings
                   where Ratings.ProductID == productId
                   select new RatingsDTO
                   {
                       ProductID = Ratings.ProductID,
                       RatingID = Ratings.RatingID,
                       RatingValue = Ratings.RatingValue,
                       CreatedDate = Ratings.CreatedDate
                   }).ToListAsync();

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
        //public async Task<ExecutionResult<List<ProductVmDTO>>> GetProductsWithDiscount()
        //{
        //    try
        //    {
        //        var products = await
        //            (from Product in _context.Products
        //             join Category in _context.Categories on Product.CategoryID equals Category.CategoryID into categories
        //             from Category in categories.DefaultIfEmpty()
        //             join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID into subCategories
        //             from SubCategory in subCategories.DefaultIfEmpty()
        //             join Brand in _context.Brand on Product.BrandID equals Brand.BrandId into Brands
        //             from Brand in Brands.DefaultIfEmpty()
        //             join DiscountsByBrand in _context.Discounts on Product.BrandID equals DiscountsByBrand.BrandID into DiscountBrand
        //             from DiscountsByBrand in DiscountBrand.DefaultIfEmpty()
        //             join DiscountsByCategory in _context.Discounts on Product.CategoryID equals DiscountsByCategory.CategoryID into DiscountCategory
        //             from DiscountsByCategory in DiscountCategory.DefaultIfEmpty()
        //             join DiscountsByProduct in _context.Discounts on Product.ProductID equals DiscountsByProduct.ProductID into DiscountProduct
        //             from DiscountsByProduct in DiscountProduct.DefaultIfEmpty()
        //             join DiscountsBySubcategory in _context.Discounts on Product.SubCategoryID equals DiscountsBySubcategory.SubCategoryID into DiscountSubCategory
        //             from DiscountsBySubcategory in DiscountSubCategory.DefaultIfEmpty()
        //             where DiscountsByBrand != null || DiscountsByCategory != null || DiscountsByProduct != null || DiscountsBySubcategory != null

        //             select new ProductVmDTO
        //             {
        //                 ProductID = Product.ProductID,
        //                 ProductName = Product.ProductName,
        //                 Price = Product.Price,
        //                 Description = Product.Description,
        //                 Quantity = Product.Quantity,
        //                 Category = Category.CategoryName,
        //                 SubCategory = SubCategory.SubCategoryName,
        //                 Brand = Brand.BrandName,
        //                 CreatedDate = Product.CreatedDate,
        //                 ProductPicture = Product.ProductPicture,
        //                 DiscountAmount = (decimal?)DiscountsByBrand.DiscountAmount ?? (decimal?)DiscountsByCategory.DiscountAmount ?? (decimal?)DiscountsByProduct.DiscountAmount ?? (decimal?)DiscountsBySubcategory.DiscountAmount,
        //                 DiscountPercent = (decimal?)DiscountsByBrand.DiscountPercent ?? (decimal?)DiscountsByCategory.DiscountPercent ?? (decimal?)DiscountsByProduct.DiscountPercent ?? (decimal?)DiscountsBySubcategory.DiscountPercent,
        //                 DiscountTag = Product.DiscountTag
        //             }).ToListAsync();

        //        // get all discounts with tags
        //        var discountTagged = await (
        //                   from Discounts in _context.Discounts
        //                   where Discounts.DiscountActive == true && !string.IsNullOrEmpty(Discounts.DiscountTag)
        //                   select new DiscountDTO
        //                   {
        //                       DiscountPercent = Discounts.DiscountPercent,
        //                       DiscountAmount = Discounts.DiscountAmount,
        //                       DiscountActive = Discounts.DiscountActive,
        //                       DiscountTag = Discounts.DiscountTag
        //                   }).ToListAsync();

        //        var discountTagLookup = discountTagged
        //                    .ToDictionary(d => d.DiscountTag.ToUpper().Trim(), d => d);

        //        var productsWithDTags = await
        //                     (from Product in _context.Products
        //                      join Category in _context.Categories on Product.CategoryID equals Category.CategoryID into categories
        //                      from Category in categories.DefaultIfEmpty()
        //                      join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID into subCategories
        //                      from SubCategory in subCategories.DefaultIfEmpty()
        //                      join Brand in _context.Brand on Product.BrandID equals Brand.BrandId into Brands
        //                      from Brand in Brands.DefaultIfEmpty()
        //                      where !string.IsNullOrEmpty(Product.DiscountTag)
        //                      select new ProductVmDTO
        //                      {
        //                          ProductID = Product.ProductID,
        //                          ProductName = Product.ProductName,
        //                          Price = Product.Price,
        //                          Description = Product.Description,
        //                          Quantity = Product.Quantity,
        //                          Category = Category.CategoryName,
        //                          SubCategory = SubCategory.SubCategoryName,
        //                          Brand = Brand.BrandName,
        //                          CreatedDate = Product.CreatedDate,
        //                          ProductPicture = Product.ProductPicture,
        //                          DiscountTag = Product.DiscountTag
        //                      }).ToListAsync();


        //        for (int x = 0; x < productsWithDTags.Count(); x++)
        //        {
        //            var productDTagsArray = productsWithDTags[x].DiscountTag.Split(',').Select(k => k.ToUpper().Trim()).ToList();

        //            foreach (var tag in productDTagsArray)//(var discount in discountTagged)
        //            {
        //                if (discountTagLookup.TryGetValue(tag, out var discount)) //(productDTagsArray.Contains(discount.DiscountTag.ToUpper().Trim()))
        //                {
        //                    productsWithDTags[x].DiscountAmount = (productsWithDTags[x].DiscountAmount ?? 0) + discount.DiscountAmount;
        //                    productsWithDTags[x].DiscountPercent = (productsWithDTags[x].DiscountPercent ?? 0) + discount.DiscountPercent;

        //                }

        //            }

        //        }

        //        var combinedProducts = products
        //                    .Concat(productsWithDTags)
        //                    .GroupBy(p => p.ProductID)
        //                    .Select(g => new ProductVmDTO
        //                    {
        //                        ProductID = g.Key,
        //                        ProductName = g.First().ProductName,
        //                        Price = g.First().Price,
        //                        Description = g.First().Description,
        //                        Quantity = g.First().Quantity,
        //                        Category = g.First().Category,
        //                        SubCategory = g.First().SubCategory,
        //                        Brand = g.First().Brand,
        //                        CreatedDate = g.First().CreatedDate,
        //                        ProductPicture = g.First().ProductPicture,
        //                        DiscountAmount = g.Sum(x => x.DiscountAmount ?? 0),
        //                        DiscountPercent = g.Sum(x => x.DiscountPercent ?? 0),
        //                        DiscountTag = g.First().DiscountTag
        //                    })
        //                    .ToList();

        //        return ExecutionResult<List<ProductVmDTO>>.Success(combinedProducts);
        //    }
        //    catch (Exception e)
        //    {
        //        return ExecutionResult<List<ProductVmDTO>>.Failure("No search information provided.");
        //    }
            

        //}
        public async Task<ExecutionResult<List<ProductVM>>> GetProductsSearch(string? categorySelection,
           string? subCategorySelection, string? brandSelection, int? productID, string? searchString)
        {
            try
            {
                if (categorySelection != null || subCategorySelection != null || brandSelection != null)
                {
                    var products =
                       from Product in _context.Products
                       join Category in _context.Categories on Product.CategoryID equals Category.CategoryID into categories
                       from Category in categories.DefaultIfEmpty()
                       join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID into subCategories
                       from SubCategory in subCategories.DefaultIfEmpty()
                       join Brand in _context.Brand on Product.BrandID equals Brand.BrandId into Brands
                       from Brand in Brands.DefaultIfEmpty()
                       where (brandSelection == null || (Brand != null && Brand.BrandName == brandSelection)) &&
                          (categorySelection == null || (Category != null && Category.CategoryName == categorySelection)) &&
                          (subCategorySelection == null || (SubCategory != null && SubCategory.SubCategoryName == subCategorySelection))

                       select new ProductVmDTO
                       {
                           ProductID = Product.ProductID,
                           ProductName = Product.ProductName,
                           Price = Product.Price,
                           Description = Product.Description,
                           Quantity = Product.Quantity,
                           Category = Category.CategoryName,
                           SubCategory = SubCategory.SubCategoryName,
                           Brand = Brand.BrandName,
                           CreatedDate = Product.CreatedDate,
                           ProductPicture = Product.ProductPicture,
                           DiscountTag = Product.DiscountTag
                       };

                    var resultList = await products.ToListAsync();

                    var productSearch = _mapper.Map<List<ProductVM>>(resultList);

                    for (int x = 0; x < productSearch.Count; x++)
                    {
                        productSearch[x].ProductImage = _utility.BytesToImage(productSearch[x].ProductPicture);
                    }

                    return ExecutionResult<List<ProductVM>>.Success(productSearch);
                }
                else if (productID != null)
                {
                   var exactMatches =
                       from Product in _context.Products
                       join Category in _context.Categories on Product.CategoryID equals Category.CategoryID into categories
                       from Category in categories.DefaultIfEmpty()
                       join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID into subCategories
                       from SubCategory in subCategories.DefaultIfEmpty()
                       join Brand in _context.Brand on Product.BrandID equals Brand.BrandId into Brands
                       from Brand in Brands.DefaultIfEmpty()
                       where Product.ProductID == productID

                       select new ProductVmDTO
                       {
                           ProductID = Product.ProductID,
                           ProductName = Product.ProductName,
                           Price = Product.Price,
                           Description = Product.Description,
                           Quantity = Product.Quantity,
                           Category = Category.CategoryName,
                           SubCategory = SubCategory.SubCategoryName,
                           Brand = Brand.BrandName,
                           CreatedDate = Product.CreatedDate,
                           ProductPicture = Product.ProductPicture,
                           DiscountTag = Product.DiscountTag
                       };

                    // find discounts
                    var products = from p in exactMatches                                   
                                   join DiscountsByTag in _context.Discounts on p.ProductID equals DiscountsByTag.ProductID into DiscountsTag
                                   from DiscountsByTag in DiscountsTag.DefaultIfEmpty()

                                   select new ProductVM
                                   {
                                       ProductID = p.ProductID,
                                       ProductName = p.ProductName,
                                       Price = p.Price,
                                       Description = p.Description,
                                       Quantity = p.Quantity,
                                       ProductPicture = p.ProductPicture,
                                       DiscountAmount = (decimal?)DiscountsByTag.DiscountAmount,
                                       DiscountPercent = (decimal?)DiscountsByTag.DiscountPercent
                                   };

                    var resultList = await products.ToListAsync();

                    var productSearch = _mapper.Map<List<ProductVM>>(resultList);

                    for (int x = 0; x < productSearch.Count; x++)
                    {
                        productSearch[x].ProductPicture = _utility.StringToBytes(productSearch[x].ProductImage);
                    }

                    return ExecutionResult<List<ProductVM>>.Success(productSearch);

                }
                else if (searchString != null)
                {                   
                    var upperSearchQuery = searchString.ToLower().Trim();
                    var keywords = searchString.Split(' ').Select(k => k.ToUpper().Trim()).ToList();

                    var exactMatches = _context.Products.Where(p =>
                        p.SearchWords != null && EF.Functions.Like(p.SearchWords.ToUpper(), "%" + upperSearchQuery + "%")
                    ).ToList();

                    if (!exactMatches.Any())
                    {
                        exactMatches = _context.Products.Where(p =>
                            p.SearchWords != null && keywords.Any(k => EF.Functions.Like(p.SearchWords.ToUpper(), "%" + k + "%"))
                        ).ToList();
                    }

                    // find discounts
                    var products = from p in exactMatches
                                   join DiscountsByBrand in _context.Discounts on p.BrandID equals DiscountsByBrand.BrandID into DiscountBrand
                                   from DiscountsByBrand in DiscountBrand.DefaultIfEmpty()
                                   join DiscountsByCategory in _context.Discounts on p.CategoryID equals DiscountsByCategory.CategoryID into DiscountCategory
                                   from DiscountsByCategory in DiscountCategory.DefaultIfEmpty()
                                   join DiscountsBySubcategory in _context.Discounts on p.SubCategoryID equals DiscountsBySubcategory.SubCategoryID into DiscountSubCategory
                                   from DiscountsBySubcategory in DiscountSubCategory.DefaultIfEmpty()
                                   join DiscountsByTag in _context.Discounts on p.DiscountTag equals DiscountsByTag.DiscountTag into DiscountsTag
                                   from DiscountsByTag in DiscountsTag.DefaultIfEmpty()
                                   join DiscountsByProduct in _context.Discounts on p.ProductID equals DiscountsByProduct.ProductID into DiscountsProduct
                                   from DiscountsByProduct in DiscountsProduct.DefaultIfEmpty()

                                   select new ProductVM
                                   {
                                       ProductID = p.ProductID,
                                       ProductName = p.ProductName,
                                       Price = p.Price,
                                       Description = p.Description,
                                       Quantity = p.Quantity,
                                       ProductPicture = p.ProductPicture,
                                       DiscountAmount = (decimal?)DiscountsByBrand?.DiscountAmount ?? (decimal?)DiscountsByCategory?.DiscountAmount 
                                            ?? (decimal?)DiscountsBySubcategory?.DiscountAmount ?? (decimal?)DiscountsByTag?.DiscountAmount ?? (decimal?)DiscountsByProduct?.DiscountAmount,

                                       DiscountPercent = (decimal?)DiscountsByBrand?.DiscountPercent ?? (decimal?)DiscountsByCategory?.DiscountPercent
                                            ?? (decimal?)DiscountsBySubcategory?.DiscountPercent ?? (decimal?)DiscountsByTag?.DiscountPercent ?? (decimal?)DiscountsByProduct?.DiscountPercent
                                   };


                    var prodList = products.ToList();

                    var productSearch = _mapper.Map<List<ProductVM>>(prodList);

                    for (int x = 0; x < productSearch.Count; x++)
                    {
                        productSearch[x].ProductImage = _utility.BytesToImage(productSearch[x].ProductPicture);
                        productSearch[x].SalePrice = _utility.CalculateSalePrice(productSearch[x].DiscountAmount, productSearch[x].DiscountPercent, productSearch[x].Price);
                    }

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
                    products = await
                            (from Product in _context.Products
                             join Category in _context.Categories on Product.CategoryID equals Category.CategoryID into categories
                             from Category in categories.DefaultIfEmpty()
                             join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID into subCategories
                             from SubCategory in subCategories.DefaultIfEmpty()
                             join Brand in _context.Brand on Product.BrandID equals Brand.BrandId into Brands
                             from Brand in Brands.DefaultIfEmpty()
                             join DiscountsByBrand in _context.Discounts on Product.BrandID equals DiscountsByBrand.BrandID into DiscountBrand
                             from DiscountsByBrand in DiscountBrand.DefaultIfEmpty()
                             join DiscountsByCategory in _context.Discounts on Product.CategoryID equals DiscountsByCategory.CategoryID into DiscountCategory
                             from DiscountsByCategory in DiscountCategory.DefaultIfEmpty()
                             join DiscountsBySubcategory in _context.Discounts on Product.SubCategoryID equals DiscountsBySubcategory.SubCategoryID into DiscountSubCategory
                             from DiscountsBySubcategory in DiscountSubCategory.DefaultIfEmpty()
                             where (DiscountsByBrand != null && Product.BrandID == DiscountsByBrand.BrandID) || (DiscountsByCategory != null && Product.CategoryID == DiscountsByCategory.CategoryID) ||
                                (DiscountsBySubcategory != null && Product.SubCategoryID == DiscountsBySubcategory.SubCategoryID)

                             select new ProductVmDTO
                             {
                                 ProductID = Product.ProductID,
                                 ProductName = Product.ProductName,
                                 Price = Product.Price,
                                 Description = Product.Description,
                                 Quantity = Product.Quantity,
                                 Category = Category.CategoryName,
                                 SubCategory = SubCategory.SubCategoryName,
                                 Brand = Brand.BrandName,
                                 CreatedDate = Product.CreatedDate,
                                 ProductPicture = Product.ProductPicture,
                                 DiscountAmount = (decimal?)DiscountsByBrand.DiscountAmount ?? (decimal?)DiscountsByCategory.DiscountAmount ?? (decimal?)DiscountsBySubcategory.DiscountAmount,
                                 DiscountPercent = (decimal?)DiscountsByBrand.DiscountPercent ?? (decimal?)DiscountsByCategory.DiscountPercent ?? (decimal?)DiscountsBySubcategory.DiscountPercent,
                                 DiscountTag = Product.DiscountTag
                             }).ToListAsync();

                }

                List<ProductVmDTO> productsWithDTags = new List<ProductVmDTO>();
                var filteredProducts = new List<ProductVmDTO>();
                if (discountTags != null)
                {
                    var discountTagsArray = discountTags.Split(',')
                                                         .Select(k => k.ToUpper().Trim())
                                                         .ToList();

                    // Get all discounts and split DiscountTag outside of the query
                    var allDiscounts = await _context.Discounts
                                               .Where(d => d.DiscountTag != null)
                                               .ToListAsync();

                    var discounts =
                        allDiscounts
                        .Where(d => d.DiscountTag.Split(',')
                                                  .Select(tag => tag.ToUpper().Trim())
                                                  .Intersect(discountTagsArray)
                                                  .Any())
                        .ToList();

                    var discountTagLookup = discounts
                        .ToDictionary(d => d.DiscountTag.ToUpper().Trim(), d => d);


                    productsWithDTags = await
                          (from Product in _context.Products
                           join Category in _context.Categories on Product.CategoryID equals Category.CategoryID into categories
                           from Category in categories.DefaultIfEmpty()
                           join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID into subCategories
                           from SubCategory in subCategories.DefaultIfEmpty()
                           join Brand in _context.Brand on Product.BrandID equals Brand.BrandId into Brands
                           from Brand in Brands.DefaultIfEmpty()
                           where !string.IsNullOrEmpty(Product.DiscountTag) && discountTagsArray.Any(k => EF.Functions.Like(Product.DiscountTag.ToUpper(), "%" + k + "%"))
                           select new ProductVmDTO
                           {
                               ProductID = Product.ProductID,
                               ProductName = Product.ProductName,
                               Price = Product.Price,
                               Description = Product.Description,
                               Quantity = Product.Quantity,
                               Category = Category.CategoryName,
                               SubCategory = SubCategory.SubCategoryName,
                               Brand = Brand.BrandName,
                               CreatedDate = Product.CreatedDate,
                               ProductPicture = Product.ProductPicture,
                               DiscountTag = Product.DiscountTag
                           }).ToListAsync();

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