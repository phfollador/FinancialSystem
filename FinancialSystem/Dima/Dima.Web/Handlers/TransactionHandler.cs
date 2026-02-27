using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
    {
        private readonly HttpClient client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            var result = await client.PostAsJsonAsync("v1/transactions", request);
            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>() 
                ?? new Response<Transaction?>(null, 400, "Nao foi possivel criar a transacao");
        }

        public Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            const string format = "yyyy-MM-dd";

            var startDate = request.StartDate is not null 
                ? request.StartDate.Value.ToString(format) 
                : DateTime.Now.GetFirstDate().ToString(format);

            var endDate = request.EndDate is not null 
                ? request.EndDate.Value.ToString(format)
                : DateTime.Now.GetLastDate().ToString(format);

            var url = $"v1/transactions?startDate={startDate}&endDate={endDate}";

            return await client.GetFromJsonAsync<PagedResponse<List<Transaction>?>>(url) 
                ?? new PagedResponse<List<Transaction>?>(null, 400, "Nao foi possivel obter as transacoes");
        }

        public Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
