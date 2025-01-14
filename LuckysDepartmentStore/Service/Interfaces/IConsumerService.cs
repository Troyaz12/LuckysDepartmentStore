using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Utilities;
using LuckysDepartmentStore.Models.ViewModels.Consumer;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface IConsumerService
    {
        public Task<ExecutionResult<bool>> CreateShippingAddress(ShippingAddressVM shippingAddress);
        public Task<ExecutionResult<List<ShippingAddressVM>>> GetShippingAddress(string userId);
        public Task<ExecutionResult<bool>> CreatePaymentOption(PaymentOptionsVM paymentOption);
        public Task<ExecutionResult<List<PaymentOptionsVM>>> GetPaymentOptions(string userId);

    }
}
