using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class OrderHandler(IHttpClientFactory httpClientFactory) : IOrderHandler
    {
        private readonly HttpClient client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Order?>> CancelAsync(CancelOrderRequest request)
        {
            var result = await client.PostAsJsonAsync($"v1/orders/{request.Id}/cancel", request);
            return await result.Content.ReadFromJsonAsync<Response<Order?>>() ?? new Response<Order?>(null, 400, "Nao foi possivel cancelar o pedido");
        }

        public async Task<Response<Order?>> CreateAsync(CreateOrderRequest request)
        {
            var result = await client.PostAsJsonAsync($"v1/orders", request);
            return await result.Content.ReadFromJsonAsync<Response<Order?>>() ?? new Response<Order?>(null, 400, "Nao foi possivel criar o pedido");
        }

        public Task<PagedResponse<List<Order>?>> GetAllAsync(GetAllOrdersRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<Order?>> GetByNumberAsync(GetOrderByNumberRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Order?>> PayAsync(PayOrderRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Order?>> RefoundAsync(RefoundOrderRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
