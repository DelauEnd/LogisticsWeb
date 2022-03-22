using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class TransportRequestHandler : RequestHandlerBase
    {
        private readonly string controllerUrl = "/api/Transport";

        public TransportRequestHandler(IHttpClientService client) : base(client)
        {
        }

        public async Task<HttpResponseMessage> GetAllTransport()
            => await HttpClient.Client.GetAsync(controllerUrl);

        public async Task<HttpResponseMessage> GetTransportById(int transportId)
            => await HttpClient.Client.GetAsync(controllerUrl + $"/{transportId}");

        public async Task<HttpResponseMessage> DeleteTransportById(int transportId)
            => await HttpClient.Client.DeleteAsync(controllerUrl + $"/{transportId}");

        public async Task<HttpResponseMessage> PatchTransportById(int transportId, HttpContent content)
            => await HttpClient.Client.PatchAsync(controllerUrl + $"/{transportId}", content);

        public async Task<HttpResponseMessage> CreateTransport(HttpContent content)
               => await HttpClient.Client.PostAsync(controllerUrl, content);
    }
}
