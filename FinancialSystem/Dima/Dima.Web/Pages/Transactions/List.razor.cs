using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions
{
    public partial class ListTransactionsPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public List<Transaction> Transactions { get; set; } = [];
        public string SearchTerm { get; set; } = string.Empty;
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public int CurrentMonth { get; set; } = DateTime.Now.Month;
        public int[] Years { get; set; } =
        {
            DateTime.Now.Year,
            DateTime.Now.AddYears(-1).Year,
            DateTime.Now.AddYears(-2).Year,
            DateTime.Now.AddYears(-3).Year
        };

        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [Inject]
        public ITransactionHandler Handler { get; set; } = null!;

        #endregion

        #region Methods

        protected override Task OnInitializedAsync() => GetTransactions();

        private async Task GetTransactions()
        {
            IsBusy = true;

            try
            {
                var request = new GetTransactionsByPeriodRequest
                {
                    StartDate = DateTime.Now.GetFirstDate(CurrentYear, CurrentMonth),
                    EndDate = DateTime.Now.GetLastDate(CurrentYear, CurrentMonth),
                    PageNumber = 1,
                    PageSize = 1000
                };
                var result = await Handler.GetByPeriodAsync(request);

                if (result.IsSuccess)
                {
                    Transactions = result.Data ?? [];
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally { IsBusy = false; }
        }

        private async Task OnDelete(long id, string title)
        {
            IsBusy = true;

            try
            {
                var result = await Handler.DeleteAsync(new DeleteTransactionRequest { Id = id });

                if (result.IsSuccess)
                {
                    Snackbar.Add($"Lançamento {title} removido!", Severity.Success);
                    Transactions.RemoveAll(x => x.Id == id);
                }
                else
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }
            }
            catch(Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally { IsBusy = false; }
        }

        #endregion

        #region Public Methods

        public Func<Transaction, bool> Filter => transaction =>
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return true;

            return transaction.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) || transaction.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase);
        };

        public async void OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await DialogService.ShowMessageBox(
                "ATENCAO", 
                $"Ao prosseguir o lançamento {title} será excluido. Esta é uma ação irreversível! Deseja prosseguir?", 
                yesText: "EXCLUIR", 
                cancelText: "Cancelar");

            if(result is true)
            {
                await OnDelete(id, title);
            }

            StateHasChanged();
        }
        #endregion
    }
}
