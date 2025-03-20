using AutoMapper;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;
using Utility = LuckysDepartmentStore.Utilities.Utility;


namespace LuckysDepartmentStore.Data.Stores
{
    public class ProductStore : IProductStore 
    {
        public LuckysContext _context;
        public IMapper _mapper;
        private readonly Utility _utility;

        public ProductStore(LuckysContext context, IMapper mapper, Utility utility)
        {
            _context = context;
            _mapper = mapper;
            _utility = utility;
        }

        public async Task<ExecutionResult<List<ProductVM>>> GetProductsSearch(string? categorySelection,
           string? subCategorySelection, string? brandSelection, string? searchString)
        {
            try
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
                   where (Product.SearchWords != null && EF.Functions.Like(Product.SearchWords.ToUpper(), "%" + searchString + "%")) &&
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

                return ExecutionResult<List<ProductVM>>.Success(productSearch);

            }
            catch
            {
                return ExecutionResult<List<ProductVM>>.Failure("Unable to search products.");
            }

        }
        public async Task<ExecutionResult<List<ProductVM>>> GetProductByID(int? productID)
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

        public async Task<ExecutionResult<List<ProductVM>>> GetProductSearchString(string? searchString)
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
    }
}
