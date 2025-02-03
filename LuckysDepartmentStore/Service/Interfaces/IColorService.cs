using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface IColorService
    {
        public Task<ExecutionResult<int>> Create(string name);
        public Task<ExecutionResult<string>> GetColorName(int id);
        public Task<ExecutionResult<string>> GetSizeName(int id);
        public Task<ExecutionResult<int>> CreateSize(string name);

    }
}
