using Dima.Core.Handlers;
using Dima.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Orders
{
    public partial class ConfirmOrderPaymentPage : ComponentBase
    {
        #region Parameters

        [Parameter]
        public string Number { get; set; } = string.Empty;

        #endregion

        #region Properties

        public Order? Order { get; set; }

        #endregion

        #region Services

        [Inject]
        public IOrderHandler OrderHandler { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitialized()
        {
            base.OnInitialized();
        }

        #endregion
    }
}
