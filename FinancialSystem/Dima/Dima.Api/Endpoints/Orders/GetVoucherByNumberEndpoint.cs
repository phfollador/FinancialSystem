using Dima.Api.Data.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Orders;

namespace Dima.Api.Endpoints.Orders
{
    public class GetVoucherByNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            throw new NotImplementedException();
        }

        private static async Task<IResult> HandleAsync(IVoucherHandler handler, string number)
        {
            var request = new GetVoucherByNumberRequest
            {
                Number = number
            };

            var result = await handler.GetByNumberAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
