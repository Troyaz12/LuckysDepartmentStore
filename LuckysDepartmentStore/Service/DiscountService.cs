using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class DiscountService : IDiscountService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        private readonly Utility _utility;

        public DiscountService(LuckysContext context, IMapper mapper, Utility utility)
        {
            _context = context;
            _mapper = mapper;
            _utility = utility;
        }     

        public void DeleteDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public Discount GetDiscount(int productID)
        {
            throw new NotImplementedException();
        }

        public void UpdateDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public async Task<Discount> CreateAsync(DiscountCreateVM discount)
        {

            try
            {
                var newDiscount = _mapper.Map<Discount>(discount);
                newDiscount.DiscountArt = _utility.ImageBytes(discount.DiscountArtFile);

                _context.Add(newDiscount);
                await _context.SaveChangesAsync();

                return newDiscount;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error saving product to database", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing your request", ex);
            }
        }
    }
}
