using AutoMapper;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Home;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;


namespace LuckysDepartmentStore.Data.Stores
{
    public class ProductStore : IProductStore
    {
        public LuckysContext _context;
        public IMapper _mapper;
        private readonly IUtility _utility;

        public ProductStore(LuckysContext context, IMapper mapper, IUtility utility)
        {
            _context = context;
            _mapper = mapper;
            _utility = utility;
        }
        /// no discounts here
        public async Task<List<ProductVM>> GetProductsSearch(string? categorySelection,
           string? subCategorySelection, string? brandSelection, string? searchString)
        {           
            string? upperSearchQuery = null;

            if (!String.IsNullOrEmpty(upperSearchQuery))
            {
                upperSearchQuery = searchString.ToLower().Trim();
            }
            var products =
               from Product in _context.Products
               join Category in _context.Categories on Product.CategoryID equals Category.CategoryID into categories
            from Category in categories.DefaultIfEmpty()
               join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID into subCategories
               from SubCategory in subCategories.DefaultIfEmpty()
               join Brand in _context.Brand on Product.BrandID equals Brand.BrandId into Brands
               from Brand in Brands.DefaultIfEmpty()
               where (searchString == null || Product.SearchWords != null && EF.Functions.Like(Product.SearchWords.ToUpper(), "%" + searchString + "%")) &&
                  (brandSelection == null || (Brand != null && Brand.BrandName == brandSelection)) &&
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

            return productSearch;          

        }
        
        public async Task<List<ProductVM>> GetProductDiscountByIDSearch(int? productID)
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

            for (int x = 0; x < resultList.Count; x++)
            {
                resultList[x].ProductPicture = _utility.StringToBytes(resultList[x].ProductImage);
            }

            return resultList;
        }

        public async Task<List<ProductVM>> GetProductSearchString(string? searchString)
        {
            var synonymMap = new Dictionary<string, string>
            {
                { "MEN", "MENS" },
                { "WOMAN", "WOMENS" },
                { "WOMEN", "WOMENS" },
                { "LADIES", "WOMENS" },
                { "KID", "KIDS" },
                { "GIRL", "GIRLS" },
                { "BOY", "BOYS" }
            };

            var upperSearchQuery = searchString.ToLower().Trim();
            var keywords = searchString.Split(' ').Select(k => k.ToUpper().Trim()).ToList();

            var expandedKeywords = keywords.Select(k => synonymMap.ContainsKey(k) ? synonymMap[k] : k).ToList();


            var exactMatches = _context.Products.Where(p =>
                p.SearchWords != null && EF.Functions.Like(p.SearchWords.ToUpper(), "%" + upperSearchQuery + "%")
            ).ToList();

            if (!exactMatches.Any())
            {
                exactMatches = _context.Products
                    .AsEnumerable() // Switch to in-memory for splitting
                    .Where(p => p.SearchWords != null &&
                                expandedKeywords.All(k => p.SearchWords.ToUpper().Split(' ').Contains(k)))
                    .ToList();
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


            var productSearch = products.ToList();

            for (int x = 0; x < productSearch.Count; x++)
            {
                productSearch[x].ProductImage = _utility.BytesToImage(productSearch[x].ProductPicture);
                productSearch[x].SalePrice = _utility.CalculateSalePrice(productSearch[x].DiscountAmount, productSearch[x].DiscountPercent, productSearch[x].Price);
            }

            return productSearch;
        }

        public async Task<ItemDTO> GetItem(int productId)
        {
            var productDTO = await(
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
                   join DiscountsByTag in _context.Discounts on Product.DiscountTag equals DiscountsByTag.DiscountTag into DiscountsTag
                   from DiscountsByTag in DiscountsTag.DefaultIfEmpty()
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
                       DiscountAmount = (decimal?)DiscountsByBrand.DiscountAmount ?? (decimal?)DiscountsByCategory.DiscountAmount ?? (decimal?)DiscountsByProduct.DiscountAmount ?? (decimal?)DiscountsBySubcategory.DiscountAmount ?? (decimal?)DiscountsByTag.DiscountAmount,
                       DiscountPercent = (decimal?)DiscountsByBrand.DiscountPercent ?? (decimal?)DiscountsByCategory.DiscountPercent ?? (decimal?)DiscountsByProduct.DiscountPercent ?? (decimal?)DiscountsBySubcategory.DiscountPercent ?? (decimal?)DiscountsByTag.DiscountPercent,
                       DiscountTag = Product.DiscountTag
                   }).FirstOrDefaultAsync();

            return productDTO;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == productId);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Category>> Categories()
        {
            var categories = await _context.Categories
             .ToListAsync();

            return categories;
        }

        public async Task<List<SubCategory>> SubCategories()
        {
            var subCategory = await _context.SubCategories
              .ToListAsync();

            return subCategory;
        }

        public async Task<List<Brand>> Brand()
        {
            var brands = await _context.Brand
                .ToListAsync();

            return brands;            
        }

        public async Task<List<ProductVmDTO>> AllProductAllInfoNoDiscount()
        {
            var products = await(
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

            return products;
        }
        // Gets by category, subcategory or brand
        public async Task<List<ProductVmDTO>> ProductsWithDiscountsSelection()
        {
            var products = await
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



            return products;
        }

        public async Task<List<ProductVmDTO>> ProductsWithDiscountsByTag(List<string> discountTags)
        {

            var upperTags = discountTags.Select(k => k.ToUpper()).ToList();

            var productsWithDTags = await
                          (from Product in _context.Products
                           join Category in _context.Categories on Product.CategoryID equals Category.CategoryID into categories
                           from Category in categories.DefaultIfEmpty()
                           join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID into subCategories
                           from SubCategory in subCategories.DefaultIfEmpty()
                           join Brand in _context.Brand on Product.BrandID equals Brand.BrandId into Brands
                           from Brand in Brands.DefaultIfEmpty()
                           where !string.IsNullOrEmpty(Product.DiscountTag) && upperTags.Contains(Product.DiscountTag.ToUpper())
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


            return productsWithDTags;
        }

        public async Task<ProductDTO> GetProductByIDNoDiscount(int productID)
        {
            var productDTO = await(
                from Product in _context.Products
                join Category in _context.Categories on Product.CategoryID equals Category.CategoryID
                join SubCategory in _context.SubCategories on Product.SubCategoryID equals SubCategory.SubCategoryID
                join Brand in _context.Brand on Product.BrandID equals Brand.BrandId
                where Product.ProductID == productID
                select new ProductDTO
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
                    SearchWords = Product.SearchWords,
                    ProductPicture = Product.ProductPicture
                }).FirstOrDefaultAsync();

            return productDTO;
        }

        public async Task<Product> ProductByID(int productID)
        {
            var productOld = await _context.Products.FindAsync(productID);

            return productOld;
        }
        public async Task UpdateProduct(Product product, List<ColorProduct> colorProductsToAdd, List<ColorProduct> colorProductsToRemove)
        {
            _context.Products.Update(product);

            foreach (var colorProduct in colorProductsToRemove)
            {
                _context.ColorProducts.Remove(colorProduct);
            }

            foreach (var colorProduct in colorProductsToAdd)
            {
                _context.ColorProducts.Add(colorProduct);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddProduct(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddColorProductList(List<ColorProduct> newColorProducts)
        {
            _context.AddRange(newColorProducts);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ColorProduct>> GetColorProductList(ProductEditVM productEdit)
        {

            var colorProductsDB = _context.ColorProducts.Where(e => e.ProductID == productEdit.ProductID).ToList();

            return colorProductsDB;
        }
    }
}
