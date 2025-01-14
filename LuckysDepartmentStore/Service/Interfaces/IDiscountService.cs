using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface IDiscountService
    {
        public Task<ExecutionResult<DiscountEditVM>> UpdateDiscount(DiscountEditVM discount);
        public ExecutionResult<int> DeleteDiscount(int discountId);
        public ExecutionResult<DiscountVM> GetDiscount(int discountID);
        public Task<Discount> CreateAsync(DiscountCreateVM discount);
        public ExecutionResult<List<DiscountVM>> GetActiveDiscounts();
    }
}
