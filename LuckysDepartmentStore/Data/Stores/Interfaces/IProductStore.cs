using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Home;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface IProductStore
    {
        public Task<List<ProductVM>> GetProductsSearch(string? categorySelection,
          string? subCategorySelection, string? brandSelection, string? searchString);
        public Task<List<ProductVM>> GetProductDiscountByIDSearch(int? productID);
        public Task<List<ProductVM>> GetProductSearchString(string? searchString);
        public Task<ItemDTO> GetItem(int productId);
        public Task<bool> DeleteProduct(int productId);
        public Task<List<Category>> Categories();
        public Task<List<SubCategory>> SubCategories();
        public Task<List<Brand>> Brand();
        public Task<List<ProductVmDTO>> AllProductAllInfoNoDiscount();
        public Task<List<ProductVmDTO>> ProductsWithDiscountsSelection();
        public Task<List<ProductVmDTO>> ProductsWithDiscountsByTag(List<string> discountTags);
        public Task<ProductDTO> GetProductByIDNoDiscount(int productID);
        public Task<Product> ProductByID(int productID);
        public Task UpdateProduct(Product product, List<ColorProduct> colorProductsToAdd, List<ColorProduct> colorProductsToRemove);
        public Task AddProduct(Product product);
        public Task AddColorProductList(List<ColorProduct> colorProductsToAdd);
        public Task<List<ColorProduct>> GetColorProductList(ProductEditVM productEdit);
    }
}
