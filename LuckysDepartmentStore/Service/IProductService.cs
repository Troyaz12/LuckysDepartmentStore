using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Service
{
    public interface IProductService
    {
        public Task Create(ProductVM product);
    }
}
