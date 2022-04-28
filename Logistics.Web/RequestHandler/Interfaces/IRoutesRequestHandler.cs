using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Interfaces
{
    public interface IRouteRequestHandler
    {
        public Task<HttpResponseMessage> GetAllRoutes();
        public Task<HttpResponseMessage> GetRouteById(int routeId);
        public Task<HttpResponseMessage> DeleteRouteById(int routeId);
        public Task<HttpResponseMessage> PutRouteById(int routeId, HttpContent content);
        public Task<HttpResponseMessage> CreateRoute(HttpContent content);
        public Task<HttpResponseMessage> GetCargoesForRoute(int routeId);
    }
}
