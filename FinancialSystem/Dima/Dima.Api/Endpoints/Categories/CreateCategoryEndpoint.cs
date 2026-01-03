using Dima.Api.Data.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.Identity.Client;

namespace Dima.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) 
            => app.MapPost("", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria uma nova categoria")
            .WithDescription("Cria uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Category?>>();

        public static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
        {
            var result = await handler.CreateAsync(request);

            if(result.IsSuccess)
                return TypedResults.Created($"/{result.Data?.Id}", result?.Data);

            return TypedResults.BadRequest();
        }
    }
}
