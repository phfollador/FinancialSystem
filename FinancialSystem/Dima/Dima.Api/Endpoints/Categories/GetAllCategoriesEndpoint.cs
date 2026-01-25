using Dima.Api.Data.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All Categories")
            .WithSummary("Obtem todas as categoria")
            .WithDescription("Obtem todas as categoria")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, [FromQuery]int pageNumber = Configuration.DefaultPageNumber, [FromQuery]int pageSize = Configuration.DefaultPageSige)
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await handler.GetAllAsync(request);

            if (result.IsSuccess)
                return TypedResults.Ok(result);

            return TypedResults.BadRequest(result);
        }
    }
}
