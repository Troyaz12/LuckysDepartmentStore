using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class DiscountService : IDiscountService
    {
        public LuckysContext _context;
        public IMapper _mapper;

        public DiscountService(LuckysContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public void AddDiscount(Discount discount)
        {
            try
            {
                _context.Add(discount);
                var discountResult = _context.SaveChanges();
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
    }
}
