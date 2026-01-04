using Dima.Api.Data.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("{id}", HandleAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Obtem uma categoria")
            .WithDescription("Obtem uma categoria")
            .WithOrder(4)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
        {
            var request = new GetCategoryByIdRequest
            {
                Id = id,
                UserId = "teste@pedro"
            };

            var result = await handler.GetByIdAsync(request);

            if (result.IsSuccess)
                return TypedResults.Ok(result);

            return TypedResults.BadRequest(result);
        }
    }
}
