using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
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

        public ProductService(LuckysContext context, IMapper mapper, IColorService colorService, 
            ICategoryService categoryService, ISubCategoryService subCategoryService, IBrandService brandService)
        {
            _context = context;
            _mapper = mapper;
            _colorService = colorService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _brandService = brandService;
        }
        public async Task Create(ProductVM product)
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

                _context.Add(newProduct);
                await _context.SaveChangesAsync();

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
    }
}