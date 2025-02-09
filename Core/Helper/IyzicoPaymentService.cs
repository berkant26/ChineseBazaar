using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class PaymentService
    {
        private readonly Options _options;

        public PaymentService()
        {
            _options = new Options
            {
                ApiKey = "sandbox-c8xW2OaHfbe1AtBDnY5FTRsANQTi3BMb",
                SecretKey = "sandbox-sOzKTeOaWOwR23sbjBse8rirIoEnTbFn",
                BaseUrl = "https://sandbox-api.iyzipay.com" // Use prod URL for live environment
            };
        }

        public async Task<Payment> CreatePayment(decimal price, string cardHolderName, string cardNumber, string expireMonth, string expireYear, string cvc)
        {
            var request = new CreatePaymentRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Price = price.ToString("F2", CultureInfo.InvariantCulture),
                PaidPrice = price.ToString("F2", CultureInfo.InvariantCulture),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = "BASKET_123",
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString()
            };

            // Kredi kartı bilgileri
            request.PaymentCard = new PaymentCard
            {
                CardHolderName = cardHolderName,
                CardNumber = cardNumber,
                ExpireMonth = expireMonth,
                ExpireYear = expireYear,
                Cvc = cvc,
                RegisterCard = 0
            };

            // Kullanıcı bilgileri (Dummy veriler)
            request.Buyer = new Buyer
            {
                Id = "BUYER123",
                Name = "John",
                Surname = "Doe",
                GsmNumber = "+905350000000",
                Email = "johndoe@email.com",
                IdentityNumber = "74300864791",
                LastLoginDate = "2025-02-06 12:00:00",
                RegistrationDate = "2024-01-01 12:00:00",
                RegistrationAddress = "Istanbul, Turkey",
                Ip = "85.34.78.112",
                City = "Istanbul",
                Country = "Turkey"
            };

            var address = new Address
            {
                ContactName = "John Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Test Address"
            };
            request.ShippingAddress = address;
            request.BillingAddress = address;

            // Sepet içeriği (Dummy)
            request.BasketItems = new List<BasketItem>
            {
                new BasketItem
                {
                    Id = "BI101",
                    Name = "Product 1",
                    Category1 = "Category",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = price.ToString("F2", CultureInfo.InvariantCulture)
                }
            };

            // Await the asynchronous method to get the result
            return await Payment.Create(request, _options);
        }
    }
}
