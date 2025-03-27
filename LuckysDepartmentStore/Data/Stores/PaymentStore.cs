using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Consumer;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Data.Stores
{
    public class PaymentStore : IPaymentStore
    {
        public LuckysContext _context;

        public PaymentStore(LuckysContext context)
        {
            _context = context;
        }

        public async Task CreatePaymentOption(PaymentOptions paymentOption)
        {
            await _context.PaymentOptions.AddAsync(paymentOption);
            await _context.SaveChangesAsync();            
        }

        public async Task SavePayment(Payment payment)
        {
            await _context.Payments.AddAsync(payment);

            await _context.SaveChangesAsync();
        }

        public async Task<int> UpdatePayment(Payment payment)
        {
            if (_context.Entry(payment).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _context.Payments.Update(payment);

            }
            int paymentSaved = await _context.SaveChangesAsync();

            return paymentSaved;
        }

        public async Task<List<PaymentOptionsDTO>> GetPaymentOptions(string userId)
        {
            var allPayments = await _context.PaymentOptions
               .Where(payment => payment.UserId == userId)
               .Select(payment => new PaymentOptionsDTO
               {
                   FirstName = payment.FirstName,
                   LastName = payment.LastName,
                   RoutingNumber = payment.RoutingNumber,
                   AccountNumber = payment.AccountNumber,
                   CvcCode = payment.CvcCode,
                   BillingAddress1 = payment.BillingAddress1,
                   BillingAddress2 = payment.BillingAddress2,
                   City = payment.City,
                   State = payment.State,
                   ZipCode = payment.ZipCode,
                   IsCheckingAccount = payment.IsCheckingAccount,
                   IsCreditCard = payment.IsCreditCard,
                   PaymentOptionsID = payment.PaymentOptionsID
               }).ToListAsync();

            return allPayments;
        }

        public async Task<int> EditPaymentOption(PaymentOptionsVM paymentOptions)
        {
            var paymentOptionsOld = await _context.PaymentOptions.FindAsync(paymentOptions.PaymentOptionsID);

            if (paymentOptionsOld == null)
            {
                return 0;
            }

            paymentOptionsOld.FirstName = paymentOptions.FirstName;
            paymentOptionsOld.LastName = paymentOptions.LastName;
            paymentOptionsOld.BillingAddress1 = paymentOptions.BillingAddress1;
            paymentOptionsOld.BillingAddress2 = paymentOptions.BillingAddress2;
            paymentOptionsOld.City = paymentOptions.City;
            paymentOptionsOld.State = paymentOptions.State;
            paymentOptionsOld.ZipCode = paymentOptions.ZipCode;
            paymentOptionsOld.AccountNumber = paymentOptions.AccountNumber;
            paymentOptionsOld.CvcCode = paymentOptions.CvcCode;

            var paymentSave = _context.SaveChanges();

            return paymentSave;
        }

        public async Task DeletePaymentOption(PaymentOptions paymentOption)
        {
            _context.PaymentOptions.Remove(paymentOption);
            await _context.SaveChangesAsync();
        }

        public async Task<PaymentOptions> GetPaymentOptionByID(int paymentOptionID)
        {

            var paymentOption = await _context.PaymentOptions.FindAsync(paymentOptionID);

            return paymentOption;
        }
    }
}
