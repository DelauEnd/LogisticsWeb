using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class OrderRequestHandler : RequestHandlerBase
    {
        private readonly string controllerUrl = "/api/Orders";

        public OrderRequestHandler(IHttpClientService client) : base(client)
        {
        }

        public async Task<HttpResponseMessage> GetAllOrders()
            => await HttpClient.Client.GetAsync(controllerUrl);

        public async Task<HttpResponseMessage> GetOrderById(int orderId)
            => await HttpClient.Client.GetAsync(controllerUrl + $"/{orderId}");

        public async Task<HttpResponseMessage> DeleteOrderById(int orderId)
            => await HttpClient.Client.DeleteAsync(controllerUrl + $"/{orderId}");

        public async Task<HttpResponseMessage> PatchOrderById(int orderId, HttpContent content)
            => await HttpClient.Client.PatchAsync(controllerUrl + $"/{orderId}", content);

        public async Task<HttpResponseMessage> CreateOrder(HttpContent content)
               => await HttpClient.Client.PostAsync(controllerUrl, content);

        public async Task<HttpResponseMessage> CreateCargoForOrder(int orderId, HttpContent content)
               => await HttpClient.Client.PostAsync(controllerUrl + $"/{orderId}/Cargoes", content);

        public async Task<HttpResponseMessage> GetCargoesForOrder(int orderId)
               => await HttpClient.Client.GetAsync(controllerUrl + $"/{orderId}/Cargoes");
    }
}
