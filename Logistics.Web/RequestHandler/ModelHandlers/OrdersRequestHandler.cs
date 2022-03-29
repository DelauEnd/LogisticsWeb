using RequestHandler.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class OrderRequestHandler : IOrderRequestHandler
    {
        private readonly string controllerUrl = "/api/Orders";

        private readonly IHttpClientFactoryHandler _httpClientHandler;
        public OrderRequestHandler(IHttpClientFactoryHandler httpClientHandler)
        {
            _httpClientHandler = httpClientHandler;
        }

        public async Task<HttpResponseMessage> GetAllOrders()
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl);
        }

        public async Task<HttpResponseMessage> GetOrderById(int orderId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl + $"/{orderId}");
        }

        public async Task<HttpResponseMessage> DeleteOrderById(int orderId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.DeleteAsync(controllerUrl + $"/{orderId}");
        }

        public async Task<HttpResponseMessage> PatchOrderById(int orderId, HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PatchAsync(controllerUrl + $"/{orderId}", content);
        }

        public async Task<HttpResponseMessage> CreateOrder(HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PostAsync(controllerUrl, content);
        }

        public async Task<HttpResponseMessage> CreateCargoForOrder(int orderId, HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PostAsync(controllerUrl + $"/{orderId}/Cargoes", content);
        }

        public async Task<HttpResponseMessage> GetCargoesForOrder(int orderId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl + $"/{orderId}/Cargoes");
        }
    }
}
