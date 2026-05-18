using Dima.Core.Models;
using Dima.Core.Requests;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Handlers
{
    internal interface IOrderHandler
    {
        Task<Response<Order?>> CancelAsync(CancelOrderRequest request);
        Task<Response<Order?>> CreateAsync(CreateOrderRequest request);
        Task<Response<Order?>> PayAsync(PayOrderRequest request);
        Task<Response<Order?>> RefoundAsync(RefoundOrderRequest request);
        Task<PagedResponse<List<Order>?>> GetAllAsync(GetAllOrdersRequest request);
        Task<PagedResponse<Order?>> GetByNumberAsync(GetOrderByNumberRequest request);
    }
}
