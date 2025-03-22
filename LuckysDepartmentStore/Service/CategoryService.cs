using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using LuckysDepartmentStore.Utilities;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Data.Stores;

namespace LuckysDepartmentStore.Service
{
    public class CategoryService : ICategoryService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        private ICategoryStore _categoryStore;

        public CategoryService(LuckysContext context, IMapper mapper, ICategoryStore categoryStore)
        {
            _context = context;
            _mapper = mapper;
            _categoryStore = categoryStore;
        }
        public async Task<ExecutionResult<int>> Create(ProductCreateVM product)
        {
            try
            {
               var newCategoryId = _categoryStore.CreateCategory(product);

                return ExecutionResult<int>.Success(newCategoryId.Result);
            }
            catch (Exception ex)
            {
                return ExecutionResult<int>.Failure("Failed to update payment total.");
            }
        }
    }
}
