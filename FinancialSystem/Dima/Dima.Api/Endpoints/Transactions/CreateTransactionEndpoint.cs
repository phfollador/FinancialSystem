using Dima.Api.Data.Common.Api;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("", HandleAsync)
            .WithName("Transactions: Create")
            .WithSummary("Cria uma nova transaction")
            .WithDescription("Cria uma nova transaction")
            .WithOrder(1)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, CreateTransactionRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);

            if (result.IsSuccess)
                return TypedResults.Created($"/{result.Data?.Id}", result);

            return TypedResults.BadRequest(result);
        }
    }
}
