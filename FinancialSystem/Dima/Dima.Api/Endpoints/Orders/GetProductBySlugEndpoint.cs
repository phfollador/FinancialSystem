using Dima.Api.Data.Common.Api;
using Dima.Core.Handlers;

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

        }
    }
}
