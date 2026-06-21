using Dima.Api.Data.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Orders;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders
{
    public class PayOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            throw new NotImplementedException();
        }

        private static async Task<IResult> HandleAsync(IOrderHandler handler, long id, PayOrderRequest request, ClaimsPrincipal user)
        {
            request.Id = id;
            request.UserId = user.Identity!.Name ?? string.Empty;

            var result = await handler.PayAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
