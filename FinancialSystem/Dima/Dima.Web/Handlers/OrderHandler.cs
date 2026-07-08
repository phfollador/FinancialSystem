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

        public async Task<PagedResponse<List<Order>?>> GetAllAsync(GetAllOrdersRequest request)
        {
            return await client.GetFromJsonAsync<PagedResponse<List<Order>?>>("v1/orders") ?? new PagedResponse<List<Order>?>(null, 400, "Nao foi possivel obter os pedidos");
        }

        public Task<PagedResponse<Order?>> GetByNumberAsync(GetOrderByNumberRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Order?>> PayAsync(PayOrderRequest request)
        {
            var result = await client.PostAsJsonAsync($"v1/orders/{request.Id}/refound", request);
            return await result.Content.ReadFromJsonAsync<Response<Order?>>() ?? new Response<Order?>(null, 400, "Nao foi possivel reembolsar o pedido");
        }

        public async Task<Response<Order?>> RefoundAsync(RefoundOrderRequest request)
        {
            var result = await client.PostAsJsonAsync($"v1/orders/{request.Id}/pay", request);
            return await result.Content.ReadFromJsonAsync<Response<Order?>>() ?? new Response<Order?>(null, 400, "Nao foi possivel pagar o pedido");
        }
    }
}
