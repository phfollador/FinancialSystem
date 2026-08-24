using Dima.Core.Handlers;
using Dima.Core.Requests.Stripe;
using Dima.Core.Responses;
using Dima.Core.Responses.Stripe;

namespace Dima.Api.Handlers
{
    public class StripeHandler : IStripeHandler
    {
        public async Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<StripeTransactionsResponse>>> GetTransactionsByOrderNumberAsync(GetTransactionsByOrderNumberRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
