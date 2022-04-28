using RequestHandler.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class RouteRequestHandler : IRouteRequestHandler
    {
        private readonly string controllerUrl = "/api/Routes";

        private readonly IHttpClientFactoryHandler _httpClientHandler;
        public RouteRequestHandler(IHttpClientFactoryHandler httpClientHandler)
        {
            _httpClientHandler = httpClientHandler;
        }

        public async Task<HttpResponseMessage> GetAllRoutes()
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl);
        }

        public async Task<HttpResponseMessage> GetRouteById(int routeId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl + $"/{routeId}");
        }
        public async Task<HttpResponseMessage> DeleteRouteById(int routeId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.DeleteAsync(controllerUrl + $"/{routeId}");
        }

        public async Task<HttpResponseMessage> PutRouteById(int routeId, HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PutAsync(controllerUrl + $"/{routeId}", content);
        }

        public async Task<HttpResponseMessage> CreateRoute(HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PostAsync(controllerUrl, content);
        }

        public async Task<HttpResponseMessage> GetCargoesForRoute(int routeId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl + $"/{routeId}/Cargoes");
        }
    }
}
