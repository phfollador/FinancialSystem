using Dima.Api.Data.Common.Api;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;
using System.Transactions;

namespace Dima.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("{id}", HandleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Remove uma transaction")
            .WithDescription("Remove uma transaction")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, long id)
        {
            var request = new DeleteTransactionRequest
            {
                Id = id,
                UserId = user.Identity?.Name ?? string.Empty
            };

            var result = await handler.DeleteAsync(request);

            if (result.IsSuccess)
                return TypedResults.Ok(result);

            return TypedResults.BadRequest(result);
        }
    }
}
