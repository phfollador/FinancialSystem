using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Orders
{
    public partial class CheckoutOrderPage : ComponentBase
    {
        #region Parameters

        [Parameter]
        public string ProductSlug { get; set; } = string.Empty;

        [SupplyParameterFromQuery(Name = "voucher")]
        public string? VoucherNumber { get; set; }

        #endregion

        #region Properties

        public bool IsBusy { get; set; } = false;
        public bool IsValid { get; set; }
        public CreateOrderRequest InputModel { get; set; }  = new();
        public Product? Product { get; set; }
        public Voucher? Voucher { get; set; }
        public decimal Total { get; set; }

        #endregion

        #region Services

        [Inject]
        public IProductHandler ProductHandler { get; set; } = null!;
        [Inject]
        public IOrderHandler OrderHandler { get; set; } = null!;
        [Inject]
        public IVoucherHandler VoucherHandler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Methods
        #endregion
    }
}
