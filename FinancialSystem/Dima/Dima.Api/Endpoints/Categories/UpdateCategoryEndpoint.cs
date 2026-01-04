using Dima.Api.Data.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapPut("{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
        {
            request.Id = id;
            request.UserId = "teste@pedro"; // fixed to tests

            var result = await handler.UpdateAsync(request);

            if (result.IsSuccess)
                return TypedResults.Ok(result);

            return TypedResults.BadRequest(result);
        }
    }
}
