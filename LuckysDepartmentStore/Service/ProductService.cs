using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Product> CreateAsync(ProductVM product)
        {

            try
            {
                if ((product != null && product.ColorId == null) || product.ColorId == 0)
                {
                    product.ColorId = _colorService.Create(product);
                }
                if ((product != null && product.CategoryId == null) || product.CategoryId == 0)
                {
                    product.CategoryId = _categoryService.Create(product);
                }
                if ((product != null && product.SubCategoryId == null) || product.SubCategoryId == 0)
                {
                    product.SubCategoryId = _subCategoryService.Create(product);
                }
                if ((product != null && product.BrandId == null) || product.BrandId == 0)
                {
                    product.BrandId = _brandService.Create(product);
                }

                var newProduct = _mapper.Map<Product>(product);

                newProduct.ProductPicture = ImageBytes(product.ProductPictureFile);

                _context.Add(newProduct);
                await _context.SaveChangesAsync();

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
        public byte[] ImageBytes(IFormFile? fileImport)
        {
            byte[]? imageBytes = null;
            //var defaultImage = _config["ImagePaths:ShoppingImageEmpty"];

            //using (var memoryStream = new MemoryStream())
            //{
            //    if (fileImport != null)
            //    {
            //        //await file.form FormFile.CopyToAsync(memoryStream);
            //        fileImport.CopyTo(memoryStream);
            //        // Upload the file if less than 2 MB
            //        if (memoryStream.Length < 2097152)
            //        {
            //            imageBytes = memoryStream.ToArray();
            //        }
            //        else
            //        {
            //            throw new Exception("Insufficient Memory");
            //        }
            //    }
            //    else
            //    {
            //        var fileInfo = new FileInfo(defaultImage);
            //        var fileStream = new FileStream(defaultImage, FileMode.Open);

            //        var formFile = new FormFile(fileStream, 0, fileInfo.Length, fileInfo.Name, fileInfo.Name)
            //        {
            //            Headers = new HeaderDictionary(),
            //            ContentType = "image/jpeg" // Or whatever the MIME type of the image is
            //        };

            //        //await file.form FormFile.CopyToAsync(memoryStream);
            //        formFile.CopyTo(memoryStream);
            //        // Upload the file if less than 2 MB
            //        if (memoryStream.Length < 2097152)
            //        {
            //            imageBytes = memoryStream.ToArray();
            //        }
            //        else
            //        {
            //            throw new Exception("Insufficient Memory");
            //        }
            //    }
            //}

            return imageBytes;
        }
    }
}