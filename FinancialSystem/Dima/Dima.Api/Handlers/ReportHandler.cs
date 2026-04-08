using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dima.Api.Handlers
{
    public class ReportHandler(AppDbContext context) : IReportHandler
    {
        public async Task<Response<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request)
        {
            try
            {
                var data = await context
                    .ExpensesByCategories
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .OrderByDescending(x => x.Year)
                    .ThenBy(x => x.Category)
                    .ToListAsync();

                return new Response<List<ExpensesByCategory>?>(data);
            }
            catch (Exception ex)
            {
                return new Response<List<ExpensesByCategory>?>(null, 500, "Nao foi possivel obter as saidas por categoria");
            }
        }

        public async Task<Response<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request)
        {
            try
            {
                var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var data = await context
                    .Transactions
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId && x.PaidOrReceivedAt >= startDate && x.PaidOrReceivedAt <= DateTime.Now)
                    .GroupBy(x => true)
                    .Select(x => new FinancialSummary(
                        request.UserId, 
                        x.Where(type => type.Type == Core.Enums.ETransactionType.Deposit).Sum(t => t.Amount), 
                        x.Where(type => type.Type == Core.Enums.ETransactionType.Whitdraw).Sum(t => t.Amount))
                    )
                    .FirstOrDefaultAsync();

                return new Response<FinancialSummary?>(data);
            }
            catch
            {
                return new Response<FinancialSummary?>(null, 500, "Nao foi possivel obter o resultado financeiro");
            }
        }

        public async Task<Response<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync(GetIncomesAndExpensesRequest request)
        {
            try
            {
                var data = await context
                    .IncomesAndExpenses
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .OrderByDescending(x => x.Year)
                    .ThenBy(x => x.Month)
                    .ToListAsync();

                return new Response<List<IncomesAndExpenses>?>(data);
            }
            catch (Exception ex) 
            {
                return new Response<List<IncomesAndExpenses>?>(null, 500, "Nao foi possivel obter as entradas e saidas");
            }
        }

        public async Task<Response<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request)
        {
            try
            {
                var data = await context
                    .IncomesByCategories
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .OrderByDescending(x => x.Year)
                    .ThenBy(x => x.Category)
                    .ToListAsync();

                return new Response<List<IncomesByCategory>?>(data);
            }
            catch (Exception ex)
            {
                return new Response<List<IncomesByCategory>?>(null, 500, "Nao foi possivel obter as entradas por categoria");
            }
        }
    }
}
