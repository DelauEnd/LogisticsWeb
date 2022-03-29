using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Interfaces
{
    public interface ITransportRequestHandler
    {
        public Task<HttpResponseMessage> GetAllTransport();
        public Task<HttpResponseMessage> GetTransportById(int transportId);
        public Task<HttpResponseMessage> DeleteTransportById(int transportId);
        public Task<HttpResponseMessage> PatchTransportById(int transportId, HttpContent content);
        public Task<HttpResponseMessage> CreateTransport(HttpContent content);
    }
}
