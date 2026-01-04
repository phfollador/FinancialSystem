using Dima.Api.Data.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Remove uma categoria")
            .WithDescription("Remove uma categoria")
            .WithOrder(3)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
        {
            var request = new DeleteCategoryRequest
            {
                Id = id,
                UserId = "teste@pedro"
            };

            var result = await handler.DeleteAsync(request);

            if (result.IsSuccess)
                return TypedResults.Ok(result);

            return TypedResults.BadRequest(result);
        }
    }
}
