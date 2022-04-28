using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Interfaces
{
    public interface ICargoRequestHandler
    {
        public Task<HttpResponseMessage> GetAllCargoes();
        public Task<HttpResponseMessage> GetUnassignedCargoes();
        public Task<HttpResponseMessage> GetCargoById(int cargoId);
        public Task<HttpResponseMessage> DeleteCargoById(int cargoId);
        public Task<HttpResponseMessage> PatchCargoById(int cargoId, HttpContent content);
        public Task<HttpResponseMessage> AssignCargoesToRoute(int id, HttpContent content);
    }
}
