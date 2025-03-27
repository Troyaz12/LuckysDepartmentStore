using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Discount;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class DiscountService : IDiscountService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        private readonly IUtility _utility;
        private IDiscountStore _discountStore;

        public DiscountService(LuckysContext context, IMapper mapper, IUtility utility, IDiscountStore discountStore)
        {
            _context = context;
            _mapper = mapper;
            _utility = utility;
            _discountStore = discountStore;
        }     

        public async Task<ExecutionResult<int>> DeleteDiscount(int discountId)
        {
            try
            {
                var discount = await _discountStore.GetDiscountByID(discountId);

                if (discount == null)
                {
                    return ExecutionResult<int>.Failure("Unable to delete discount.");
                }

                
                await _discountStore.DeleteDiscounts(discount);

                return ExecutionResult<int>.Success(discountId);
            }
            catch (Exception ex)
            {
                return ExecutionResult<int>.Failure("Unable to delete discount.");
            }
        }

        public async Task<ExecutionResult<DiscountVM>> GetDiscount(int discountId)
        {
            try
            {
                var discount = await _discountStore.GetDiscountsWithSelections(discountId);                

                if (discount == null)
                {
                    return ExecutionResult<DiscountVM>.Failure("Unable to retrieve Discounts.");
                }

                var discountProducts = _mapper.Map<DiscountVM>(discount);

                discountProducts.DiscountImage = _utility.BytesToImage(discountProducts.DiscountArt);

                return ExecutionResult<DiscountVM>.Success(discountProducts);

            }
            catch
            {
                return ExecutionResult<DiscountVM>.Failure("Unable to retrieve discount details.");
            }
        }

        public async Task<ExecutionResult<DiscountEditVM>> UpdateDiscount(DiscountEditVM discount)
        {
            try
            {
                if (discount == null)
                {
                    return ExecutionResult<DiscountEditVM>.Failure("Unable to save discount.");
                }          

                await _discountStore.UpdateDiscount(discount);

                return ExecutionResult<DiscountEditVM>.Success(discount);
            }
            catch (Exception ex)
            {
                return ExecutionResult<DiscountEditVM>.Failure("Unable to save discount.");
            }
        }

        public async Task<Discount> CreateAsync(DiscountCreateVM discount)
        {
            try
            {
                if(discount.DiscountPercent != 0)
                {
                    discount.DiscountPercent = discount.DiscountPercent/100;
                }

                if (discount.DiscountAmount != 0)
                {
                    discount.DiscountAmount = discount.DiscountAmount / 100;
                }

                var newDiscount = _mapper.Map<Discount>(discount);
                newDiscount.DiscountArt = _utility.ImageBytes(discount.DiscountArtFile);

                await _discountStore.AddDiscount(newDiscount);

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

        public async Task<ExecutionResult<List<DiscountVM>>> GetActiveDiscounts()
        {
            try
            {
                var discount = await _discountStore.GetAllDiscounts();

                if (discount == null)
                {
                    return ExecutionResult<List<DiscountVM>>.Failure("Unable to retrieve Discounts.");
                }

                var discountProducts = _mapper.Map<List<DiscountVM>>(discount);

                for (int x=0; x < discountProducts.Count; x++)
                {
                    discountProducts[x].DiscountImage = _utility.BytesToImage(discount[x].DiscountArt);
                }

                return ExecutionResult<List<DiscountVM>>.Success(discountProducts);

            }
            catch(Exception ex)
            {

                return ExecutionResult<List<DiscountVM>>.Failure("Unable to retrieve Discounts." + ex.Message);
            }

        }
    }
}
