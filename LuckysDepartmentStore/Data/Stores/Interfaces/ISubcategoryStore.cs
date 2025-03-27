using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface ISubcategoryStore
    {
        public Task<int> CreateSubcategory(SubCategory subCategory);
    }
}
