using Dima.Api.Data;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class OrderHandler(AppDbContext context) : IOrderHandler
    {
        public async Task<Response<Order?>> CancelAsync(CancelOrderRequest request)
        {
            Order? order;

            try
            {
                order = await context.Orders
                    .Include(x => x.Product)
                    .Include(x => x.Voucher)
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (order == null)
                    return new Response<Order?>(null, 404, "Pedido nao encontrado");
            }
            catch
            {
                return new Response<Order?>(null, 500, "Falha ao obter pedido");
            }

            switch (order.Status)
            {
                case EOrderStatus.Canceled:
                    return new Response<Order?>(order, 400, "Esse pedido ja foi cancelado");
                case EOrderStatus.WaitingPayment:
                    break;
                case EOrderStatus.Paid:
                    return new Response<Order?>(order, 400, "Esse pedido ja foi pago e nao pode ser cancelado");
                case EOrderStatus.Refounded:
                    return new Response<Order?>(order, 400, "Esse pedido ja foi reembolsado e nao pode ser cancelado");
                default:
                    return new Response<Order?>(order, 400, "Esse pedido nao pode ser cancelado");
            }
        }

        public Task<Response<Order?>> CreateAsync(CreateOrderRequest request)
        {
            throw new NotImplementedException();
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
