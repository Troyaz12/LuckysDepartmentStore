using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class ColorService : IColorService
    {
        private readonly IColorStore _colorStore;
        private readonly ILogger _logger;

        public ColorService(IColorStore colorStore, ILogger<IColorService> logger) 
        {
            _colorStore = colorStore;
            _logger = logger;
        }
        public async Task<ExecutionResult<int>> Create(string name)
        {
            try
            {                
                var ColorResult = await _colorStore.AddColor(name);

                int newColorId = ColorResult;

                return ExecutionResult<int>.Success(newColorId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to create color {@name} in database.", name);
                return ExecutionResult<int>.Failure("Failed to save color to database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create color {@name}", name);
                return ExecutionResult<int>.Failure("Unable to create color.");
            }
        }
        public async Task<ExecutionResult<string>> GetColorName(int id)
        {
            try
            {
                var colorName = await _colorStore.GetColorName(id);

                if (colorName == null)
                {
                    _logger.LogError("Failed to get color {@id}", id);
                    return ExecutionResult<string>.Failure("Color not found for the given ID.");
                }

                return ExecutionResult<string>.Success(colorName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get color {@id}", id);
                return ExecutionResult<string>.Failure("Unable to get color name.");
            }
           
        }
        public async Task<ExecutionResult<string>> GetSizeName(int id)
        {
            try
            {
                var size = await _colorStore.GetSizeName(id);

                if (size == null)
                {
                    _logger.LogError("Failed to get size {@id}", id);
                    return ExecutionResult<string>.Failure("Size not found for the given ID.");
                }

                return ExecutionResult<string>.Success(size);                
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Failed to get size name {@id}", id);
                return ExecutionResult<string>.Failure("Unable to get size name.");
            }
           
        }
        public async Task<ExecutionResult<int>> CreateSize(string name)
        {
            try
            {             
                var sizeResult = await _colorStore.CreateSize(name);

                int newSizeId = sizeResult;

                return ExecutionResult<int>.Success(newSizeId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to create size {@name} in database.", name);
                return ExecutionResult<int>.Failure("Failed to save size to database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create size {@name}", name);
                return ExecutionResult<int>.Failure("Unable to create size.");
            }
        }
    }
}
