using Dima.Core.Handlers;
using Microsoft.AspNetCore.Components;

namespace Dima.Web.Pages.Categories
{
    public partial class EditCategoryPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;

        #endregion

        #region Parameters

        [Parameter]
        public string Id { get; set; } = string.Empty;

        #endregion

        #region Services

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;

        #endregion
    }
}
