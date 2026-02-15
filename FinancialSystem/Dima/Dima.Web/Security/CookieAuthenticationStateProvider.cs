using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Security
{
    public class CookieAuthenticationStateProvider(IHttpClientFactory clientFactory) : AuthenticationStateProvider, ICookieAuthenticationStateProvider
    {
        private bool isAuthenticated = false;
        private readonly HttpClient client = clientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<bool> CheckAuthenticationAsync()
        {
            await GetAuthenticationStateAsync();
            return isAuthenticated;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }

        public void NotifyAuthenticationStateChanged()
        {
            base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
