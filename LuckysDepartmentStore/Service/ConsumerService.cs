using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Utilities;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models.DTO.Consumer;
using Microsoft.EntityFrameworkCore;
using LuckysDepartmentStore.Models.ViewModels.Product;
using AutoMapper;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Tokens;

namespace LuckysDepartmentStore.Service
{
    public class ConsumerService : IConsumerService
    {
        public LuckysContext _context { get; set; }
        public IMapper _mapper;
        public ConsumerService(LuckysContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExecutionResult<bool>> CreateShippingAddress(ShippingAddressVM shippingAddress)
        {
            if (shippingAddress == null)
            {
                return ExecutionResult<bool>.Failure("Did not receive an address.");
            }

            try
            {
                ShippingAddress newAddress = new ShippingAddress();

                newAddress.FirstName = shippingAddress.FirstName;
                newAddress.LastName = shippingAddress.LastName;
                newAddress.Address1 = shippingAddress.Address1;
                newAddress.Address2 = shippingAddress.Address2;
                newAddress.City = shippingAddress.City;
                newAddress.State = shippingAddress.State;
                newAddress.ZipCode = shippingAddress.ZipCode;
                newAddress.UserId = shippingAddress.UserId;

                var addressId = await _context.ShippingAddress.AddAsync(newAddress);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ExecutionResult<bool>.Failure("Unable to save address.");
            }

            return ExecutionResult<bool>.Success(true);
        }

        public async Task<ExecutionResult<List<ShippingAddressVM>>> GetShippingAddress(string userId)
        {
            if (userId == null)
            {
                return ExecutionResult<List<ShippingAddressVM>>.Failure("UserId not recieved.");
            }

            try
            {
                var allAddresses = await _context.ShippingAddress
                .Where(address => address.UserId == userId)
                .Select(address => new ShippingAddressDTO
                { 
                    FirstName = address.FirstName,
                    LastName = address.LastName,
                    Address1 = address.Address1,
                    Address2 = address.Address2,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode,
                    ShippingAddressID = address.ShippingAddressID
                  })
                .ToListAsync();

                var addressListVM = _mapper.Map<List<ShippingAddressVM>>(allAddresses);

                return ExecutionResult<List<ShippingAddressVM>>.Success(addressListVM);
            }
            catch (Exception ex)
            {
                return ExecutionResult<List<ShippingAddressVM>>.Failure("Unable to get address.");
            }


          
        }
        public async Task<ExecutionResult<bool>> CreatePaymentOption(PaymentOptionsVM paymentOption)
        {
            if (paymentOption == null)
            {
                return ExecutionResult<bool>.Failure("Payment data not recieved.");
            }

            try
            {
                var newPaymentOption = _mapper.Map<PaymentOptions>(paymentOption);

                var paymentOptionId = await _context.PaymentOptions.AddAsync(newPaymentOption);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ExecutionResult<bool>.Failure("Unable to add payment data.");
            }

            return ExecutionResult<bool>.Success(true);
        }

        public async Task<ExecutionResult<List<PaymentOptionsVM>>> GetPaymentOptions(string userId)
        {
            if (userId == null)
            {
                return ExecutionResult<List<PaymentOptionsVM>>.Failure("UserId not recieved.");
            }

            try
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
                })
                .ToListAsync();

                var paymentListVM = _mapper.Map<List<PaymentOptionsVM>>(allPayments);

                return ExecutionResult<List<PaymentOptionsVM>>.Success(paymentListVM);
            }
            catch (Exception ex)
            {
                return ExecutionResult<List<PaymentOptionsVM>>.Failure("Unable to get address.");
            }

        }
        public async Task<ExecutionResult<ShippingAddress>> DeleteShippingRecord(int id)
        {
            try
            {
                if (id == null)
                {
                    return ExecutionResult<ShippingAddress>.Failure("Payment data not recieved.");
                }
                var shippingAddress = await _context.ShippingAddress.FindAsync(id);

                if (shippingAddress == null)
                {
                    return ExecutionResult<ShippingAddress>.Failure("An error occured. Cannot find delete product.");
                }

                _context.ShippingAddress.Remove(shippingAddress);
                await _context.SaveChangesAsync();

                return ExecutionResult<ShippingAddress>.Success(shippingAddress);
            }
            catch (Exception ex)
            {
                return ExecutionResult<ShippingAddress>.Failure("Unable to get address.");
            }           
        }
        public async Task<ExecutionResult<ShippingAddressVM>> EditShippingRecord(ShippingAddressVM shippingAddress)
        {
            try
            {
                if (shippingAddress == null)
                {
                    return ExecutionResult<ShippingAddressVM>.Failure("No shipping address selected.");
                }

                var shippingAddressOld = await _context.ShippingAddress.FindAsync(shippingAddress.ShippingAddressID);

                if (shippingAddressOld == null)
                {
                    return ExecutionResult<ShippingAddressVM>.Failure("No shipping address selected.");
                }

                shippingAddressOld.FirstName = shippingAddress.FirstName;
                shippingAddressOld.LastName = shippingAddress.LastName;
                shippingAddressOld.Address1 = shippingAddress.Address1;
                shippingAddressOld.Address2 = shippingAddress.Address2;
                shippingAddressOld.City = shippingAddress.City;
                shippingAddressOld.State = shippingAddress.State;
                shippingAddressOld.ZipCode =shippingAddress.ZipCode;

                var productSave = _context.SaveChanges();

                return ExecutionResult<ShippingAddressVM>.Success(shippingAddress);
            }
            catch (Exception ex)
            {
                return ExecutionResult<ShippingAddressVM>.Failure("Unable to edit product.");
            }
        }
        public async Task<ExecutionResult<PaymentOptionsVM>> EditPaymentRecord(PaymentOptionsVM paymentOptions)
        {
            try
            {
                if (paymentOptions == null)
                {
                    return ExecutionResult<PaymentOptionsVM>.Failure("No payment option selected.");
                }

                var paymentOptionsOld = await _context.PaymentOptions.FindAsync(paymentOptions.PaymentOptionsID);

                if (paymentOptionsOld == null)
                {
                    return ExecutionResult<PaymentOptionsVM>.Failure("No payment option selected.");
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

                return ExecutionResult<PaymentOptionsVM>.Success(paymentOptions);
            }
            catch (Exception ex)
            {
                return ExecutionResult<PaymentOptionsVM>.Failure("Unable to edit product.");
            }
        }
        public async Task<ExecutionResult<PaymentOptions>> DeletePaymentRecord(int id)
        {
            try
            {
                if (id == null)
                {
                    return ExecutionResult<PaymentOptions>.Failure("Payment data not recieved.");
                }
                var paymentRecord = await _context.PaymentOptions.FindAsync(id);

                if (paymentRecord == null)
                {
                    return ExecutionResult<PaymentOptions>.Failure("An error occured. Cannot find delete product.");
                }

                _context.PaymentOptions.Remove(paymentRecord);
                await _context.SaveChangesAsync();

                return ExecutionResult<PaymentOptions>.Success(paymentRecord);
            }
            catch (Exception ex)
            {
                return ExecutionResult<PaymentOptions>.Failure("Unable to get address.");
            }
        }

    }
}
