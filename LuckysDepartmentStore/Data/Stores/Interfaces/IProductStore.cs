using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface IProductStore
    {
        public Task<ExecutionResult<List<ProductVM>>> GetProductsSearch(string? categorySelection,
          string? subCategorySelection, string? brandSelection, string? searchString);

        public Task<ExecutionResult<List<ProductVM>>> GetProductByID(int? productID);

        public Task<ExecutionResult<List<ProductVM>>> GetProductSearchString(string? searchString);
    }
}
