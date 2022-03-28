using Microsoft.Extensions.Configuration;
using RequestHandler.Interfaces;
using RequestHandler.Utils;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class CargoRequestHandler : ICargoRequestHandler
    {
        private readonly string controllerUrl = "/api/Cargoes";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public CargoRequestHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> GetAllCargoes()
        {
            
            using HttpClient client = await HttpClientFactoryHandler.BuildClient(_httpClientFactory, _configuration);
            return await client.GetAsync(controllerUrl);
        }
    }
}
