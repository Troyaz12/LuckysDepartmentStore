using LuckysDepartmentStore.Data.Stores;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Consumer;

namespace XunitTestProject.Stores
{
    public class PaymentStoreTest
    {
        private LuckysContext GetInMemoryDbContext()
        {
            // Create an in-memory database with a unique name per test
            var options = new DbContextOptionsBuilder<LuckysContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new LuckysContext(options);

            context.SaveChanges();
            return context;
        }

        private LuckysContext GetTestSetup()
        {
            var context = GetInMemoryDbContext();
            context.Payments.Add(
                new Payment {
                    PaymentID = 1,
                    AccountNumber = "123456789",
                    CvcCode = 123,
                    BillingAddress1 = "1125 W FIrst Street",
                    BillingAddress2 = "Apt 123",
                    City = "Dallas",
                    State = "TX",
                    ZipCode = 75002,
                    IsCheckingAccount = false,
                    IsCreditCard = true,
                    IsProcessed = false,
                    ProcessMessage = "Pending",
                    Total = null
                });

            context.PaymentOptions.Add(
                new PaymentOptions
                {
                    FirstName = "Troy",
                    LastName = "Gugler",
                    PaymentOptionsID = 123,
                    RoutingNumber = "123456798",
                    AccountNumber = "151699321",
                    CvcCode = 123,
                    BillingAddress1 = "1234 Oak Street",
                    BillingAddress2 = "Apt 123",
                    City = "Richardson",
                    State = "MN",
                    ZipCode = 45612,
                    IsCheckingAccount = false,
                    IsCreditCard = true,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    UserId = "12345"
                });
                

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetPaymentOptionsCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new PaymentStore(context);

            // Act
            var paymentOptions = await repository.GetPaymentOptions("12345");

            // Assert
            Assert.NotNull(paymentOptions);
            Assert.Equal(1, paymentOptions.Count);
        }

        [Fact]
        public async Task SavePaymentCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new PaymentStore(context);

            Payment payment = new Payment();
            payment.AccountNumber = "4894123123";
            payment.CvcCode = 23156;
            payment.BillingAddress1 = "1125 W FIrst Street";
            payment.BillingAddress2 = "Apt 123";
            payment.City = "Dallas";
            payment.State = "TX";
            payment.ZipCode = 75002;
            payment.IsCheckingAccount = false;
            payment.IsCreditCard = true;
            payment.IsProcessed = false;
            payment.ProcessMessage = "Pending";

            // Act
            await repository.SavePayment(payment);

            var Allpayments = context.Payments.ToListAsync();

            // Assert
            Assert.NotNull(Allpayments);
            Assert.Equal(2, Allpayments.Result.Count);
        }

        [Fact]
        public async Task UpdatePaymentCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new PaymentStore(context);

            var payment = context.Payments.Find(1);
            payment.Total = 100.00m;

            // Act
            int rows = await repository.UpdatePayment(payment);

            var paymentUpdated = context.Payments.FirstOrDefault();

            // Assert
            Assert.NotNull(paymentUpdated);
            Assert.Equal(100.00m, paymentUpdated.Total);
        }

        [Fact]
        public async Task CreatePaymentOptionCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new PaymentStore(context);

            PaymentOptions paymentOptions = new PaymentOptions
            {
                FirstName = "Troy",
                LastName = "Gugler",
                RoutingNumber = "1222256798",
                AccountNumber = "154569321",
                CvcCode = 133,
                BillingAddress1 = "1234 Oak Street",
                BillingAddress2 = "Apt 123",
                City = "Dallas",
                State = "TX",
                ZipCode = 45612,
                IsCheckingAccount = false,
                IsCreditCard = true,
                IsActive = true,
                CreatedDate = DateTime.Now,
                UserId = "12345"

            };
            
            await repository.CreatePaymentOption(paymentOptions);

           var paymentOptionsRes = context.PaymentOptions.ToListAsync();

            // Assert
            Assert.NotNull(paymentOptionsRes);
            Assert.Equal(2, paymentOptionsRes.Result.Count);
        }

        [Fact]
        public async Task EditPaymentOptionCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new PaymentStore(context);

            var paymentOption = new PaymentOptionsVM
            {
                FirstName = "Will",
                LastName = "Sandler",
                PaymentOptionsID = 123,
                AccountNumber = "151699321",
                CvcCode = 123,
                BillingAddress1 = "1234 Oak Street",
                BillingAddress2 = "Apt 123",
                City = "Richardson",
                State = "MN",
                ZipCode = 45612,
                CreatedDate = DateTime.Now,
                UserId = "12345"
            };

            var rowEffected = await repository.EditPaymentOption(paymentOption);

            var paymentOptionValues = await context.PaymentOptions.FindAsync(123);

            // Assert
            Assert.Equal(1, rowEffected);
            Assert.NotNull(paymentOptionValues);
            Assert.Equal("Will", paymentOptionValues.FirstName);
            Assert.Equal("Sandler", paymentOptionValues.LastName);
        }
        [Fact]
        public async Task DeletePaymentOptionCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new PaymentStore(context);

            var shippingAddress = await context.PaymentOptions.FindAsync(123);

            // Act
            await repository.DeletePaymentOption(shippingAddress);

            var shippingAddressSearch = context.ShippingAddress.Find(123);


            // Assert
            Assert.Null(shippingAddressSearch);
        }

        [Fact]
        public async Task GetPaymentOptionByIDCorrect()
        {
            // Arrange
            var context = GetTestSetup();
            var repository = new PaymentStore(context);

            // Act
            var paymentOption = await repository.GetPaymentOptionByID(123);

            // Assert
            Assert.NotNull(paymentOption);            
        }

    }
}
