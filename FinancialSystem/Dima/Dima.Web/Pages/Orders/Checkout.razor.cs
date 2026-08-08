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

        public PatternMask Mask { get; set; } = new("####-####")
        {
            MaskChars = [new MaskChar('#', @"[0-9a-fA-F]")],
            Placeholder = '_',
            CleanDelimiters = true,
            Transformation = AllUpperCase
        };

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

        protected override async Task OnInitializedAsync()
        {
            // recuperar o produto
            try
            {
                var result = await ProductHandler.GetBySlugAsync(new GetProductBySlugRequest
                {
                    Slug = ProductSlug
                });

                if (!result.IsSuccess)
                {
                    Snackbar.Add("Nao foi possivel obter o produto", Severity.Error);
                    IsValid = false;
                    return;
                }

                Product = result.Data;
            }
            catch
            {
                Snackbar.Add("Nao foi possivel obter o produto", Severity.Error);
                IsValid = false;
                return;
            }

            if(Product is null)
            {
                Snackbar.Add("Nao foi possivel obter o produto", Severity.Error);
                IsValid = false;
                return;
            }

            // recuperar o voucher
            if (string.IsNullOrEmpty(VoucherNumber) == false)
            {
                try 
                {
                    var result = await VoucherHandler.GetByNumberAsync(new GetVoucherByNumberRequest
                    {
                        Number = VoucherNumber.Replace("-", "")
                    });

                    if (!result.IsSuccess)
                    {
                        VoucherNumber = string.Empty;
                        Snackbar.Add("Nao foi possivel obter o voucher", Severity.Error);
                    }

                    if(result.Data is null)
                    {
                        VoucherNumber = string.Empty;
                        Snackbar.Add("Nao foi possivel obter o voucher", Severity.Error);
                    }

                    Voucher = result.Data;
                }
                catch
                {
                    VoucherNumber = string.Empty;
                    Snackbar.Add("Nao foi possivel obter o voucher", Severity.Error);
                }
            }

            IsValid = true;
            Total = Product.Price - (Voucher?.Amount ?? 0);
        }

        private static char AllUpperCase(char s)
        {
            return s.ToString().ToUpperInvariant()[0];
        }

        #endregion
    }
}
