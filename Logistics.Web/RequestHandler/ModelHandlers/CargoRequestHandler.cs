using RequestHandler.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class CargoRequestHandler : ICargoRequestHandler
    {
        private readonly string controllerUrl = "/api/Cargoes";

        private readonly IHttpClientFactoryHandler _httpClientHandler;
        public CargoRequestHandler(IHttpClientFactoryHandler httpClientHandler)
        {
            _httpClientHandler = httpClientHandler;
        }

        public async Task<HttpResponseMessage> GetAllCargoes()
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl);
        }

        public async Task<HttpResponseMessage> AssignCargoesToRoute(int routeId, HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PostAsync(controllerUrl + $"/AssignToRoute/{routeId}", content);
        }

        public string BuildIdsString(int[] ids)
        {
            StringBuilder str = new StringBuilder();
            str.AppendJoin(",", ids);
            return str.ToString();
        }

        public async Task<HttpResponseMessage> GetUnassignedCargoes()
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl + "/Unassigned");
        }

        public async Task<HttpResponseMessage> GetCargoById(int cargoId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl + $"/{cargoId}");
        }

        public async Task<HttpResponseMessage> DeleteCargoById(int cargoId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.DeleteAsync(controllerUrl + $"/{cargoId}");
        }

        public async Task<HttpResponseMessage> PatchCargoById(int cargoId, HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PatchAsync(controllerUrl + $"/{cargoId}", content);
        }
    }
}
