using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface IProductService
    {
        public Task<Product> CreateAsync(ProductCreateVM product);
        public List<Color> GetColors();
        public List<Category> GetCategory();
        public List<SubCategory> GetSubCategory();
        public List<Brand> GetBrand();
        public List<ProductVM> GetProductsSearchBar(string categorySearch, string searchString);
        public ProductEditVM GetAProduct(int productId);
        public Task<ExecutionResult<ProductEditVM>> EditProduct(ProductEditVM product);
        public ExecutionResult<ProductDetailVM> GetDetails(int productId);
        public ExecutionResult<int> Delete(int productId);
        public Task<ExecutionResult<List<ProductVM>>> GetProductsByDiscount(string? categorySelection, string? subCategorySelection, string? brandSelection,
          int? productID, string? keywords);
        public List<Sizes> GetSize();
        public ExecutionResult<ItemVM> GetItem(int productId);
        public List<ProductVmDTO> GetProductsWithDiscount();
        public Task<ExecutionResult<List<ProductVM>>> GetProductsSearch(string? categorySelection,
          string? subCategorySelection, string? brandSelection, int? productID, string? discountTags);
    }
}
