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
        public IConfiguration _config;

        public ProductService(LuckysContext context, IMapper mapper, IColorService colorService, 
            ICategoryService categoryService, ISubCategoryService subCategoryService, IBrandService brandService
            , IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _colorService = colorService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _brandService = brandService;
            _config = config;
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

                newProduct.ProductPicture = ImageBytes(product.ProductPictureFile);

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
        public byte[] ImageBytes(IFormFile? fileImport)
        {
            byte[]? imageBytes = null;
            var defaultImage = _config["ImagePaths:ShoppingImageEmpty"];

            using (var memoryStream = new MemoryStream())
            {
                if (fileImport != null)
                {
                    //await file.form FormFile.CopyToAsync(memoryStream);
                    fileImport.CopyTo(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        imageBytes = memoryStream.ToArray();
                    }
                    else
                    {
                        throw new Exception("Insufficient Memory");
                    }
                }
                else
                {
                    var fileInfo = new FileInfo(defaultImage);
                    var fileStream = new FileStream(defaultImage, FileMode.Open);

                    var formFile = new FormFile(fileStream, 0, fileInfo.Length, fileInfo.Name, fileInfo.Name)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/jpeg" // Or whatever the MIME type of the image is
                    };

                    //await file.form FormFile.CopyToAsync(memoryStream);
                    formFile.CopyTo(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        imageBytes = memoryStream.ToArray();
                    }
                    else
                    {
                        throw new Exception("Insufficient Memory");
                    }
                }
            }

            return imageBytes;
        }
        public List<ProductVM> GetProducts()
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

            var list = products.ToList();
            var prod = _mapper.Map<ProductVM>(list.FirstOrDefault());

            return _mapper.Map<List<ProductVM>>(list);
        }
    }
}