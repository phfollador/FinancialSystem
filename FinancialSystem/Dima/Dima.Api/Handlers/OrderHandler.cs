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

            order.Status = EOrderStatus.Canceled;
            order.UpdatedAt = DateTime.Now;

            try
            {
                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }
            catch
            {
                return new Response<Order?>(order, 500, "Nao foi possivel cancelar seu pedido");
            }

            return new Response<Order?>(order, 200, $"Pedido {order.Number} cancelado com sucesso");
        }

        public async Task<Response<Order?>> CreateAsync(CreateOrderRequest request)
        {
            Product? product;

            try
            {
                product = await context
                    .Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.ProductId && x.IsActive == true);

                if (product is null)
                    return new Response<Order?>(null, 400, "Produto nao encontrado");

                context.Attach(product);
            }
            catch
            {
                return new Response<Order?>(null, 500, "Nao foi possivel obter o produto");
            }

            Voucher? voucher = null;

            try
            {
                if(request.VoucherId != null)
                {
                    voucher = await context
                        .Vouchers
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == request.VoucherId && x.IsActive == true);

                    if (voucher is null)
                        return new Response<Order?>(null, 400, "Voucher invalido ou nao encontrado");

                    if(!voucher.IsActive)
                        return new Response<Order?>(null, 400, "Este voucher ja foi utilizado");

                    voucher.IsActive = false;
                    context.Vouchers.Update(voucher);
                }
            }
            catch
            {
                return new Response<Order?>(null, 500, "Falha ao obter o voucher informado");
            }

            var order = new Order
            {
                UserId = request.UserId,
                Product = product,
                ProductId = request.ProductId,
                Voucher = voucher,
                VoucherId = request.VoucherId
            };

            try
            {
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();
            }
            catch
            {
                return new Response<Order?>(null, 500, "Nao foi possovel concluir seu pedido");
            }

            return new Response<Order?>(order, 201, $"Pedido n {order.Number} feito com sucesso");
        }

        public async Task<PagedResponse<List<Order>?>> GetAllAsync(GetAllOrdersRequest request)
        {
            try
            {
                var query = context
                    .Orders
                    .AsNoTracking()
                    .Include(x => x.Product)
                    .Include(x => x.Voucher)
                    .Where(x => x.UserId == request.UserId)
                    .OrderByDescending(x => x.CreatedAt);

                var orders = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Order>?>(orders, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Order>?>(null, 500, "Nao foi possivel obter os pedidos");
            }
        }

        public async Task<PagedResponse<Order?>> GetByNumberAsync(GetOrderByNumberRequest request)
        {
            try
            {
                var order = await context
                    .Orders
                    .AsNoTracking()
                    .Include(x => x.Product)
                    .Include(x => x.Voucher)
                    .FirstOrDefaultAsync(x => x.Number == request.Number && x.UserId == request.UserId);

                return order is null ? new PagedResponse<Order?>(null, 404, "Pedido nao encontrado") : new PagedResponse<Order?>(order);
            }
            catch
            {
                return new PagedResponse<Order?>(null, 500, "Nao foi possivel recuperar esse pedido");
            }
        }

        public async Task<Response<Order?>> PayAsync(PayOrderRequest request)
        {
            Order? order;

            try
            {
                order = await context.Orders
                    .Include(x => x.Product)
                    .Include(x => x.Voucher)
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (order is null)
                    return new Response<Order?>(null, 404, "Pedido nao encontrado");
            }
            catch
            {
                return new Response<Order?>(null, 500, "Falha ao consultar pedido");
            }

            switch (order.Status)
            {
                case EOrderStatus.Canceled:
                    return new Response<Order?>(order, 400, "Esse pedido foi cancelado e nao pode ser pago");

                case EOrderStatus.Paid:
                    return new Response<Order?>(order, 400, "Esse pedido ja esta pago");

                case EOrderStatus.Refounded:
                    return new Response<Order?>(order, 400, "Esse pedido ja foi reembolsado e nao pode ser pago");

                case EOrderStatus.WaitingPayment:
                    break;

                default:
                    return new Response<Order?>(order, 400, "Nao foi possivel pagar o pedido");
            }

            order.Status = EOrderStatus.Paid;
            order.ExternalReference = request.ExternalReference;
            order.UpdatedAt = DateTime.Now;

            try
            {
                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }
            catch
            {
                return new Response<Order?>(order, 500, "Falha ao tentar pagar o pedido");
            }

            return new Response<Order?>(order, 200, $"Pedido {order.Number} pago com sucesso");
        }

        public async Task<Response<Order?>> RefoundAsync(RefoundOrderRequest request)
        {
            Order? order;

            try
            {
                order = await context.Orders.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if(order is null)
                    return new Response<Order?>(null, 404, "Esse pedido nao foi encontrado");
            }
            catch
            {
                return new Response<Order?>(null, 500, "Nao foi possivel recuperar seu pedido");
            }

            switch (order.Status)
            {
                case EOrderStatus.Canceled:
                    return new Response<Order?>(order, 400, "Esse pedido foi cancelado e nao pode ser estornado");

                case EOrderStatus.Paid:
                    break;

                case EOrderStatus.Refounded:
                    return new Response<Order?>(order, 400, "Esse pedido ja foi reembolsado");

                case EOrderStatus.WaitingPayment:
                    return new Response<Order?>(order, 400, "Esse pedido ainda nao foi pago e nao pode ser reembolsado");

                default:
                    return new Response<Order?>(order, 400, "Nao foi possivel reembolsar o pedido");
            }

            order.Status = EOrderStatus.Refounded;
            order.UpdatedAt = DateTime.Now;

            try
            {
                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }
            catch
            {
                return new Response<Order?>(order, 500, "Falha ao reembolsar o pagamento");
            }

            return new Response<Order?>(order, 200, $"Pedido {order.Number} estornado com sucesso");
        }
    }
}
