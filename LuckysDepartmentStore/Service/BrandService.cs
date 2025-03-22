using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using LuckysDepartmentStore.Utilities;
using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Data.Stores.Interfaces;

namespace LuckysDepartmentStore.Service
{
    public class BrandService : IBrandService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        public IBrandStore _brandStore;

        public BrandService(LuckysContext context, IMapper mapper, IBrandStore brandStore)
        {
            _context = context;
            _mapper = mapper;
            _brandStore = brandStore;
        }

        public async Task<ExecutionResult<int>> Create(ProductCreateVM product)
        {
            try
            {
                var brandID = _brandStore.CreateBrand(product);

                return ExecutionResult<int>.Success(brandID.Result);
            }
            catch (Exception ex)
            {
                return ExecutionResult<int>.Failure("Failed to update brand service.");
            }
        }
    }
}
