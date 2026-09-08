using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Requests.Stripe;
using Dima.Core.Responses;
using Dima.Core.Responses.Stripe;
using Stripe;
using Stripe.Checkout;

namespace Dima.Api.Handlers
{
    public class StripeHandler : IStripeHandler
    {
        public async Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request)
        {
            var options = new SessionCreateOptions 
            {
                CustomerEmail = request.UserId,
                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        { "order", request.OrderNumber }
                    }
                },
                PaymentMethodTypes = ["card", "boleto", "pix"],
                LineItems = 
                [
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "BRL",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = request.ProductTitle,
                                Description = request.ProductDescription
                            },
                            UnitAmount = request.OrderTotal
                        },
                        Quantity = 1
                    }
                ],
                Mode = "payment",
                SuccessUrl = $"{Configuration.FrontendUrl}/pedidos/{request.OrderNumber}/confirmar",
                CancelUrl = $"{Configuration.FrontendUrl}/pedidos/{request.OrderNumber}/cancelar"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return new Response<string?>(session.Id);
        }

        public async Task<Response<List<StripeTransactionsResponse>>> GetTransactionsByOrderNumberAsync(GetTransactionsByOrderNumberRequest request)
        {
            var options = new ChargeSearchOptions
            {
                Query = $"metadata['order'] : '{request.Number}'"
            };

            var service = new ChargeService();
            var result = await service.SearchAsync(options);

            if (result.Data.Count == 0)
                return new Response<List<StripeTransactionsResponse>>(null, 404, "Nenhuma transacao foi encontrada");

            var data = new List<StripeTransactionsResponse>();
            foreach(var transaction in result.Data)
            {
                data.Add(new StripeTransactionsResponse
                {
                    Id = transaction.Id,
                    Email = transaction.BillingDetails.Email,
                    Amount = transaction.Amount,
                    AmountCaptured = transaction.AmountCaptured,
                    Status = transaction.Status,
                    Paid = transaction.Paid,
                    Refounded = transaction.Refunded
                });
            }

            return new Response<List<StripeTransactionsResponse>>(data);
        }
    }
}
