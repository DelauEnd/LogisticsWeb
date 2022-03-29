using RequestHandler.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class TransportRequestHandler : ITransportRequestHandler
    {
        private readonly string controllerUrl = "/api/Transport";

        private readonly IHttpClientFactoryHandler _httpClientHandler;
        public TransportRequestHandler(IHttpClientFactoryHandler httpClientHandler)
        {
            _httpClientHandler = httpClientHandler;
        }

        public async Task<HttpResponseMessage> GetAllTransport()
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl);
        }

        public async Task<HttpResponseMessage> GetTransportById(int transportId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl + $"/{transportId}");
        }

        public async Task<HttpResponseMessage> DeleteTransportById(int transportId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.DeleteAsync(controllerUrl + $"/{transportId}");
        }

        public async Task<HttpResponseMessage> PatchTransportById(int transportId, HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PatchAsync(controllerUrl + $"/{transportId}", content);
        }

        public async Task<HttpResponseMessage> CreateTransport(HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PostAsync(controllerUrl, content);
        }
    }
}
