using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class ColorService : IColorService
    {
        public LuckysContext _context;
        public IMapper _mapper;

        public ColorService(LuckysContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ExecutionResult<int>> Create(string name)
        {
            try
            {
                var newColor = new Color();
                newColor.Name = name;

                _context.Add(newColor);
                var ColorResult = await _context.SaveChangesAsync();

                int newColorId = newColor.ColorID;

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
                var colorName = await _context.Colors
               .Where(c => c.ColorID == id)
               .Select(c => c.Name)
               .SingleOrDefaultAsync();

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
                var size = await _context.Sizes
               .Where(c => c.SizesID == id)
               .Select(c => c.Size)
               .SingleOrDefaultAsync();

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
                var newSize = new Sizes();
                newSize.Size = name;

                _context.Add(newSize);
                var sizeResult = await _context.SaveChangesAsync();

                int newSizeId = newSize.SizesID;

                return ExecutionResult<int>.Success(newSizeId);
            }
            catch (Exception ex)
            {
                return ExecutionResult<int>.Failure("Unable to create color.");
            }
        }
    }
}
