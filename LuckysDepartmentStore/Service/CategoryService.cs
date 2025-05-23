﻿using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class CategoryService : ICategoryService
    {    
        private readonly ICategoryStore _categoryStore;
        private readonly ILogger _logger;

        public CategoryService(ICategoryStore categoryStore, ILogger<ICategoryService> logger)
        {
            _categoryStore = categoryStore;
            _logger = logger;
        }
        public async Task<ExecutionResult<int>> Create(ProductCreateVM product)
        {

            if (product == null)
            {
                return ExecutionResult<int>.Failure("Product data not received.");
            }

            try
            {
               var newCategoryId = await _categoryStore.CreateCategory(product);

                return ExecutionResult<int>.Success(newCategoryId);

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to create category in database for {@Product}", product);
                return ExecutionResult<int>.Failure("Failed to save category to database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create category for product {@Product}", product);
                return ExecutionResult<int>.Failure("Failed to create product.");
            }
        }
    }
}
