using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Web.Pages.Orders;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Components.Orders
{
    public partial class OrderActionComponent : ComponentBase
    {
        #region Parameters

        [Parameter]
        [EditorRequired]
        public Order Order { get; set; } = null!;

        [CascadingParameter]
        public DetailsPage Parent { get; set; } = null!;

        #endregion

        #region Services

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [Inject]
        public IOrderHandler OrderHandler { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Public Methods

        public async void OnCancelButtonClicked()
        {
            bool? result = await DialogService.ShowMessageBox("ATENÇÃO", "Deseja realmente cancelar esse pedido?", "SIM", "NÃO");

            if (result is not null && result == true)
                await CancelOrderAsync();
        }

        public async void OnPayButtonClicked()
        {
            await PayOrderAsync();
        }

        public async void OnRefoundButtonClicked()
        {
            bool? result = await DialogService.ShowMessageBox("ATENÇÃO", "Deseja realmente estornar esse pedido?", "SIM", "NÃO");

            if (result is not null && result == true)
                await RefoundOrderAsync();
        }

        #endregion

        #region Private Methods

        private async Task CancelOrderAsync()
        {
            var request = new CancelOrderRequest
            {
                Id = Order.Id
            };

            var result = await OrderHandler.CancelAsync(request);
            if (result.IsSuccess)
                Parent.RefreshOrderStatus(result.Data!);
            else
                Snackbar.Add(result.Message!, Severity.Error);
        }

        private async Task PayOrderAsync()
        {
            await Task.Delay(1);
            Snackbar.Add("Pagamento nao implementado", Severity.Error);
        }

        private async Task RefoundOrderAsync()
        {
            var request = new RefoundOrderRequest
            {
                Id = Order.Id
            };

            var result = await OrderHandler.RefoundAsync(request);
            if (result.IsSuccess)
                Parent.RefreshOrderStatus(result.Data!);
            else
                Snackbar.Add(result.Message!, Severity.Error);
        }

        #endregion
    }
}
