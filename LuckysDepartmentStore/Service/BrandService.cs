using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class BrandService : IBrandService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        public BrandService(LuckysContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(ProductCreateVM product)
        {
            var newBrand = new Brand();
            newBrand.BrandName = product.BrandSelection;

            _context.Add(newBrand);
            var brandResult = _context.SaveChanges();

            int newBrandId = newBrand.BrandId;

            return newBrandId;
        }
    }
}
