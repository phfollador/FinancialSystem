using Dima.Api.Data.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders
{
    public class RefoundOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapPost("/{id}/refound", HandleAsync)
            .WithName("Orders: refound order")
            .WithSummary("Estorna um pedido")
            .WithDescription("Estorna um pedido")
            .WithOrder(6).Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(IOrderHandler handler, long id, ClaimsPrincipal user)
        {
            var request = new RefoundOrderRequest
            {
                Id = id,
                UserId = user.Identity!.Name ?? string.Empty
            };

            var result = await handler.RefoundAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
