using AutoMapper;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Data.Stores
{
    public class DiscountStore : IDiscountStore
    {
        public LuckysContext _context;
        public IMapper _mapper;
        private readonly IUtility _utility;

        public DiscountStore(LuckysContext context, IMapper mapper, IUtility utility)
        {
            _context = context;
            _mapper = mapper;
            _utility = utility;
        }

        public async Task<List<Discount>> DiscountWithTag()
        {
            var allDiscounts = await _context.Discounts
                                             .Where(d => d.DiscountTag != null)
                                             .ToListAsync();

            return allDiscounts;
        }
    }
}
