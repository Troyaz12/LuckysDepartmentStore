using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service
{
    public class SubCategoryService : ISubCategoryService
    {
        public LuckysContext _context;
        ISubcategoryStore _subcategoryStore;

        public SubCategoryService(LuckysContext context, ISubcategoryStore subcategoryStore) 
        {
            _context = context;      
            _subcategoryStore = subcategoryStore;
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
                return ExecutionResult<int>.Failure($"Subcategory creation failed.");

            }            
        }
    }
}
