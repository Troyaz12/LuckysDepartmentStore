using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service
{
    public class SubCategoryService : ISubCategoryService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        public SubCategoryService(LuckysContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExecutionResult<int>> Create(ProductCreateVM product)
        {
            try
            {
                var subCategory = new SubCategory();
                subCategory.SubCategoryName = product.SubCategorySelection;
                subCategory.SubCategoryDescription = product.SubCategorySelection;

                _context.Add(subCategory);
                var subCategoryResult = await _context.SaveChangesAsync();

                int newSubCategoryId = subCategory.SubCategoryID;

                return ExecutionResult<int>.Success(newSubCategoryId);
            }
            catch (Exception ex)
            {
                return ExecutionResult<int>.Failure($"Subcategory creation failed.");

            }            
        }
    }
}
