using IdentityModel.Client;
using Logistics.IdentityServer.Services.Interfaces;
using Logistics.Models.IdentityModels;
using Logistics.Models.RequestDTO;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Threading.Tasks;

namespace Logistics.IdentityServer.Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<User> ReturnUserIfValid(UserForAuthenticationDto userForAuth)
        {
            var user = await _userManager.FindByNameAsync(userForAuth.UserName);

            var res = await _signInManager.PasswordSignInAsync(userForAuth.UserName, userForAuth.Password, false, false);

            if (res.Succeeded)
            {
                return user;
            }
            return null;
        }

        public async Task<(string accessToken, string refreshToken)> GetTokens(UserForAuthenticationDto user)
        {
            var client = _httpClientFactory.CreateClient();
            PasswordTokenRequest tokenRequest = new PasswordTokenRequest()
            {
                Address = "https://localhost:44320/connect/token",
                ClientId = "APIClient",
                Scope = "Logistics.API offline_access",
                UserName = user.UserName,
                Password = user.Password,
            };
            var tokenResponse = await client.RequestPasswordTokenAsync(tokenRequest);

            return (tokenResponse.AccessToken, tokenResponse.RefreshToken);
        }
    }
}
