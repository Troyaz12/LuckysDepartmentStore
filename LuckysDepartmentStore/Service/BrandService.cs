using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandStore _brandStore;
        private readonly ILogger _logger;

        public BrandService(IBrandStore brandStore, ILogger<IBrandService> logger)
        {
            _brandStore = brandStore;
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
                var brandID = await _brandStore.CreateBrand(product);

                return ExecutionResult<int>.Success(brandID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to create brand for product {@Product}", product);
                return ExecutionResult<int>.Failure("Failed to save brand to database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create brand for product {@Product}", product);
                return ExecutionResult<int>.Failure("Failed to create brand.");
            }
        }
    }
}
