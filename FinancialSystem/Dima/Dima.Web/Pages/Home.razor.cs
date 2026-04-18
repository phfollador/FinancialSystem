using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages
{
    public partial class HomePage : ComponentBase
    {
        #region Properties

        public bool ShowValues { get; set; } = true;
        public FinancialSummary FinancialSummary { get; set; }

        #endregion

        #region Services

        [Inject]
        public IReportHandler Handler { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            var request = new GetFinancialSummaryRequest();
            var result = await Handler.GetFinancialSummaryReportAsync(request);

            if (result.IsSuccess && result.Data is not null)
                FinancialSummary = result.Data;
        }

        #endregion

        #region Private Methods

        public void ToggleShowValues() => ShowValues = !ShowValues;

        #endregion
    }
}
