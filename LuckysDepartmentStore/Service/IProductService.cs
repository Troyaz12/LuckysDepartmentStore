using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Service
{
    public interface IProductService
    {
        public Task<Product> CreateAsync(ProductCreateVM product);
        public List<Color> GetColors();
        public List<Category> GetCategory();
        public List<SubCategory> GetSubCategory();
        public List<Brand> GetBrand();
        public List<ProductVM> GetProducts(string categorySearch, string searchString);
        public ProductEditVM GetAProduct(int productId);
        public ProductEditVM EditProduct(ProductEditVM product);
        public ProductDetailVM GetDetails(int productId);
        public void Delete(int productId);

    }
}
