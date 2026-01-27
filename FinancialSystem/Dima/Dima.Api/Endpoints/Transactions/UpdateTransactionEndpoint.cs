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
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("{id}", HandleAsync)
            .WithName("Transactions: Update")
            .WithSummary("Atualiza uma transaction")
            .WithDescription("Atualiza uma transaction")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, UpdateTransactionRequest request, long id)
        {
            request.Id = id;
            request.UserId = user.Identity?.Name ?? string.Empty;

            var result = await handler.UpdateAsync(request);

            if (result.IsSuccess)
                return TypedResults.Ok(result);

            return TypedResults.BadRequest(result);
        }
    }
}
