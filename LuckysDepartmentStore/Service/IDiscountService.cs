using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service
{
    public interface IDiscountService
    {
        public void UpdateDiscount(Discount discount);
        public ExecutionResult<int> DeleteDiscount(int discountId);
        public ExecutionResult<DiscountVM> GetDiscount(int discountID);
        public Task<Discount> CreateAsync(DiscountCreateVM discount);
        public ExecutionResult<List<DiscountVM>> GetActiveDiscounts();
    }
}
