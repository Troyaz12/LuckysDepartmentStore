using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface IBrandService
    {
        public Task<ExecutionResult<int>> Create(ProductCreateVM product);
    }
}
