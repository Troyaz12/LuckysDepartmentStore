using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubcategoryStore _subcategoryStore;
        private readonly ILogger _logger;

        public SubCategoryService(ISubcategoryStore subcategoryStore, ILogger<ISubCategoryService> logger) 
        {
            _subcategoryStore = subcategoryStore;
            _logger = logger;
        }

        public async Task<ExecutionResult<int>> Create(ProductCreateVM product)
        {
            try
            {
                var subCategory = new SubCategory();
                subCategory.SubCategoryName = product.SubCategorySelection;
                subCategory.SubCategoryDescription = product.SubCategorySelection;

                var id = await _subcategoryStore.CreateSubcategory(subCategory);

                return ExecutionResult<int>.Success(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to create a subCategory for {@product}", product);
                return ExecutionResult<int>.Failure($"Subcategory creation failed.");
            }            
        }
    }
}
