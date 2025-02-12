using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Utilities;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface IConsumerService
    {
        public Task<ExecutionResult<bool>> CreateShippingAddress(ShippingAddressVM shippingAddress);
        public Task<ExecutionResult<List<ShippingAddressVM>>> GetShippingAddress(string userId);
        public Task<ExecutionResult<bool>> CreatePaymentOption(PaymentOptionsVM paymentOption);
        public Task<ExecutionResult<List<PaymentOptionsVM>>> GetPaymentOptions(string userId);
        public Task<ExecutionResult<ShippingAddress>> DeleteShippingRecord(int id);
        public Task<ExecutionResult<ShippingAddressVM>> EditShippingRecord(ShippingAddressVM shippingAddress);
        public Task<ExecutionResult<PaymentOptionsVM>> EditPaymentRecord(PaymentOptionsVM paymentOptions);
        public Task<ExecutionResult<PaymentOptions>> DeletePaymentRecord(int id);
    }
}
