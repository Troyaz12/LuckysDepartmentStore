using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service
{
    public class ColorService : IColorService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        private readonly IColorStore _colorStore;

        public ColorService(LuckysContext context, IMapper mapper, IColorStore colorStore) 
        {
            _context = context;
            _mapper = mapper;
            _colorStore = colorStore;
        }
        public async Task<ExecutionResult<int>> Create(string name)
        {
            try
            {                
                var ColorResult = await _colorStore.AddColor(name);

                int newColorId = ColorResult;

                return ExecutionResult<int>.Success(newColorId);
            }
            catch (Exception ex)
            {
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
                    return ExecutionResult<string>.Failure("Color not found for the given ID.");
                }

                return ExecutionResult<string>.Success(colorName);
            }
            catch (Exception ex)
            {
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
                    return ExecutionResult<string>.Failure("Size not found for the given ID.");
                }

                return ExecutionResult<string>.Success(size);                
            }
            catch (Exception ex) 
            {
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
            catch (Exception ex)
            {
                return ExecutionResult<int>.Failure("Unable to create color.");
            }
        }
    }
}
