using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface IBrandStore
    {
        public Task<int> CreateBrand(ProductCreateVM product);


    }
}
