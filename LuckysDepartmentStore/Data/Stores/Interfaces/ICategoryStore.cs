using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface ICategoryStore
    {
        public Task<int> CreateCategory(ProductCreateVM product);
    }
}
