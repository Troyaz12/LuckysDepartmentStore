using AutoMapper;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Home;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;
using Utility = LuckysDepartmentStore.Utilities.Utility;

namespace LuckysDepartmentStore.Data.Stores
{
    public class ColorsStore : IColorStore
    {
        public LuckysContext _context;
        public IMapper _mapper;
        private readonly IUtility _utility;

        public ColorsStore(LuckysContext context, IMapper mapper, IUtility utility)
        {
            _context = context;
            _mapper = mapper;
            _utility = utility;
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
    }
}
