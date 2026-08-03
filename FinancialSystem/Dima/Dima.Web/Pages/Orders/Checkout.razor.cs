using Microsoft.AspNetCore.Components;

namespace Dima.Web.Pages.Orders
{
    public partial class CheckoutOrderPage : ComponentBase
    {
        #region Parameters

        [Parameter]
        public string ProductSlug { get; set; } = string.Empty;

        [SupplyParameterFromQuery]
        public string? VoucherNumber { get; set; }

        #endregion
    }
}
