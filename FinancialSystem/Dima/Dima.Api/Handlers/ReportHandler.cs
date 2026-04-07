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
        public Task<Response<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync(GetIncomesAndExpensesRequest request)
        {
            try
            {
                var data = await context
                                .IncomesAndExpenses
                                .AsNoTracking()
                                .Where(x => x.UserId == request.UserId)
                                .OrderBy(x => x.Year)
                                .ThenBy(x => x.Month)
                                .ToListAsync();

                return new Response<List<IncomesAndExpenses>?>(data);
            }
            catch (Exception ex) 
            {
                return new Response<List<IncomesAndExpenses>?>(null, 500, "Nao foi possivel obter as entradas e saidas");
            }
        }

        public Task<Response<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
