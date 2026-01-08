using Dima.Api.Data.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("{id}", HandleAsync)
            .WithName("Transactions: Get By Id")
            .WithSummary("Obtem uma transaction")
            .WithDescription("Obtem uma transaction")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
        {
            var request = new GetTransactionByIdRequest
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
