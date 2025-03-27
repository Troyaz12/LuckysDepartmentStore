using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Consumer;
using LuckysDepartmentStore.Models.ViewModels.Consumer;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface IPaymentStore
    {
        public Task SavePayment(Payment payment);
        public Task<int> UpdatePayment(Payment payment);
        public Task CreatePaymentOption(PaymentOptions payment);
        public Task<List<PaymentOptionsDTO>> GetPaymentOptions(string userId);
        public Task<int> EditPaymentOption(PaymentOptionsVM paymentOptions);
        public Task<PaymentOptions> GetPaymentOptionByID(int paymentOptionID);
        public Task DeletePaymentOption(PaymentOptions paymentOption);
    }
}
