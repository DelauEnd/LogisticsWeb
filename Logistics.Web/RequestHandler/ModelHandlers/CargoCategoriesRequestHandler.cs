using Microsoft.Extensions.Configuration;
using RequestHandler.Interfaces;
using RequestHandler.Utils;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class CargoCategoriesRequestHandler : ICargoCategoriesRequestHandler
    {
        private readonly string controllerUrl = "/api/Categories";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public CargoCategoriesRequestHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> GetAllCategories()
        {
            using HttpClient client = await HttpClientFactoryHandler.BuildClient(_httpClientFactory, _configuration);
            return await client.GetAsync(controllerUrl);
        }
    }
}
