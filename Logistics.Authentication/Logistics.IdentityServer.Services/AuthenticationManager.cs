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

namespace Logistics.IdentityServer.Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration, SignInManager<User> signInManager, IIdentityServerInteractionService interactionService, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _interactionService = interactionService;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<User> ReturnUserIfValid(UserForAuthenticationDto userForAuth)
        {
            var user = await _userManager.FindByNameAsync(userForAuth.UserName);

            var res = await _signInManager.PasswordSignInAsync(userForAuth.UserName, userForAuth.Password, false, false);

            if(res.Succeeded)
            {
                return user;
            }
            return null;
        }

        public async Task<string> CreateToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var keyString = _configuration.GetSection("JwtSettings").GetSection("secretKey").Value;
            var key = Encoding.UTF8.GetBytes(keyString);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

    }
}
