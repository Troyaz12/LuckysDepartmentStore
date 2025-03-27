using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Home;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Data.Stores
{
    public class ColorsStore : IColorStore
    {
        public LuckysContext _context;

        public ColorsStore(LuckysContext context)
        {
            _context = context;
        }

        public async Task<List<ColorProductItemDTO>> ColorsByProductID(int productId)
        {
            var colorProductDTO = await(
                   from ColorProducts in _context.ColorProducts
                   join Colors in _context.Colors on ColorProducts.ColorID equals Colors.ColorID
                   join Sizes in _context.Sizes on ColorProducts.SizeID equals Sizes.SizesID
                   where ColorProducts.ProductID == productId
                   select new ColorProductItemDTO
                   {
                       ProductID = ColorProducts.ProductID,
                       ColorID = ColorProducts.ColorID,
                       Quantity = ColorProducts.Quantity,
                       Name = Colors.Name,
                       ColorProductID = ColorProducts.ColorProductID,
                       SizeID = ColorProducts.SizeID,
                       SizeName = Sizes.Size
                   }).ToListAsync();


            return colorProductDTO;
        }

        public async Task<List<Color>> AllColors()
        {
            var color = await _context.Colors
              .ToListAsync();

            return color;
        }

        public async Task<List<Sizes>> AllSizes()
        {
            var size = await _context.Sizes
                .ToListAsync();

            return size;
        }

        public async Task<int> AddColor(string name)
        {
            var newColor = new Color();
            newColor.Name = name;

            _context.Add(newColor);
            var ColorResult = await _context.SaveChangesAsync();

            return ColorResult;
        }
        public async Task<string> GetColorName(int id)
        {
            var colorName = await _context.Colors
            .Where(c => c.ColorID == id)
            .Select(c => c.Name)
            .SingleOrDefaultAsync();

            return colorName;
        }

        public async Task<string> GetSizeName(int id)
        {
            var size = await _context.Sizes
               .Where(c => c.SizesID == id)
               .Select(c => c.Size)
               .SingleOrDefaultAsync();

            return size;
        }
        public async Task<int> CreateSize(string name)
        {
            var newSize = new Sizes();
            newSize.Size = name;

            _context.Add(newSize);
            var sizeResult = await _context.SaveChangesAsync();

            int newSizeId = newSize.SizesID;

            return newSizeId;
        }
    }
}
