using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.Service
{
    public interface IConsumerService
    {
        public ExecutionResult<DiscountVM> GetDiscount(int discountID);
        public Task<Discount> CreateAsync(DiscountCreateVM discount);



    }
}
