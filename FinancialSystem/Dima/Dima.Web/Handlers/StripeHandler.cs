using Dima.Core.Handlers;
using Dima.Core.Requests.Stripe;
using Dima.Core.Responses;
using Dima.Core.Responses.Stripe;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class StripeHandler(IHttpClientFactory httpClientFactory) : IStripeHandler
    {
        private readonly HttpClient client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request)
        {
            var result = await client.PostAsJsonAsync($"v1/payments/stripe/session", request);
            return await result.Content.ReadFromJsonAsync<Response<string?>>() ?? new Response<string?>(null, 400, "Falha ao criar sessao no stripe");
        }

        public async Task<Response<List<StripeTransactionsResponse>>> GetTransactionsByOrderNumberAsync(GetTransactionsByOrderNumberRequest request)
        {
            var result = await client.PostAsJsonAsync($"v1/payments/stripe/{request.Number}/transactions", request);
            return await result.Content.ReadFromJsonAsync<Response<List<StripeTransactionsResponse>>>() ?? new Response<List<StripeTransactionsResponse>>(null, 400, "Falha ao consultar transacoes do pedido");
        }
    }
}
