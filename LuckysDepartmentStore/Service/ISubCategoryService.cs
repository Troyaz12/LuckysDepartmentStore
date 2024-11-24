using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Service
{
    public interface ISubCategoryService
    {
        public int Create(ProductVM product);
    }
}
