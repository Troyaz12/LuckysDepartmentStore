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
using LuckysDepartmentStore.Data.Stores.Interfaces;

namespace LuckysDepartmentStore.Service
{
    public class ConsumerService : IConsumerService
    {
        public LuckysContext _context { get; set; }
        public IMapper _mapper;
        private readonly ICustomerStore _customerStore;
        private readonly IPaymentStore _paymentStore;

        public ConsumerService(LuckysContext context, IMapper mapper, ICustomerStore customerStore, IPaymentStore paymentStore)
        {
            _context = context;
            _mapper = mapper;
            _customerStore = customerStore;
            _paymentStore = paymentStore;
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

                await _customerStore.CreateCustomerShippingAddress(newAddress);
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
                var allAddresses = await _customerStore.GetShippingAddress(userId);

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
                await _paymentStore.CreatePaymentOption(newPaymentOption);
                
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
                var allPayments = await _paymentStore.GetPaymentOptions(userId);

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
                    return ExecutionResult<ShippingAddress>.Failure("Shipping data not recieved.");
                }
                var shippingAddress = await _customerStore.GetShippingAddressByID(id);

                if (shippingAddress == null)
                {
                    return ExecutionResult<ShippingAddress>.Failure("An error occured. Cannot find delete product.");
                }

                
                await _customerStore.DeleteShippingAddress(shippingAddress);

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

                int rowsEffected = await _customerStore.UpdateShippingAddress(shippingAddress);

                if (rowsEffected == 0)
                {
                    return ExecutionResult<ShippingAddressVM>.Failure("Shipping address could not be updated.");
                }

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

                int rowsEffected = await _paymentStore.EditPaymentOption(paymentOptions);

                if (rowsEffected == 0)
                {
                    return ExecutionResult<PaymentOptionsVM>.Failure("Could not update payment option.");
                }

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
                var paymentRecord = await _paymentStore.GetPaymentOptionByID(id);

                if (paymentRecord == null)
                {
                    return ExecutionResult<PaymentOptions>.Failure("An error occured. Cannot find delete product.");
                }

                await _paymentStore.DeletePaymentOption(paymentRecord);

                return ExecutionResult<PaymentOptions>.Success(paymentRecord);
            }
            catch (Exception ex)
            {
                return ExecutionResult<PaymentOptions>.Failure("Unable to get address.");
            }
        }

    }
}
