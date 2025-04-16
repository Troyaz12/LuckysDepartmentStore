using AutoMapper;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly IUtility _utility;
        private readonly IDiscountStore _discountStore;
        private readonly ILogger _logger;

        public DiscountService(IMapper mapper, IUtility utility, IDiscountStore discountStore, ILogger<IDiscountService> logger)
        {
            _mapper = mapper;
            _utility = utility;
            _discountStore = discountStore;
            _logger = logger;
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
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to delete discount {@discountId} in database.", discountId);
                return ExecutionResult<int>.Failure("Unable to delete discount in database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete discount {@discountId}", discountId);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get discount {@discountId}", discountId);
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
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to update discount {@discount} in database.", discount);
                return ExecutionResult<DiscountEditVM>.Failure("Unable to update discount.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update discount {@discount}", discount);
                return ExecutionResult<DiscountEditVM>.Failure("Unable to update discount.");
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
                _logger.LogError(ex, "Failed to create discount {@discount} in database.", discount);
                throw new Exception("An error occurred while processing your request", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create discount {@discount}", discount);
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
                _logger.LogError(ex, "Failed to get active discounts");
                return ExecutionResult<List<DiscountVM>>.Failure("Unable to retrieve Discounts." + ex.Message);
            }

        }
    }
}
