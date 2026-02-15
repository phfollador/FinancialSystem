using Dima.Core.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

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

        private async Task<User?> GetUser()
        {
            try
            {
                return await client.GetFromJsonAsync<User?>("v1/identity/manage/info");
            }
            catch
            {
                return null;
            }
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Email, user.Email)
                };

            claims.AddRange(user.Claims.Where(x => x.Key != ClaimTypes.Name && x.Key != ClaimTypes.Email).Select(x => new Claim(x.Key, x.Value)));

            RoleClaim[]? roles;

            try
            {
                roles = await client.GetFromJsonAsync<RoleClaim[]>("v1/identity/roles");
            }
            catch
            {
                return claims;
            }

            foreach(var role in roles ?? [])
            {
                if(!string.IsNullOrEmpty(role.Type) && !string.IsNullOrEmpty(role.Value))
                    claims.Add(new Claim(role.Type, role.Value, role.ValueType, role.Issuer, role.OriginalIssuer));
            }

            return claims;
        }
    }
}
