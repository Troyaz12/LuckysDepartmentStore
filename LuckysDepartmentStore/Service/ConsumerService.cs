using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Utilities;
using LuckysDepartmentStore.Service;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models.DTO.Consumer;
using Microsoft.EntityFrameworkCore;
using LuckysDepartmentStore.Models.ViewModels.Product;
using AutoMapper;

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
                var allAddresses = await _context.PaymentOptions
                .Where(address => address.UserId == userId)
                .Select(address => new PaymentOptionsDTO
                {
                   RoutingNumber = address.RoutingNumber,
                   AccountNumber = address.AccountNumber,
                   CvcCode = address.CvcCode,
                   BillingAddress1 = address.BillingAddress1,
                   BillingAddress2 = address.BillingAddress2,
                   City = address.City,
                   State = address.State,
                   ZipCode = address.ZipCode,
                   IsCheckingAccount = address.IsCheckingAccount,
                   IsCreditCard = address.IsCreditCard,
                   PaymentOptionsID = address.PaymentOptionsID
                })
                .ToListAsync();

                var addressListVM = _mapper.Map<List<PaymentOptionsVM>>(allAddresses);

                return ExecutionResult<List<PaymentOptionsVM>>.Success(addressListVM);
            }
            catch (Exception ex)
            {
                return ExecutionResult<List<PaymentOptionsVM>>.Failure("Unable to get address.");
            }

        }
    }
}
