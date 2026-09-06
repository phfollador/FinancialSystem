using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Requests.Stripe;
using Dima.Web.Pages.Orders;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        public IJSRuntime JSRuntime { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [Inject]
        public IOrderHandler OrderHandler { get; set; } = null!;

        [Inject]
        public IStripeHandler StripeHandler { get; set; } = null!;

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
            var request = new CreateSessionRequest
            {
                OrderNumber = Order.Number,
                OrderTotal = (int)Math.Round(Order.Total * 100, 2),
                ProductTitle = Order.Product.Title,
                ProductDescription = Order.Product.Desctiption
            };

            try
            {
                var result = await StripeHandler.CreateSessionAsync(request);
                if(result.IsSuccess == false)
                {
                    Snackbar.Add(result.Message!, Severity.Error);
                    return;
                }

                if(result.Data is null)
                {
                    Snackbar.Add(result.Message!, Severity.Error);
                    return;
                }

                await JSRuntime.InvokeVoidAsync("checkout", Configuration.StripePublicKey, result.Data);
            }
            catch
            {
                Snackbar.Add("Nao foi possivel iniciar a sessao com o Stripe", Severity.Error);
                return;
            }
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
