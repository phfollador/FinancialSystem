using Dima.Core.Handlers;
using Dima.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Products
{
    public class ListProductsPage : ComponentBase
    {
        #region Properties

        public List<Product> Products { get; set; } = [];

        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IProductHandler Handler { get; set; } = null!;

        #endregion

    }
}
