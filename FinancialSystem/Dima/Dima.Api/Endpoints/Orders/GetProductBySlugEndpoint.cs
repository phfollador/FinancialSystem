using Dima.Api.Data.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Orders;

namespace Dima.Api.Endpoints.Orders
{
    public class GetProductBySlugEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            throw new NotImplementedException();
        }

        private static async Task<IResult> HandleAsync(IProductHandler handler, string slug)
        {
            var request = new GetProductBySlugRequest
            {
                Slug = slug
            };

            var result = await handler.GetBySlugAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
