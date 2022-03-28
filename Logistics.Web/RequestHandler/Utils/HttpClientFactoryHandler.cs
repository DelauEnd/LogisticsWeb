using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Utils
{
    class HttpClientFactoryHandler
    {
        public static async Task<HttpClient> BuildClient(IHttpClientFactory _httpClientFactory, IConfiguration _configuration)
        {
            var serverClient = _httpClientFactory.CreateClient();
            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync(_configuration.GetSection("IdentityServerBaseUrl").Value);
            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "APIClient",
                ClientSecret = "API_super_secert",
                Scope = "Logistics.API",
            });
            serverClient.Dispose();

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.BaseAddress = new Uri(_configuration.GetSection("ApiBaseUrl").Value);
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            return apiClient;
        }
    }
}
