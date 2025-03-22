using AutoMapper;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Data.Stores
{
    public class BrandStore : IBrandStore
    {
        public LuckysContext _context;

        public BrandStore(LuckysContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBrand(ProductCreateVM product)
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
