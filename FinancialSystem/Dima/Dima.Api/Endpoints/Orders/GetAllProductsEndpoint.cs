using Dima.Api.Data.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Orders
{
    public class GetAllProductsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            throw new NotImplementedException();
        }

        private static async Task<IResult> HandleAsync(IProductHandler handler, [FromQuery] int pageNumber = Configuration.DefaultPageNumber, [FromQuery] int pageSize = Configuration.DefaultPageSige)
        {
            var request = new GetAllProductsRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSuccess ? TypedResults.Ok(request) : TypedResults.BadRequest(request);
        }
    }
}
