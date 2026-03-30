using Dima.Api.Data;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreateAt = DateTime.UtcNow,
                    Amount = request.Amount > 0 && request.Type == Core.Enums.ETransactionType.Whitdraw ? request.Amount *= -1 : request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transacao criada com sucesso!");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Nao foi possivel criar uma transacao");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);

                if (transaction == null)
                    return new Response<Transaction?>(null, 404, "Nao foi possivel recuperar uma transacao");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Nao foi possivel remover uma transacao");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);

                if (transaction == null)
                    return new Response<Transaction?>(null, 404, "Nao foi possivel recuperar uma transacao");

                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Nao foi possivel obter uma transacao");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDate();
                request.EndDate ??= DateTime.Now.GetLastDate();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Nao foi possivel determinar uma data de inicio ou termino");
            }

            try
            {
                var query = context.Transactions
                    .AsNoTracking()
                    .Where(x => x.PaidOrReceivedAt >= request.StartDate && x.PaidOrReceivedAt <= request.EndDate && x.UserId == request.UserId)
                    .OrderBy(x => x.PaidOrReceivedAt);

                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query
                    .CountAsync();

                if (transactions == null)
                    return new PagedResponse<List<Transaction>?>(null, 500, "Nenhuma transacao foi encontrada");

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Nao foi possivel consultar as trasacoes");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.Id);

                if (transaction == null)
                    return new Response<Transaction?>(null, 404, "Nao foi possivel recuperar uma transacao");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount > 0 && request.Type == Core.Enums.ETransactionType.Whitdraw ? request.Amount *= -1 : request.Amount;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
                transaction.Title = request.Title;
                transaction.Type = request.Type;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Nao foi possivel atualizar uma transacao");
            }
        }
    }
}
