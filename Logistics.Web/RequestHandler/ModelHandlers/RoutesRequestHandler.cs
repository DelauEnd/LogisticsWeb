using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class RouteRequestHandler : RequestHandlerBase
    {
        private readonly string controllerUrl = "/api/Routes";

        public RouteRequestHandler(IHttpClientService client) : base(client)
        {
        }

        public async Task<HttpResponseMessage> GetAllRoutes()
            => await HttpClient.Client.GetAsync(controllerUrl);

        public async Task<HttpResponseMessage> GetRouteById(int routeId)
            => await HttpClient.Client.GetAsync(controllerUrl + $"/{routeId}");

        public async Task<HttpResponseMessage> DeleteRouteById(int routeId)
            => await HttpClient.Client.DeleteAsync(controllerUrl + $"/{routeId}");

        public async Task<HttpResponseMessage> PutRouteById(int routeId, HttpContent content)
            => await HttpClient.Client.PutAsync(controllerUrl + $"/{routeId}", content);

        public async Task<HttpResponseMessage> CreateRoute(HttpContent content)
               => await HttpClient.Client.PostAsync(controllerUrl, content);

        public async Task<HttpResponseMessage> AssignCargoesToRoute(int routeId, int[] ids)
            => await HttpClient.Client.PostAsync(controllerUrl + $"/{routeId}/Cargoes?ids=" + BuildIdsString(ids), EmptyContent);

        public string BuildIdsString(int[] ids)
        {
            StringBuilder str = new StringBuilder();
            str.AppendJoin(",", ids);
            return str.ToString();
        }

        public async Task<HttpResponseMessage> GetCargoesForRoute(int routeId)
               => await HttpClient.Client.GetAsync(controllerUrl + $"/{routeId}/Cargoes");
    }
}
