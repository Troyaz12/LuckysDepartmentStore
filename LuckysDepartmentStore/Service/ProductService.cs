using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using SQLitePCL;
using System.Collections.Generic;
using System.Linq;

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
        public async Task<Product> CreateAsync(ProductCreateVM product)
        {

            try
            {
                if ((product != null && product.ColorProduct == null))
                {
                    for (int x = 0; x > product.ColorProduct.Count; x++)
                    {
                        if (product.ColorProduct[x].ColorID == 0)
                        {
                            var colorId = _colorService.Create(product.ColorProduct[x].Name);
                            
                            product.ColorProduct[x].ColorID = colorId;
                        }
                    }
                }
                if ((product != null && product.CategoryID == null) || product.CategoryID == 0)
                {
                    product.CategoryID = _categoryService.Create(product);
                }
                if ((product != null && product.SubCategoryID == null) || product.SubCategoryID == 0)
                {
                    product.SubCategoryID = _subCategoryService.Create(product);
                }
                if ((product != null && product.BrandID == null) || product.BrandID == 0)
                {
                    product.BrandID = _brandService.Create(product);
                }

                var newProduct = _mapper.Map<Product>(product);

                newProduct.ProductPicture = _utility.ImageBytes(product.ProductPictureFile);

                _context.Add(newProduct);
                await _context.SaveChangesAsync();

                int productId = newProduct.ProductID;
                
                foreach(ColorProductVM colors in product.ColorProduct)
                {                    
                    var newColorProduct = _mapper.Map<ColorProduct>(colors);
                    newColorProduct.ProductID = productId;

                    _context.Add(newColorProduct);
                    await _context.SaveChangesAsync();
                }

                return newProduct;
            }
            catch (DbUpdateException ex)
            {             
                throw new InvalidOperationException("Error saving product to database", ex);
            }
            catch (Exception ex)
            {             
                throw new Exception("An error occurred while processing your request", ex);
            }
        }
        //public T GetItemAs<T> (object obj) where T : class 
        //{ 
           


        //    if(obj.GetType() == typeof(ProductVM))
        //    {
        //        ProductVM productVm = new ProductVM();
        //        productVm = (ProductVM) obj;

        //        var color = _context.Colors
        //                 .Where(p => p.Name.ToLower() == productVm.Color.ToLower())
        //                 .ToList();

        //        Product product = new Product();
        //        product.ColorProductID = color[0].ColorID;


        //        return product as T;
        //    }




        //    return obj as T;

        //}
        public List<Color> GetColors()
        {
            var color = _context.Colors               
                .ToList();

            return color;
        }
        public List<Category> GetCategory()
        {
            var categories = _context.Categories
                .ToList();

            return categories;
        }
        public List<SubCategory> GetSubCategory()
        {
            var subCategory = _context.SubCategories
                .ToList();

            return subCategory;
        }
        public List<Brand> GetBrand()
        {
            var brands = _context.Brand
                .ToList();

            return brands;
        }

        public List<ProductVM> GetProducts(string categorySearch, string searchString)
        {
            var products = 
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
                };

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName!.ToUpper().Contains(searchString.ToUpper()));
            }

            var list = products.ToList();            

            return _mapper.Map<List<ProductVM>>(list);
        }
        public ProductEditVM GetAProduct(int productId)
        {
            var productDTO =
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
                };

            var colorProductDTO =
                from ColorProducts in _context.ColorProducts
                join Colors in _context.Colors on ColorProducts.ColorID equals Colors.ColorID
                where ColorProducts.ProductID == productId
                select new ColorProductEditDTO
                {
                    ProductID = ColorProducts.ProductID,
                    ColorID = ColorProducts.ColorID,
                    Quantity = ColorProducts.Quantity,
                    Name = Colors.Name,
                    ColorProductID = ColorProducts.ColorProductID
                };


            var product = productDTO.FirstOrDefault();

            var productModel = _utility.MapEditProduct(product);

            var colorProducts = _mapper.Map<List<ColorProductVM>>(colorProductDTO);

            productModel.ColorProduct = colorProducts;
            

            return productModel;
        }
        public ProductEditVM EditProduct(ProductEditVM productEdit)
        {
            var productMap = _mapper.Map<Product>(productEdit);

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
                    _context.ColorProducts.Add(colorProduct);
                }
            }

            var productSave = _context.SaveChanges();

            return productEdit;
        }

        public ExecutionResult<ProductDetailVM> GetDetails(int productId)
        {          

            var productDTO =
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
                 ProductPicture = Product.ProductPicture
             };

            var colorProductDTO =
                from ColorProducts in _context.ColorProducts
                join Colors in _context.Colors on ColorProducts.ColorID equals Colors.ColorID
                where ColorProducts.ProductID == productId
                select new ColorProductDetailDTO
                {
                    ProductID = ColorProducts.ProductID,
                    ColorID = ColorProducts.ColorID,
                    Quantity = ColorProducts.Quantity,
                    Name = Colors.Name,
                    ColorProductID = ColorProducts.ColorProductID
                };

            var product = productDTO.FirstOrDefault();

            if(product == null)
            {
                return ExecutionResult<ProductDetailVM>.Failure("Cannot find product in database. Product ID does not exist."); 
            }

            var productModel = _utility.MapDetailProduct(product);

            var colorProducts = _mapper.Map<List<ColorProductVM>>(colorProductDTO);

            productModel.ProductImage = _utility.BytesToImage(productModel.ProductPicture);

            productModel.ColorProduct = colorProducts;

            return ExecutionResult<ProductDetailVM>.Success(productModel);
        }
        public void Delete(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}