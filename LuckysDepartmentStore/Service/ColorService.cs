using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
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
        public int Create(string name)
        {
            var newColor = new Color();
            newColor.Name = name;

            _context.Add(newColor);
            var ColorResult = _context.SaveChanges();

            int newColorId = newColor.ColorID;

            return newColorId;
        }
        public string GetColorName(int id)
        {
            var colorName = _context.Colors
                .Where(c => c.ColorID == id)
                .Select(c => c.Name)
                .SingleOrDefaultAsync().Result;

            return colorName;
        }
        public string GetSizeName(int id)
        {
            var size = _context.Sizes
                .Where(c => c.SizesID == id)
                .Select(c => c.Size)
                .SingleOrDefaultAsync().Result;

            return size;
        }
    }
}
