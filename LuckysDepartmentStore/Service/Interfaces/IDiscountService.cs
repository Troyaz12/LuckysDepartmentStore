using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface IDiscountService
    {
        public Task<ExecutionResult<DiscountEditVM>> UpdateDiscount(DiscountEditVM discount);
        public Task<ExecutionResult<int>> DeleteDiscount(int discountId);
        public Task<ExecutionResult<DiscountVM>> GetDiscount(int discountID);
        public Task<Discount> CreateAsync(DiscountCreateVM discount);
        public Task<ExecutionResult<List<DiscountVM>>> GetActiveDiscounts();
        public Task<ExecutionResult<List<DiscountVM>>> GetActiveDiscountsByProductID();
        public Task<ExecutionResult<List<DiscountVM>>> GetActiveDiscountGroups();
    }
}
