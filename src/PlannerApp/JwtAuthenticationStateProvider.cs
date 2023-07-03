using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace PlannerApp
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly ILocalStorageService _storage;
        public JwtAuthenticationStateProvider(ILocalStorageService storage)
        {
            _storage = storage;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _storage.ContainKeyAsync("access_token"))
            {
                //user is logged in
                var tokenAsString = await _storage.GetItemAsStringAsync("access_token");
                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.ReadJwtToken(tokenAsString);
                var identity = new ClaimsIdentity(token.Claims, "Bearer");
                var user = new ClaimsPrincipal(identity);
                var authState = new AuthenticationState(user);

                NotifyAuthenticationStateChanged(Task.FromResult(authState));

                return authState;

            }

            return new AuthenticationState(new ClaimsPrincipal());//empty means no identity and user not logged in

        }
    }
}


