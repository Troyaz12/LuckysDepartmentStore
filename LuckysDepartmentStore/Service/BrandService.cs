using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using LuckysDepartmentStore.Utilities;

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

        public async Task<ExecutionResult<int>> Create(ProductCreateVM product)
        {
            try
            {
                var newBrand = new Brand();
                newBrand.BrandName = product.BrandSelection;

                _context.Add(newBrand);
                var brandResult = _context.SaveChanges();

                int newBrandId = newBrand.BrandId;

                return ExecutionResult<int>.Success(newBrandId);
            }
            catch (Exception ex)
            {
                return ExecutionResult<int>.Failure("Failed to update brand service.");
            }
        }
    }
}
