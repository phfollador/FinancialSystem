using Dima.Api.Data.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders
{
    public class CreateOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapPost("/", HandleAsync)
            .WithName("Orders: Creates a new order")
            .WithSummary("Cria um novo pedido")
            .WithDescription("Cria um novo pedido")
            .Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(IOrderHandler handler, CreateOrderRequest request, ClaimsPrincipal user)
        {
            var result = await handler.CreateAsync(request);
            return result.IsSuccess ? TypedResults.Created($"v1/orders/{result.Data?.Number}") : TypedResults.BadRequest(result);
        }
    }
}
