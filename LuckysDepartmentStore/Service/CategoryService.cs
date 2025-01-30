using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service
{
    public class CategoryService : ICategoryService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        public CategoryService(LuckysContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ExecutionResult<int>> Create(ProductCreateVM product)
        {
            try
            {

                var newCategory = new Category();
                newCategory.CategoryName = product.CategorySelection;
                newCategory.CategoryDescription = product.CategorySelection;

                _context.Add(newCategory);
                var categoryResult = await _context.SaveChangesAsync();

                int newCategoryId = newCategory.CategoryID;

                return ExecutionResult<int>.Success(newCategoryId);
            }
            catch (Exception ex)
            {
                return ExecutionResult<int>.Failure("Failed to update payment total.");
            }
        }
    }
}
