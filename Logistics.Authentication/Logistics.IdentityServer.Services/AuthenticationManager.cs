using IdentityServer4.Services;
using Logistics.IdentityServer.Entities.Models;
using Logistics.IdentityServer.Services.Interfaces;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using IdentityModel;

namespace Logistics.IdentityServer.Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration, SignInManager<User> signInManager, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
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
                ClientId = "APIUser",
                Scope = "Logistics.API",
                UserName = user.UserName,
                Password = user.Password,
            };
            var tokenResponse = await client.RequestPasswordTokenAsync(tokenRequest);

            return (tokenResponse.AccessToken, tokenResponse.RefreshToken) ;
        }
    }
}
