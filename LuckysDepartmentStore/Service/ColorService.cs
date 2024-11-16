using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
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
        public int? Create(ProductVM product)
        {
            var newColor = new Color();
            newColor.Name = product.ColorSelection;

            _context.Add(newColor);
            var ColorResult = _context.SaveChanges();

            int newColorId = newColor.ColorID;

            return newColorId;
        }
    }
}
