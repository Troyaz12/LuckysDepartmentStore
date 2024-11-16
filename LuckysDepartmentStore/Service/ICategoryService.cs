using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Service
{
    public interface ICategoryService
    {
        public int? Create(ProductVM product);
    }
}
