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
        public Task<ExecutionResult<Product>> CreateAsync(ProductCreateVM product);
        public Task<ExecutionResult<List<Color>>> GetColors();
        public Task<ExecutionResult<List<Category>>> GetCategory();
        public Task<ExecutionResult<List<SubCategory>>> GetSubCategory();
        public Task<ExecutionResult<List<Brand>>> GetBrand();
        public Task<ExecutionResult<List<ProductVM>>> GetProductsSearchBar(string categorySearch, string searchString);
        public Task<ExecutionResult<ProductEditVM>> GetAProduct(int productId);
        public Task<ExecutionResult<ProductEditVM>> EditProduct(ProductEditVM product);
        public Task<ExecutionResult<ProductDetailVM>> GetDetails(int productId);
        public Task<ExecutionResult<int>> Delete(int productId);
        public Task<ExecutionResult<List<ProductVM>>> GetProductsByDiscount(string? categorySelection, string? subCategorySelection, string? brandSelection,
          int? productID, string? keywords);
        public Task<ExecutionResult<List<Sizes>>> GetSize();
        public Task<ExecutionResult<ItemVM>> GetItem(int productId);
        public Task<ExecutionResult<List<ProductVM>>> GetProductsSearch(string? categorySelection,
          string? subCategorySelection, string? brandSelection, int? productID, string? discountTags);
        public Task<ExecutionResult<ItemVM>> CalculateDiscount(ItemVM item);

        public Task<ExecutionResult<List<ProductVmDTO>>> GetProductsWithDiscount(string? categorySelection,
            string? subCategorySelection, string? brandSelection, string? discountTags);
    }
}
