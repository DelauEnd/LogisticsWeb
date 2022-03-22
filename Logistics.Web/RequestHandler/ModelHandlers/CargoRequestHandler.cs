using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class CargoRequestHandler : RequestHandlerBase
    {
        private readonly string controllerUrl = "/api/Cargoes";

        public CargoRequestHandler(IHttpClientService client) : base(client)
        {
        }

        public async Task<HttpResponseMessage> GetAllCargoes()
            => await HttpClient.Client.GetAsync(controllerUrl);

        public async Task<HttpResponseMessage> GetUnassignedCargoes()
            => await HttpClient.Client.GetAsync(controllerUrl + "/Unassigned");

        public async Task<HttpResponseMessage> GetCargoById(int cargoId)
            => await HttpClient.Client.GetAsync(controllerUrl + $"/{cargoId}");

        public async Task<HttpResponseMessage> DeleteCargoById(int cargoId)
            => await HttpClient.Client.DeleteAsync(controllerUrl + $"/{cargoId}");

        public async Task<HttpResponseMessage> PatchCargoById(int cargoId, HttpContent content)
            => await HttpClient.Client.PatchAsync(controllerUrl + $"/{cargoId}", content);
    }
}
