using Dima.Core.Handlers;
using Dima.Core.Models;
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

        #endregion

        #region Services

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        [Inject]
        public IOrderHandler OrderHandler { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion
    }
}
