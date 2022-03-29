using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RequestHandler.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Utils
{
    public class HttpClientFactoryHandler : IHttpClientFactoryHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        public HttpClientFactoryHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContext = httpContext;
        }

        public async Task<HttpClient> GetAPIClient()
        {
            var accessToken = await _httpContext.HttpContext.GetTokenAsync("access_token");

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.BaseAddress = new Uri(_configuration.GetSection("ApiBaseUrl").Value);
            apiClient.SetBearerToken(accessToken);

            return apiClient;
        }
    }
}
