using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class ReportHandler(IHttpClientFactory httpClientFactory) : IReportHandler
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request)
        {
            return await httpClient.GetFromJsonAsync<Response<List<ExpensesByCategory>?>>("") ?? new Response<List<ExpensesByCategory>?>(null, 400, "Nao foi possivel obter os dados");
        }

        public async Task<Response<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request)
        {
            return await httpClient.GetFromJsonAsync<Response<FinancialSummary?>>("") ?? new Response<FinancialSummary?>(null, 400, "Nao foi possivel obter os dados");
        }

        public async Task<Response<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync(GetIncomesAndExpensesRequest request)
        {
            return await httpClient.GetFromJsonAsync<Response<List<IncomesAndExpenses>?>>("") ?? new Response<List<IncomesAndExpenses>?>(null, 400, "Nao foi possivel obter os dados");
        }

        public async Task<Response<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request)
        {
            return await httpClient.GetFromJsonAsync<Response<List<IncomesByCategory>?>>("") ?? new Response<List<IncomesByCategory>?>(null, 400, "Nao foi possivel obter os dados");
        }
    }
}
